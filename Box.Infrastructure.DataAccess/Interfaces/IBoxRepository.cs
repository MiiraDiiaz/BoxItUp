using Box.Contracts.DTO;
using System.Linq.Expressions;


namespace Box.Infrastructure.DataAccess.Interfaces
{
    using Box.Domain.Models;

    /// <summary>
    /// Интерфейс для работы с репозиторием коробок.
    /// </summary>
    public interface IBoxRepository
    {
        /// <summary>
        /// Получает все коробки для указанного пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронных операций.</param>
        /// <returns>Массив объектов <see cref="BoxDto"/>.</returns>
        Task<BoxDto[]> GetAll(Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Получает коробку по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор коробки.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронных операций.</param>
        /// <returns>Объект <see cref="BoxDto"/> или null, если коробка не найдена.</returns>
        Task<BoxDto?> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новую коробку.
        /// </summary>
        /// <param name="box">Объект коробки для добавления.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронных операций.</param>
        /// <returns>Добавленный объект <see cref="BoxDto"/>.</returns>
        Task<BoxDto> Add(Box box, CancellationToken cancellationToken);

        /// <summary>
        /// Находит коробку по заданному условию.
        /// </summary>
        /// <param name="predicate">Условие для поиска.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронных операций.</param>
        /// <returns>Объект <see cref="Box"/> или null, если коробка не найдена.</returns>
        Task<Box?> FindWhere(Expression<Func<Box, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Обновляет данные о коробке.
        /// </summary>
        /// <param name="box">Объект коробки с обновленными данными.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронных операций.</param>
        /// <returns>Обновленный объект <see cref="BoxDto"/>.</returns>
        Task<BoxDto> Update(Box box, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет коробку по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор коробки для удаления.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронных операций.</param>
        /// <returns>True, если удаление прошло успешно; иначе false.</returns>
        Task<bool> Delete(Guid id, CancellationToken cancellationToken);
    }

}