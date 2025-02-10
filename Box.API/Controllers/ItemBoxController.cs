using Box.Applicaton.BackgroundServices;
using Box.Applicaton.Exceptions;
using Box.Applicaton.Interface;
using Box.Contracts.DTO;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Box.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с содержимым коробки
    /// </summary>
    /// <response code="500"> Произошла внутрення ошибка </response>
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "UserOnly")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
    public class ItemBoxController : ControllerBase
    {
        private readonly ILogger<ItemBoxController> _logger;
        private readonly IItemBoxService _service;
        private readonly RabbitMqListenerService _rabbitMqListenerService;
        private readonly IValidator<ShortItemBoxDto> _validator;

        /// <summary>
        /// Инициализирует экземпляр <see cref="ItemBoxController"/>
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="service">Сервис CRUD операции содержимым корорбки</param>
        /// <param name="rabbitMqListenerService">Фоновый сервис для прослушивания сообщений в очереди RabbitMq</param>
        public ItemBoxController(ILogger<ItemBoxController> logger,
                        IItemBoxService service,
                        RabbitMqListenerService rabbitMqListenerService,
                        IValidator<ShortItemBoxDto> validator)
        {
            _logger = logger;
            _service = service;
            _validator = validator;
            _rabbitMqListenerService = rabbitMqListenerService;
        }

        /// <summary>
        /// Получает список содержимого коробки используя Id коробки
        /// </summary>
        /// <param name="idBox">Id коробки</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="403">Доступ запрещен</response>
        /// <returns>Содержимое коробки<see cref="ShortBoxDto"/></returns>
        [HttpPost("Getall/{id:guid}")]
        [ProducesResponseType(typeof(IEnumerable<ShortItemBoxDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<ShortItemBoxDto>), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAll(Guid idBox, CancellationToken cancellationToken)
        {
            _logger.LogTrace($"Получили список коробки");
            var result = await _service.GetAll(idBox, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Создание Элемента коробки
        /// </summary>
        /// <param name="idBox">Id коробки</param>
        /// <param name="dto">Наименование и описание элемента коробки</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="201">Успешно создано</response>
        /// <response code="400">Модель невалидна</response>
        /// <response code="403">Доступ запрещен</response>
        /// <response code="422">Конфликт бизнес логики</response>
        /// <returns>Модель созданного элемента коробки <see cref="ItemBoxDto"/></returns>
        [HttpPost("AddBox/{id:guid}")]
        [ProducesResponseType(typeof(ShortItemBoxDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<ShortItemBoxDto>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateBox(Guid idBox, [FromBody] ShortItemBoxDto dto, CancellationToken cancellationToken)
        {
            var resultValidate = _validator.Validate(dto);
            if (!resultValidate.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, resultValidate.Errors);
            }

            _logger.LogTrace($"Создали элемент коробку {dto.Name}");
            var result = await _service.Add(idBox,dto, cancellationToken);
            return CreatedAtAction(nameof(CreateBox), result);
        }

        /// <summary>
        /// Обновляет данные о элементе коробки
        /// </summary>
        /// <param name="id">Идентификатор обновляемого элемента</param>
        /// <param name="dto">Наименование и описание элемента коробки</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="200">Запрос успешно выполнен</response>
        /// <response code="400">Модель невалидна</response>
        /// <response code="403">Доступ запрещен</response>
        /// <response code="404">Элемент не найден</response>
        /// <response code="422">Конфликт бизнес логики</response>
        /// <returns>Модель обновленного элемента <see cref="BoxDto"/></returns>
        [HttpPut("UpdateBox/{id:guid}")]
        [ProducesResponseType(typeof(ShortBoxDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Update(Guid id, [FromForm] ShortItemBoxDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var resultValidate = _validator.Validate(dto);
                if (!resultValidate.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, resultValidate.Errors);
                }

                _logger.LogTrace($"Обновили данные о коробке {dto.Name}");
                var result = await _service.Update(id, dto, cancellationToken);
                return Ok(result);
            }
            catch (UnauthorizedAccessException e)
            {
                _logger.LogError(e.Message);
                return Forbid();
            }
            catch (InvalidBoxException e)
            {
                _logger.LogError(e.Message);
                return NotFound();
            }
        }

        /// <summary>
        /// Удаляет элемент по ID
        /// </summary>
        /// <param name="id">Идентификатор элемента</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="204">Удаление произведено успешно</response>
        /// <response code="403">Нет доступа</response>
        /// <response code="404">Коробка с таким ID не найдена</response>
        [HttpDelete("DeleteBox/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var isRemoved = await _service.Delete(id, cancellationToken);
                if (isRemoved) return NoContent();
                return NotFound();
            }
            catch (UnauthorizedAccessException e)
            {
                return Forbid();
            }
        }
    }
}
