using Auth.Contracts.DTO;
using System.Linq.Expressions;

namespace Auth.Applicaton.User.Repository
{
    using User = Auth.Domain.Models.User;

    /// <summary>
    /// Реозиторий для <see cref="Auth.Domain.Models.User"/>
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Получить список пользователей
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список пользователей <see cref="ShortUserDto"/></returns>
        //Task<ShortUserDto[]> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Получить пользователя по ID
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Пользователь <see cref="UserDto"/> по идентификатору</returns>
        //Task<UserDto?> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Найти пользователя по предикату
        /// </summary>
        /// <param name="predicate">Предикат</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Пользователь <see cref="User"/></returns>
        Task<User?> FindWhere(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Добавить пользователя по модели
        /// </summary>
        /// <param name="user">Модель</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Созданный пользователь <see cref="ShortUserDto"/></returns>
        Task<ShortUserDto> Add(User user, CancellationToken cancellationToken);

        /// <summary>
        /// Обновить пользователя по модели
        /// </summary>
        /// <param name="id">Идентификатор обновляемой сущности</param>
        /// <param name="user">Модель с обновлёнными данными</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Обновленный пользователь <see cref="ShortUserDto"/></returns>
        //Task<ShortUserDto> Update(Guid id, User user, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить пользователя по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Удален ли пользователь</returns>
        //Task<bool> Delete(Guid id, CancellationToken cancellationToken);
    }
}
