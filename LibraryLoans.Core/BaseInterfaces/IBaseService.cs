using LibraryLoans.Core.BaseClasses;

namespace LibraryLoans.Core.BaseInterfaces;

public interface IBaseService<TId, TCreateUpdateDto, TDetailsDto>
    where TCreateUpdateDto : BaseCreateUpdateDto
    where TDetailsDto : BaseDetailsDto<TId>
{
    Task<TDetailsDto> CreateAsync(TCreateUpdateDto dto);

    Task<TDetailsDto> GetAsync(TId id);

    Task<IEnumerable<TDetailsDto>> GetAllAsync();

    Task<TDetailsDto> UpdateAsync(TId id, TCreateUpdateDto dto);

    Task DeleteAsync(TId id);
}
