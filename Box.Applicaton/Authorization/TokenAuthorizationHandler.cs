using Box.Applicaton.BackgroundServices.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Box.Applicaton.Handler
{
    /// <summary>
    /// Обработчик авторизации для проверки требований пользователя.
    /// </summary>
    public class TokenAuthorizationHandler : AuthorizationHandler<UserOnlyRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Конструктор обработчика авторизации.
        /// </summary>
        /// <param name="httpContextAccessor">Accessor для доступа к текущему контексту HTTP.</param>
        public TokenAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Обрабатывает требования авторизации.
        /// </summary>
        /// <param name="context">Контекст авторизации, содержащий информацию о пользователе и требованиях.</param>
        /// <param name="requirement">Требование, которое необходимо проверить.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserOnlyRequirement requirement)
        {
            var userRole = UserContext.Role;

            // Проверка роли пользователя и успешное выполнение требования, если роль соответствует
            if (userRole == "User") context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }

}
