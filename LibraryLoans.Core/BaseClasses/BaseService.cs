using LibraryLoans.Core.BaseInterfaces;
using LibraryLoans.Core.Exceptions;

namespace LibraryLoans.Core.BaseClasses;

public class BaseService<TEntity, TId, TCreateUpdateDto, TDetailsDto> : IBaseService<TId, TCreateUpdateDto, TDetailsDto> 
    where TEntity : BaseEntity<TId> 
    where TCreateUpdateDto : BaseCreateUpdateDto
    where TDetailsDto : BaseDetailsDto<TId>
{
    protected IBaseRepository<TEntity, TId> _repository;
    protected IBaseMapper<TEntity, TId, TCreateUpdateDto, TDetailsDto> _mapper;

    public BaseService(IBaseRepository<TEntity, TId> repository, 
        IBaseMapper<TEntity, TId, TCreateUpdateDto, TDetailsDto> mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async virtual Task<TDetailsDto> CreateAsync(TCreateUpdateDto dto)
    {
        TEntity model = _mapper.MapCreateUpdateDtoToEntity(dto);

        return _mapper.MapEntityToDetailsDto(await _repository.CreateAsync(model));
    }

    public async virtual Task<IEnumerable<TDetailsDto>> GetAllAsync()
    {
        IEnumerable<TEntity> entities = await _repository.GetAllAsync();

        return entities.Select(_mapper.MapEntityToDetailsDto);
    }

    public async virtual Task<TDetailsDto> GetAsync(TId id)
    {
        TEntity? entity = await _repository.GetAsync(id);

        if (entity == null)
        {
            throw new NotFoundRestException("Could not fetch entity with specified id");
        }

        return _mapper.MapEntityToDetailsDto(entity);
    }

    public async virtual Task UpdateAsync(TId id, TCreateUpdateDto dto)
    {
        TEntity updatedEntity = _mapper.MapCreateUpdateDtoToEntity(dto);
        updatedEntity.Id = id;

        try
        {
            await _repository.UpdateAsync(updatedEntity);
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
