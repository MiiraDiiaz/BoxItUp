using Auth.Applicaton.User.Repository;
using Auth.Contracts.DTO;
using Auth.DataAccess.Interfaces;
using Auth.Domain.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Auth.Infrastructure.DataAccess.Repository
{
    /// <inheritdoc cref="IUserRepository"/>
    public class UserRepository : IUserRepository
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="UserRepository"/>.
        /// </summary>
        /// <param name="repository">Репозиторий для работы с сущностью User.</param>
        /// <param name="mapper">Mapper для преобразования сущностей.</param>
        public UserRepository(IRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public async Task<ShortUserDto> Add(User user, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(user, cancellationToken);
            return _mapper.Map<ShortUserDto>(user);
        }
        /// <inheritdoc/>
        public async Task<User?> FindWhere(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _repository.GetAllFiltered(predicate).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
