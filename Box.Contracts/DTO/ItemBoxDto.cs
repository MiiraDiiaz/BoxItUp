namespace Box.Contracts.DTO
{
    /// <summary>
    /// DTO для модели ItemBox
    /// </summary>
    public class ItemBoxDto
    {
        /// <summary>
        /// Идентификатор элемента списка
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование элемента списка
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание элемента списка
        /// </summary>
        public string Description { get; set; } = string.Empty;

    }
}
