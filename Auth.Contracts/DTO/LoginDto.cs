namespace Auth.Contracts.DTO
{
    /// <summary>
    /// Информация о пользователе для аутентификации
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Логин
        /// </summary>
        public  string Login {  get; set; } = string.Empty;
        /// <summary>
        /// Пароль
        /// </summary>
        public  string Password { get; set; } = string.Empty;
    }
}
