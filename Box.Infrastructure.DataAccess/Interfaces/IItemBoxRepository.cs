using Box.Contracts.DTO;
using Box.Domain.Models;
using System.Linq.Expressions;

namespace Box.Infrastructure.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с элементами коробок.
    /// </summary>
    public interface IItemBoxRepository
    {
        /// <summary>
        /// Добавляет новый элемент коробки.
        /// </summary>
        /// <param name="box">Элемент коробки для добавления.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>DTO добавленного элемента коробки.</returns>
        Task<ItemBoxDto> Add(ItemBox box, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет элемент коробки по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор элемента коробки для удаления.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>True, если элемент был успешно удален; иначе - false.</returns>
        Task<bool> Delete(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Находит элемент коробки по заданному условию.
        /// </summary>
        /// <param name="predicate">Условие для поиска элемента коробки.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>Элемент коробки, если найден; иначе - null.</returns>
        Task<ItemBox?> FindWhere(Expression<Func<ItemBox, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Получает все элементы коробки по идентификатору коробки.
        /// </summary>
        /// <param name="id">Идентификатор коробки.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>Массив DTO элементов коробки.</returns>
        Task<ItemBoxDto[]> GetAll(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получает элемент коробки по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор элемента коробки.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>Элемент коробки, если найден; иначе - null.</returns>
        Task<ItemBox?> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Обновляет данные элемента коробки.
        /// </summary>
        /// <param name="box">Элемент коробки с обновленными данными.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>DTO обновленного элемента коробки.</returns>
        Task<ItemBoxDto> Update(ItemBox box, CancellationToken cancellationToken);
    }

}