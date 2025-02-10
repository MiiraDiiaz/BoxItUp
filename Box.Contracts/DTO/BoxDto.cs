namespace Box.Contracts.DTO
{
    /// <summary>
    /// Модель Box для удаления и обновления 
    /// </summary>
    public class BoxDto
    {
        /// <summary>
        /// Уникальный идентификатор коробки
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Имя коробки
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
