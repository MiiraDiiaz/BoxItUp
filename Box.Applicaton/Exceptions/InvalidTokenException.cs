namespace Box.Applicaton.Exceptions
{
    /// <summary>
    /// Ошибка при извлечении и валидации токена
    /// </summary>
    public class InvalidTokenException: Exception
    {
        public InvalidTokenException(): base() { }

        public InvalidTokenException(string message) : base(message) { }
    }
}
