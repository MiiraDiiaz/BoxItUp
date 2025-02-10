using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Auth.DataAccess
{
    /// <summary>
    /// Основной контекст данных AuthService
    /// </summary>
    public class AuthServiceDbContext : DbContext
    {
        /// <summary>
        /// Конструктор <see cref="AuthServiceDbContext"/>
        /// </summary>
        /// <param name="options">Настройки контекста</param>
        public AuthServiceDbContext(DbContextOptions options) : base(options)
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
