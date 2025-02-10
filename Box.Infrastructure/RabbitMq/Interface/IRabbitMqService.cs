namespace Box.Infrastructure.RabbitMq.Interface
{
    /// <summary>
    /// Сервис для работы с RabbitMQ.
    /// </summary>
    public interface IRabbitMqService
    {
        /// <summary>
        /// Начинает прослушивание сообщений из указанной очереди.
        /// </summary>
        /// <param name="queueName">Имя очереди для прослушивания.</param>
        /// <returns>Сообщение из очереди.</returns>
        Task<string> StartListeningAsync(string queueName);
    }
}