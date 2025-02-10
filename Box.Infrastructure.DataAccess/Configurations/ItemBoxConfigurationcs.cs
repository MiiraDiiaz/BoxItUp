using Box.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Box.Infrastructure.DataAccess.Configurations
{
    /// <summary>
    /// Конфигурация EF Core сущности <see cref="Box.Domain.Models.ItemBox"/>
    /// Определяет настройки для таблицы ItemBox в базе данных.
    /// </summary>
    public class ItemBoxConfigurationcs : IEntityTypeConfiguration<ItemBox>
    {
        /// <summary>
        /// Конфигурирует сущность <see cref="ItemBox"/> с использованием указанного строителя.
        /// </summary>
        /// <param name="builder">Строитель для конфигурации сущности.</param>
        public void Configure(EntityTypeBuilder<ItemBox> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(i => i.Description)
                .HasMaxLength(500);

            builder.HasOne(b => b.Box)
                .WithMany(i => i.Items)
                .HasForeignKey(i => i.BoxId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
