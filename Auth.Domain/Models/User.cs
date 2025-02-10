namespace Auth.Domain.Models
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// Уникальный логин пользователя
        /// </summary>
        public string Login { get; set; } = string.Empty;
        /// <summary>
        /// Хэш пароля для безопасности
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;
        /// <summary>
        /// Роль пользователя
        /// </summary>
        public string Role { get; set; } = string.Empty;
        /// <summary>
        /// Электронная почта пользователя
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Дата и время создания пользователя
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }
        /// <summary>
        /// Дата и время последнего обновления информации о пользователе
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }
        /// <summary>
        /// Навигационное свойство
        /// </summary>
        public virtual List<RefreshTokens> RefreshTokens { get; set; } = [];
    }
}
