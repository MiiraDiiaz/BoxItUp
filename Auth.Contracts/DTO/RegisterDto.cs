namespace Auth.Contracts.DTO
{
    /// <summary>
    /// Данные для создания нового пользователя
    /// </summary>
    public class RegisterDto
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; } = string.Empty;
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }
}
