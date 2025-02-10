using Microsoft.AspNetCore.Authorization;

namespace Box.Applicaton.Handler
{
    /// <summary>
    /// Требование авторизации для проверки, что пользователь имеет доступ только для определенных ролей.
    /// </summary>
    public class UserOnlyRequirement : IAuthorizationRequirement { }
}