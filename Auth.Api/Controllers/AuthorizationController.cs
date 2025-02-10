using Auth.Contracts.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Auth.Applicaton.Exceptions;
using FluentValidation;
using Auth.Applicaton.Interfaces;
using Auth.Infrastructure.RabbitMQ.Interfaces;

namespace Auth.API.Controllers
{
    /// <summary>
    /// Контроллер для авторизации и регистрации
    /// </summary>
    /// <response code="500"> Произошла внутрення ошибка </response>
    [ApiController]
    [AllowAnonymous]
    [Route("Auth")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Contracts.DTO.ErrorDto), StatusCodes.Status500InternalServerError)]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IValidator<RegisterDto> _registValidator;
        private readonly IValidator<LoginDto> _loginValidator;
        private readonly IRabbitMqService _mqService;
        /// <summary>
        /// Инициализирует контроллер
        /// </summary>
        /// <param name="authenticationService">Сервис аутентификации</param>
        public AuthorizationController (
            IAuthenticationService authenticationService, 
            IValidator<RegisterDto> registValidator,
            IValidator<LoginDto> loginValidator,
            IRabbitMqService mqService)
        {
            _authenticationService = authenticationService;
            _registValidator = registValidator;
            _loginValidator = loginValidator;
            _mqService = mqService;
        }

        /// <summary>
        /// Создаёт пользователя
        /// </summary>
        /// <param name="dto">Модель для регистрации</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="202">Создание выполнено успешно</response>
        /// <response code="400">Модель данных не валидна</response>
        /// <returns>ID созданного пользователя</returns>
        [HttpPost("Register")]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegisterDto dto, CancellationToken cancellationToken)
        {
            var resultValidate = _registValidator.Validate(dto);
            if (!resultValidate.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, resultValidate.Errors);
            }

            Guid result;
            try
            {
                result = await _authenticationService.Register(dto, cancellationToken);
            }
            catch (InvalidLoginDataException e)
            {
                ModelState.AddModelError("login", e.Message);
                return BadRequest(ModelState);
            }
            return CreatedAtAction(nameof(Register), result);
        }
        /// <summary>
        /// Аутентифицирует пользователя
        /// </summary>
        /// <param name="dto">Модель для аутентификации</param>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="400">Модель данных не валидна</response>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>JWT токен</returns>
        [HttpPost("Login")]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LoginDto dto, CancellationToken cancellationToken)
        {
            var resultValidate = _loginValidator.Validate(dto);
            if (!resultValidate.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, resultValidate.Errors);
            }
             
            try
            {
                var loginResult = await _authenticationService.Login(dto, cancellationToken);

                await _mqService.SendMessage(loginResult, "token-queue");

                return await Task.Run(() => Ok(loginResult), cancellationToken);
            }
            catch (InvalidLoginDataException e)
            {
                ModelState.AddModelError("errors", "Invalid login or password");
                return BadRequest(ModelState);
            }
        }
    }
}
