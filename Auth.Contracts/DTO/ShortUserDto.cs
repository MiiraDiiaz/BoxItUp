namespace Auth.Contracts.DTO
{
    /// <summary>
    /// Краткая информация о пользователе
    /// </summary>
    public class ShortUserDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; } = string.Empty;
    }
}
