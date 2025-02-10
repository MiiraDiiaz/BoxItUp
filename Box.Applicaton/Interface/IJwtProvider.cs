namespace Box.Applicaton.Interface
{
    /// <summary>
    /// Интерфейс для работы с JWT токенами.
    /// </summary>
    public interface IJwtProvider
    {
        /// <summary>
        /// Декодирует и валидирует JWT токен.
        /// </summary>
        /// <param name="token">JWT токен для декодирования.</param>
        void DecodeToken(string token);
    }
}