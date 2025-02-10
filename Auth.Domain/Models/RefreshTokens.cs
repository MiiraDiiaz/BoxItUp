namespace Auth.Domain.Models
{
    /// <summary>
    /// Токен обновления
    /// </summary>
    public class RefreshTokens
    {
        /// <summary>
        /// Уникальный идентификатор токена
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Значение токена
        /// </summary>
        public string Token { get; set; } = string.Empty;
        /// <summary>
        /// Дата и время истечения срока действия токена
        /// </summary>
        public DateTime ExpiresAt { get; set; }
        /// <summary>
        /// Дата и время создания токена
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Внешний ключ на User
        /// </summary>
        public Guid? UserId { get; set; }
        /// <summary>
        /// Навигационное свойство
        /// </summary>
        public virtual User? User { get; set; }
    }
}
