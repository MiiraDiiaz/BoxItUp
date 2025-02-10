using Box.Infrastructure.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
namespace Box.Infrastructure.DataAccess.Configurations
{
    /// <summary>
    /// Конфигурация <see cref="BoxServiceDbContext"/> контекста 
    /// </summary>
    public class BoxServiceDbContextConfiguration : IDbContextOptionsConfigurator<BoxServiceDbContext>
    {
        private const string PostgresConnectionStringName = "SqlServerBoxServiceDb";

        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;

        /// <summary>
        /// Конструктор <see cref="AuthServiceDbContextConfiguration">
        /// </summary>
        /// <param name="configuration">Конфигурации</param>
        /// <param name="loggerFactory">Фабрика логгеров</param>
        public BoxServiceDbContextConfiguration(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">Строка подключения не найдена в конфигурациях</exception>
        public void Configure(DbContextOptionsBuilder<BoxServiceDbContext> optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString(PostgresConnectionStringName);
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException(
                    $"Не найдена строка подключения с именем {PostgresConnectionStringName}");
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
