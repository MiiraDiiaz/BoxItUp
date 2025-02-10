using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Box.Infrastructure.DataAccess
{
    /// <summary>
    /// Основной контекст данных BoxService
    /// </summary>
    public class BoxServiceDbContext : DbContext
    {
        /// <summary>
        /// Конструктор <see cref="BoxServiceDbContext"/>
        /// </summary>
        /// <param name="options">Настройки контекста</param>
        public BoxServiceDbContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Конфигурирует модель данных при создании контекста.
        /// </summary>
        /// <param name="modelBuilder">Объект для построения модели данных.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), t => t.GetInterfaces().Any(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
        }
    }
}
