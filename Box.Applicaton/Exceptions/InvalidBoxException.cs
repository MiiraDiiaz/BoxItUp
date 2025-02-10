namespace Box.Applicaton.Exceptions
{
    /// <summary>
    /// Исключение, возникающее при недопустимых данных коробки.
    /// </summary>
    public class InvalidBoxException: Exception
    {
        public InvalidBoxException():base() { }

        public InvalidBoxException(string message) : base(message) { }
    }
}
