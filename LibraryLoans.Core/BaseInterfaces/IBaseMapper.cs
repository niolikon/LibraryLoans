using LibraryLoans.Core.BaseClasses;

namespace LibraryLoans.Core.BaseInterfaces;

public interface IBaseMapper<TEntity, TId, TCreateUpdateDto, TDetailsDto> 
    where TEntity: class 
    where TCreateUpdateDto: BaseCreateUpdateDto 
    where TDetailsDto: BaseDetailsDto<TId>
{
    TEntity MapCreateUpdateDtoToEntity(TCreateUpdateDto dto);

    TCreateUpdateDto MapEntityToCreateUpdateDto(TEntity entity);

    TDetailsDto MapEntityToDetailsDto(TEntity entity);
}
