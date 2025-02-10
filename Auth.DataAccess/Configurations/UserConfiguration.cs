using Auth.Domain.Enums;
using Auth.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.DataAccess.Configurations
{
    /// <summary>
    /// Конфигурация EF Core сущности <see cref="Auth.Domain.Models.User"/>
    /// Определяет настройки для таблицы User в базе данных.
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Конфигурирует сущность <see cref="User"/> с использованием указанного строителя.
        /// </summary>
        /// <param name="builder">Строитель для конфигурации сущности.</param>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.UserName)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(u => u.Login)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(u => u.PasswordHash)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.CreatedDate)
                .IsRequired();

            builder.HasIndex(u => u.Login).IsUnique();

            builder.HasMany(u => u.RefreshTokens)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasData(new User()
            {
                Id = new Guid("6a3480d3-f38d-4383-9e48-09ca297ba2a9"),
                UserName = "Admin",
                Login = "login",
                PasswordHash = "3fc0a7acf087f549ac2b266baf94b8b1",
                Role = Role.Admin.ToString(),
                Email = "admin@gmail.com",
                CreatedDate = new DateTime(2025, 1, 28, 8, 34, 17, 719, DateTimeKind.Utc).AddTicks(4429)
            });
        }
    }
}
