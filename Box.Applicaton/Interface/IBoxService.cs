using Box.Contracts.DTO;

namespace Box.Applicaton.Interface
{
    /// <summary>
    /// Интерфейс для сервиса работы с коробками.
    /// </summary>
    public interface IBoxService
    {
        /// <summary>
        /// Получает все коробки.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>Массив DTO всех коробок.</returns>
        Task<BoxDto[]> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Получает коробку по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор коробки.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>DTO коробки или null, если коробка не найдена.</returns>
        Task<BoxDto?> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новую коробку.
        /// </summary>
        /// <param name="dto">Модель коробки для добавления.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>DTO добавленной коробки.</returns>
        Task<BoxDto> Add(ShortBoxDto dto, CancellationToken cancellationToken);

        /// <summary>
        /// Обновляет существующую коробку.
        /// </summary>
        /// <param name="id">Идентификатор обновляемой коробки.</param>
        /// <param name="dto">Модель коробки с обновленными данными.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>DTO обновленной коробки.</returns>
        Task<BoxDto> Update(Guid id, ShortBoxDto dto, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет коробку по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор удаляемой коробки.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>True, если коробка была успешно удалена; иначе - false.</returns>
        Task<bool> Delete(Guid id, CancellationToken cancellationToken);
    }

}