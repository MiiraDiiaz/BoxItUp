using Auth.Infrastructure.RabbitMQ.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;

namespace Auth.Infrastructure.RabbitMQ
{
    /// <inheritdoc cref="IRabbitMqService"/>
    /// <summary>
    /// Реализация сервиса для работы с RabbitMQ.
    /// </summary>
    public class RabbitMqService : IRabbitMqService, IDisposable
    {
        private readonly ILogger<RabbitMqService> _logger;
        private readonly IConnection _connection;
        private readonly IOptions<RabbitMqOptions> _options;
        private readonly IModel _channel;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RabbitMqService"/>.
        /// </summary>
        /// <param name="options">Опции RabbitMQ.</param>
        /// <param name="logger">Логгер для записи сообщений.</param>
        public RabbitMqService(IOptions<RabbitMqOptions> options,
                               ILogger<RabbitMqService> logger)
        {
            _options = options;
            _logger = logger;
            var factory = new ConnectionFactory
            {
                HostName = _options.Value.HostName,
                Port = _options.Value.Port,
                UserName = _options.Value.UserName,
                Password = _options.Value.Password,
            };
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to connect to RabbitMQ: {ex.Message}");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task SendMessage(string message, string queueName)
        {
            _channel.QueueDeclare(queueName,
                            durable: true,
                            exclusive: false,
                            autoDelete: false);

            var body = Encoding.UTF8.GetBytes(message);

            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            await Task.Run(() =>
            {
                _channel.BasicPublish(exchange: "",
                            routingKey: queueName,
                            basicProperties: properties,
                            body: body);
            });
        }

        /// <summary>
        /// Освобождает ресурсы, используемые сервисом RabbitMQ.
        /// </summary>
        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
