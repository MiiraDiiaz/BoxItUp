using Microsoft.EntityFrameworkCore;

namespace Box.Infrastructure.DataAccess.Interfaces
{
    /// <summary>
    /// Конфигуратор контекста.
    /// </summary>
    public interface IDbContextOptionsConfigurator<TContext> where TContext : DbContext
    {
        /// <summary>
        /// Выполняет конфигурацию контекста.
        /// </summary>
        /// <param name="options">настройки.</param>
        void Configure(DbContextOptionsBuilder<TContext> options);
    }
}
