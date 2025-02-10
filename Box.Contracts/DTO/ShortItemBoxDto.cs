namespace Box.Contracts.DTO
{
    /// <summary>
    /// Упрощенный вариант DTO для модели ItemBox
    /// </summary>
    public class ShortItemBoxDto
    {
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
