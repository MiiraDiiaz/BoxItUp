using Box.Contracts.DTO;
using Box.Domain.Models;

namespace Box.Applicaton.Interface
{
    /// <summary>
    /// Интерфейс для сервиса работы с элементами коробок.
    /// </summary>
    public interface IItemBoxService
    {
        /// <summary>
        /// Добавляет новый элемент в указанную коробку.
        /// </summary>
        /// <param name="idBox">Идентификатор коробки, в которую добавляется элемент.</param>
        /// <param name="dto">Модель элемента коробки для добавления.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>DTO добавленного элемента коробки.</returns>
        Task<ItemBoxDto> Add(Guid idBox, ShortItemBoxDto dto, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет элемент коробки по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор удаляемого элемента коробки.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>True, если элемент был успешно удален; иначе - false.</returns>
        Task<bool> Delete(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получает все элементы для указанной коробки.
        /// </summary>
        /// <param name="idBox">Идентификатор коробки, для которой нужно получить элементы.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>Массив DTO всех элементов коробки.</returns>
        Task<ItemBoxDto[]> GetAll(Guid idBox, CancellationToken cancellationToken);

        /// <summary>
        /// Получает элемент коробки по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор элемента коробки.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>DTO элемента коробки или null, если элемент не найден.</returns>
        Task<ItemBox?> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Обновляет существующий элемент коробки.
        /// </summary>
        /// <param name="id">Идентификатор обновляемого элемента коробки.</param>
        /// <param name="dto">Модель элемента коробки с обновленными данными.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>DTO обновленного элемента коробки.</returns>
        Task<ItemBoxDto> Update(Guid id, ShortItemBoxDto dto, CancellationToken cancellationToken);
    }

}