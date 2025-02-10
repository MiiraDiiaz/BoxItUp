using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Box.Infrastructure.RabbitMq.Interface;
using Box.Applicaton.Interface;


namespace Box.Applicaton.BackgroundServices
{
    /// <summary>
    /// Сервис для прослушивания сообщений RabbitMQ в фоновом режиме.
    /// </summary>
    public class RabbitMqListenerService : BackgroundService
    {
        private readonly IRabbitMqService _rabbitMqService;
        private readonly IJwtProvider _jwtProvider;
        private readonly ILogger<RabbitMqListenerService> _logger;

        /// <summary>
        /// Конструктор сервиса RabbitMqListenerService.
        /// </summary>
        /// <param name="rabbitMqService">Сервис для работы с RabbitMQ.</param>
        /// <param name="logger">Логгер для записи ошибок.</param>
        /// <param name="jwtProvider">Сервис для работы с JWT токенами.</param>
        public RabbitMqListenerService(IRabbitMqService rabbitMqService,
                                        ILogger<RabbitMqListenerService> logger,
                                        IJwtProvider jwtProvider)
        {
            _rabbitMqService = rabbitMqService;
            _jwtProvider = jwtProvider;
            _logger = logger;
        }

        /// <summary>
        /// Выполняет асинхронную работу в фоновом режиме.
        /// </summary>
        /// <param name="stoppingToken">Токен отмены для остановки фоновой задачи.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var token = await _rabbitMqService.StartListeningAsync("token-queue");
                    if (!string.IsNullOrEmpty(token))
                    {
                        _jwtProvider.DecodeToken(token);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Ошибка при обработке токена: {ex.Message}");
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }


}
