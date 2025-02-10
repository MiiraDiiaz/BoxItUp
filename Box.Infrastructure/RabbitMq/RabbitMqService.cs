using Box.Infrastructure.RabbitMq.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


namespace Box.Infrastructure.RabbitMq
{
    /// <inheritdoc cref="IUserRepository"/>
    public class RabbitMqService : IDisposable, IRabbitMqService
    {
        private readonly ILogger<RabbitMqService> _logger;
        private readonly IConnection _connection;
        private readonly IOptions<RabbitMqOptions> _options;
        private readonly IModel _channel;

        /// <summary>
        /// Конструктор сервиса RabbitMQ.
        /// </summary>
        /// <param name="options">Настройки RabbitMQ.</param>
        /// <param name="logger">Логгер для записи ошибок.</param>
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
                _logger.LogError($"Нет соединения с сервисом RabbitMQ: {ex.Message}");
                throw;
            }
        }

        /// <inheritdoc cref="IUserRepository"/>
        public async Task<string> StartListeningAsync(string queueName)
        {
            _channel.QueueDeclare(queueName,
                      durable: true,
                      exclusive: false,
                      autoDelete: false);
            var consumer = new EventingBasicConsumer(_channel);

            var result = await Task.Run(() => _channel.BasicGet(queueName, false));

            ValidateResult(result, queueName);

            var body = result.Body.ToArray();
            var token = Encoding.UTF8.GetString(body);

            try
            {
                if (result != null)
                {
                    _channel.BasicAck(deliveryTag: result.DeliveryTag, multiple: false);
                }
                else
                {
                    ErrorRabbitMq(queueName);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка при обработке сообщения: {ex.Message}");
                if (result != null)
                {
                    _channel.BasicNack(deliveryTag: result.DeliveryTag, multiple: false, requeue: true);
                }
            }

            if (string.IsNullOrEmpty(token))
            {
                ErrorRabbitMq(queueName);
            }
            return token;
        }


        /// <summary>
        /// Проверяет результат получения сообщения и выбрасывает исключение, если сообщение отсутствует.
        /// </summary>
        /// <param name="result">Результат получения сообщения.</param>
        /// <param name="queueName">Имя очереди.</param>
        private void ValidateResult(BasicGetResult result, string queueName)
        {
            if (result == null)
            {
                ErrorRabbitMq(queueName);
            }
        }

        /// <summary>
        /// Обрабатывает ошибку, если сообщение отсутствует в очереди.
        /// </summary>
        /// <param name="queueName">Имя очереди.</param>
        private void ErrorRabbitMq(string queueName)
        {
            _logger.LogError($"RabbitMQ не содержит сообщение в очереди. QueueName: {queueName}");
            throw new InvalidOperationException($"RabbitMQ не содержит сообщение в очереди. QueueName: {queueName}");
        }

        /// <summary>
        /// Освобождает ресурсы.
        /// </summary>
        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
