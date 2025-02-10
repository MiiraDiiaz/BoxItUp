namespace Auth.Infrastructure.RabbitMQ.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для работы с RabbitMQ
    /// </summary>
    public interface IRabbitMqService
    {
        /// <summary>
        /// Отправляет сообщение в указанную очередь RabbitMQ.
        /// </summary>
        /// <param name="message">Сообщение для отправки.</param>
        /// <param name="queueName">Имя очереди, в которую будет отправлено сообщение.</param>
        Task SendMessage(string message, string queueName);
    }
}
