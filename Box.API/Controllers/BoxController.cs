using Box.Applicaton.BackgroundServices;
using Box.Applicaton.Exceptions;
using Box.Applicaton.Interface;
using Box.Contracts.DTO;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ErrorDto = Box.Contracts.DTO.ErrorDto;

namespace Box.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с коробками
    /// </summary>
    /// <response code="500"> Произошла внутрення ошибка </response>
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "UserOnly")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
    public class BoxController : ControllerBase
    {
        private readonly ILogger<BoxController> _logger;
        private readonly IBoxService _service;
        private readonly RabbitMqListenerService _rabbitMqListenerService;
        private readonly IValidator<ShortBoxDto> _validator;

        /// <summary>
        /// Инициализирует экземпляр <see cref="BoxController"/>
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="service">Сервис CRUD операции с коробкой</param>
        /// <param name="rabbitMqListenerService">Фоновый сервис для прослушивания сообщений в очереди RabbitMq</param>
        public BoxController(ILogger<BoxController> logger, 
                        IBoxService service,
                        RabbitMqListenerService rabbitMqListenerService,
                        IValidator<ShortBoxDto> validator)
        {
            _logger = logger;
            _service = service;
            _validator = validator;
            _rabbitMqListenerService = rabbitMqListenerService;
        }

        /// <summary>
        /// Получает список коробок
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="403">Доступ запрещен</response>
        /// <returns>Список коробок <see cref="ShortBoxDto"/></returns>
        [HttpPost("Getall")]
        [ProducesResponseType(typeof(IEnumerable<ShortBoxDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<ShortBoxDto>), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            _logger.LogTrace($"Получили список коробок");
            var result = await _service.GetAll(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Создание коробки
        /// </summary>
        /// <param name="dto">Наименование коробки</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="201">Успешно создано</response>
        /// <response code="400">Модель невалидна</response>
        /// <response code="403">Доступ запрещен</response>
        /// <response code="422">Конфликт бизнес логики</response>
        /// <returns>Модель созданной коробки <see cref="BoxDto"/></returns>
        [HttpPost("AddBox")]
        [ProducesResponseType(typeof(ShortBoxDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<ShortBoxDto>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateBox([FromBody] ShortBoxDto dto, CancellationToken cancellationToken)
        {
            var resultValidate = _validator.Validate(dto);
            if (!resultValidate.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, resultValidate.Errors);
            }

            _logger.LogTrace($"Создали коробку {dto.Name}");
            var result = await _service.Add(dto, cancellationToken);
            return CreatedAtAction(nameof(CreateBox), result);
        }

        /// <summary>
        /// Обновляет данные о коробке
        /// </summary>
        /// <param name="id">Идентификатор обновляемой коробки</param>
        /// <param name="dto">Модель коробки</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="200">Запрос успешно выполнен</response>
        /// <response code="400">Модель невалидна</response>
        /// <response code="403">Доступ запрещен</response>
        /// <response code="404">Коробка не найденa</response>
        /// <response code="422">Конфликт бизнес логики</response>
        /// <returns>Модель обновленной коробки <see cref="BoxDto"/></returns>
        [HttpPut("UpdateBox/{id:guid}")]
        [ProducesResponseType(typeof(ShortBoxDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Update(Guid id, [FromForm] ShortBoxDto dto, CancellationToken cancellationToken)
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
        /// Удаляет коробку по ID
        /// </summary>
        /// <param name="id">Идентификатор коробки</param>
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
