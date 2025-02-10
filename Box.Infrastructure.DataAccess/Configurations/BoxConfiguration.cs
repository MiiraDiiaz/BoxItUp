using Microsoft.EntityFrameworkCore;


namespace Box.Infrastructure.DataAccess.Configurations
{
    using Box.Domain.Models;

    /// <summary>
    /// Конфигурация EF Core сущности <see cref="Box.Domain.Models.Box"/>
    /// Определяет настройки для таблицы Box в базе данных.
    /// </summary>
    public class BoxConfiguration : IEntityTypeConfiguration<Box>
    {
        /// <summary>
        /// Конфигурирует сущность <see cref="Box"/> с использованием указанного строителя.
        /// </summary>
        /// <param name="builder">Строитель для конфигурации сущности.</param>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Box> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(b => b.CreatedDate)
                .HasConversion(b => b, b => DateTime.SpecifyKind(b, DateTimeKind.Utc))
                .IsRequired();

            builder.HasMany(b => b.Items)
                .WithOne(i => i.Box)
                .HasForeignKey(i => i.BoxId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
    
}
