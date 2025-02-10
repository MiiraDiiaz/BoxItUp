namespace Box.Infrastructure.RabbitMq
{
    /// <summary>
    /// Опции для настройки подключения к RabbitMQ для сервиса <see cref="RabbitMqService"/>.
    /// </summary>
    public class RabbitMqOptions
    {
        /// <summary>
        /// Имя хоста RabbitMQ.
        /// </summary>
        public string HostName { get; set; } = string.Empty;

        /// <summary>
        /// Порт для подключения к RabbitMQ.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Имя пользователя для аутентификации в RabbitMQ.
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Пароль для аутентификации в RabbitMQ.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
