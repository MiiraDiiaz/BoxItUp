using AutoMapper;
using AutoMapper.QueryableExtensions;
using Box.Contracts.DTO;
using Box.Domain.Models;
using Box.Infrastructure.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Box.Infrastructure.DataAccess.Repository
{
    /// <inheritdoc cref="IItemBoxRepository"/>
    public class ItemBoxRepository: IItemBoxRepository
    {
        private readonly IRepository<ItemBox> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор класса <see cref="ItemBoxRepository"/>.
        /// </summary>
        /// <param name="repository">Репозиторий для работы с сущностями Box.</param>
        /// <param name="mapper">Mapper для преобразования между сущностями и DTO.</param>
        public ItemBoxRepository(IRepository<ItemBox> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public Task<ItemBoxDto[]> GetAll(Guid id, CancellationToken cancellationToken)
        {
            return _repository
                .GetAll()
                .Where(x => x.BoxId == id)
                .ProjectTo<ItemBoxDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public Task<ItemBox?> GetById(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<ItemBox?> FindWhere(Expression<Func<ItemBox, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _repository.GetAllFiltered(predicate).FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<ItemBoxDto> Add(ItemBox box, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(box, cancellationToken);
            return _mapper.Map<ItemBoxDto>(box);
        }

        /// <inheritdoc/>
        public async Task<ItemBoxDto> Update(ItemBox box, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(box, cancellationToken);
            return _mapper.Map<ItemBoxDto>(box);
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
