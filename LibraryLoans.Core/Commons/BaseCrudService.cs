using LibraryLoans.Core.Exceptions;

namespace LibraryLoans.Core.Commons;

public class BaseCrudService<TEntity, TId, TCreateUpdateDto, TDetailsDto> : ICrudService<TId, TCreateUpdateDto, TDetailsDto> 
    where TEntity : BaseEntity<TId> 
    where TCreateUpdateDto : BaseCreateUpdateDto
    where TDetailsDto : BaseDetailsDto<TId>
{
    protected ICrudRepository<TEntity, TId> _repository;
    protected IMapper<TEntity, TId, TCreateUpdateDto, TDetailsDto> _mapper;

    public BaseCrudService(ICrudRepository<TEntity, TId> repository, 
        IMapper<TEntity, TId, TCreateUpdateDto, TDetailsDto> mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async virtual Task<TDetailsDto> CreateAsync(TCreateUpdateDto dto)
    {
        TEntity model = _mapper.MapCreateUpdateDtoToEntity(dto);

        return _mapper.MapEntityToDetailsDto(await _repository.CreateAsync(model));
    }

    public async virtual Task<IEnumerable<TDetailsDto>> ReadAllAsync()
    {
        IEnumerable<TEntity> entities = await _repository.ReadAllAsync();

        return entities.Select(_mapper.MapEntityToDetailsDto);
    }

    public async virtual Task<TDetailsDto> ReadAsync(TId id)
    {
        TEntity? entity = await _repository.ReadAsync(id);

        if (entity == null)
        {
            throw new NotFoundRestException("Could not fetch entity with specified id");
        }

        return _mapper.MapEntityToDetailsDto(entity);
    }

    public async virtual Task<TDetailsDto> UpdateAsync(TId id, TCreateUpdateDto dto)
    {
        TEntity entityWithNewData = _mapper.MapCreateUpdateDtoToEntity(dto);
        entityWithNewData.Id = id;

        try
        {
            TEntity updatedEntity = await _repository.UpdateAsync(entityWithNewData);
            return _mapper.MapEntityToDetailsDto(updatedEntity);
        }
        catch (EntityNotFoundException)
        {
            throw new NotFoundRestException("Could not find entity with specified id");
        }
    }

    public async virtual Task DeleteAsync(TId id)
    {
        try
        {
            await _repository.DeleteAsync(id);
        }
        catch (EntityNotFoundException)
        {
            throw new NotFoundRestException("Could not delete entity with specified id");
        }
    }
}
