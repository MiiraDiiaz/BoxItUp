using Auth.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.DataAccess.Configurations
{
    /// <summary>
    /// Конфигурация EF Core для сущности <see cref="Auth.Domain.Models.RefreshTokens"/>.
    /// Определяет настройки для таблицы RefreshTokens в базе данных.
    /// </summary>
    public class RefreshTokensConfiguration : IEntityTypeConfiguration<RefreshTokens>
    {
        /// <summary>
        /// Конфигурирует сущность <see cref="RefreshTokens"/> с использованием указанного строителя.
        /// </summary>
        /// <param name="builder">Строитель для конфигурации сущности.</param>
        public void Configure(EntityTypeBuilder<RefreshTokens> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(t => t.Token)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(u => u.CreatedAt)
                .HasConversion(d => d, d => DateTime.SpecifyKind(d, DateTimeKind.Utc))
                .IsRequired();

            builder.HasOne(t => t.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
