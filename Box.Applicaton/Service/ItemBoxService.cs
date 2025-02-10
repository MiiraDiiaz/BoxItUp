using AutoMapper;
using Box.Applicaton.Exceptions;
using Box.Applicaton.Interface;
using Box.Contracts.DTO;
using Box.Domain.Models;
using Box.Infrastructure.DataAccess.Interfaces;

namespace Box.Applicaton.Service
{
    /// <inheritdoc cref="IItemBoxService"/>

    public class ItemBoxService :IItemBoxService
    {
        private readonly IItemBoxRepository _repository;
        private readonly IMapper _mapper;
        /// <summary>
        /// Инициализирует сервис по работе с содержимым коробки
        /// </summary>
        /// <param name="boxRepository">Репозиторий для работы с элементами коробками.</param>
        /// <param name="mapper">Объект для маппинга между моделями.</param>
        public ItemBoxService(IItemBoxRepository boxRepository,
                        IMapper mapper)
        {
            _repository = boxRepository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public Task<ItemBoxDto[]> GetAll(Guid idBox, CancellationToken cancellationToken)
        {
            return _repository.GetAll(idBox, cancellationToken);
        }

        public Task<ItemBox?> GetById(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetById(id, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<ItemBoxDto> Add(Guid idBox, ShortItemBoxDto dto, CancellationToken cancellationToken)
        {
            var box = _mapper.Map<ItemBox>(dto);
            box.BoxId = idBox;
            return await _repository.Add(box, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<ItemBoxDto> Update(Guid id, ShortItemBoxDto dto, CancellationToken cancellationToken)
        {
            var existEntity = await GetById(id, cancellationToken);
            if (existEntity is null) throw new InvalidBoxException($"Элемент коробки с Id:{id} не найдена");

            existEntity.Name = dto.Name;
            existEntity.Description = dto.Description;

            await _repository.Update(existEntity, cancellationToken);

            return _mapper.Map<ItemBoxDto>(existEntity);
        }
        /// <inheritdoc />
        public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
        {
            var existEntity = await GetById(id, cancellationToken);

            return await _repository.Delete(id, cancellationToken);
        }
    }
}

