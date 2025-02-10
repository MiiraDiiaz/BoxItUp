namespace Auth.Applicaton.Exceptions
{
    /// <summary>
    /// Исключение, возникающее при неверных данных для входа.
    /// </summary>
    public class InvalidLoginDataException : Exception
    {
        public InvalidLoginDataException() : base() { }

        public InvalidLoginDataException(string message) : base(message) { }
    }
}
