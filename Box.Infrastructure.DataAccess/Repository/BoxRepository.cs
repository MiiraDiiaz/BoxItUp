using Box.Infrastructure.DataAccess.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Box.Contracts.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Box.Infrastructure.DataAccess.Repository
{
    using Box.Domain.Models;

    /// <inheritdoc cref="IBoxRepository"/>
    public class BoxRepository : IBoxRepository
    {
        private readonly IRepository<Box> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор класса <see cref="BoxRepository"/>.
        /// </summary>
        /// <param name="repository">Репозиторий для работы с сущностями Box.</param>
        /// <param name="mapper">Mapper для преобразования между сущностями и DTO.</param>
        public BoxRepository(IRepository<Box> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public Task<BoxDto[]> GetAll(Guid userId, CancellationToken cancellationToken)
        {
            return _repository
                .GetAll()
                .Where(x => x.IdUser == userId)
                .ProjectTo<BoxDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public Task<BoxDto?> GetById(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetAll()
                .Where(x => x.Id == id)
                .ProjectTo<BoxDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<Box?> FindWhere(Expression<Func<Box, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _repository.GetAllFiltered(predicate).FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<BoxDto> Add(Box box, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(box, cancellationToken);
            return _mapper.Map<BoxDto>(box);
        }

        /// <inheritdoc/>
        public async Task<BoxDto> Update(Box box, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(box, cancellationToken);
            return _mapper.Map<BoxDto>(box);
        }

        /// <inheritdoc/>
        public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            if (entity is null) return false;
            await _repository.DeleteAsync(entity, cancellationToken);
            return true;
        }
    }
}
