namespace LibraryLoans.Core.Commons;

public interface IMapper<TEntity, TId, TCreateUpdateDto, TDetailsDto>
    where TEntity : class
    where TCreateUpdateDto : BaseCreateUpdateDto
    where TDetailsDto : BaseDetailsDto<TId>
{
    TEntity MapCreateUpdateDtoToEntity(TCreateUpdateDto dto);

    TCreateUpdateDto MapEntityToCreateUpdateDto(TEntity entity);

    TDetailsDto MapEntityToDetailsDto(TEntity entity);
}
