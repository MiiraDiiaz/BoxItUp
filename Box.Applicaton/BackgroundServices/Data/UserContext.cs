namespace Box.Applicaton.BackgroundServices.Data
{
    /// <summary>
    /// Класс, для хранения данных о пользователе
    /// </summary>
    public static class UserContext
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public static string UserId { get; set; } = String.Empty;
        /// <summary>
        /// роль пользователя
        /// </summary>
        public static string Role { get; set; } = String.Empty;
    }

}
