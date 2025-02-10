namespace Box.Domain.Models
{
    /// <summary>
    /// Коробка
    /// </summary>
    public class Box
    {
        /// <summary>
        /// Уникальный идентификатор коробки
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Имя коробки
        /// </summary>
        public string Name { get; set; } = String.Empty;
        /// <summary>
        /// Дата создания коробки
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        public Guid IdUser { get; set; }
        /// <summary>
        /// Навигационное свойство
        /// </summary>
        public virtual List<ItemBox>? Items { get; set; } = new List<ItemBox>();
    }
}