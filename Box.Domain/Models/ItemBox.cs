namespace Box.Domain.Models
{
    /// <summary>
    /// Предмет в коробке
    /// </summary>
    public class ItemBox
    {
        /// <summary>
        /// Уникальный идентификатор предмета
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Имя предмета коробки
        /// </summary>
        public string Name { get; set; } = String.Empty;
        /// <summary>
        /// Описание коробки
        /// </summary>
        public string Description { get; set; } = String.Empty;
        /// <summary>
        /// Внешний ключ сущности Коробка
        /// </summary>
        public Guid BoxId { get; set; }
        /// <summary>
        /// навигационное свойство
        /// </summary>
        public virtual Box Box { get; set; } 
    }
}
