using AutoMapper;
using Box.Contracts.DTO;
using Box.Infrastructure.DataAccess.Interfaces;
using Box.Applicaton.Exceptions;
using Box.Applicaton.Interface;
using Box.Applicaton.BackgroundServices.Data;

namespace Box.Applicaton.Service
{ 
    using Box.Domain.Models;

    /// <inheritdoc cref="IBoxService"/>
    public class BoxService : IBoxService
    {
        private readonly IBoxRepository _repository;
        private readonly IMapper _mapper;
        /// <summary>
        /// Инициализирует сервис по работе с коробками
        /// </summary>
        /// <param name="boxRepository">Репозиторий для работы с коробками.</param>
        /// <param name="mapper">Объект для маппинга между моделями.</param>
        public BoxService(IBoxRepository boxRepository, 
                        IMapper mapper)
        {
            _repository = boxRepository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public Task<BoxDto[]> GetAll(CancellationToken cancellationToken)
        {
            if (Guid.TryParse(UserContext.UserId, out Guid parsedGuid))
            {
                return _repository.GetAll(parsedGuid,cancellationToken);
            }
            else
            {
                throw new InvalidBoxException("Невалидный IdUser");
            }
        }

        /// <inheritdoc />
        public Task<BoxDto?> GetById(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetById(id, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<BoxDto> Add(ShortBoxDto dto, CancellationToken cancellationToken)
        {
            if (await _repository.FindWhere(x => x.Name == dto.Name, cancellationToken) is not null)
            {
                throw new InvalidBoxException("Выберите другое название коробки, чтобы не запутаться");
            }
            var box = _mapper.Map<Box>(dto);
            if (Guid.TryParse(UserContext.UserId, out Guid parsedGuid))
            {
                box.IdUser = parsedGuid;
                return await _repository.Add(box, cancellationToken);
            }
            else
            {
                throw new InvalidBoxException("Невалидный IdUser");
            } 
        }

        /// <inheritdoc />
        public async Task<BoxDto> Update(Guid id,ShortBoxDto dto, CancellationToken cancellationToken)
        {
            var existEntity = await GetById(id, cancellationToken);
            if (existEntity is null) throw new InvalidBoxException($"Коробка с Id:{id} не найдена");

            var entity = _mapper.Map<ShortBoxDto, Box>(dto);
            entity.Id = id;

            await _repository.Update(entity, cancellationToken);

            return _mapper.Map<BoxDto>(entity);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
        {
            var existEntity = await GetById(id, cancellationToken);

            return await _repository.Delete(id, cancellationToken);
        }
    }
}
