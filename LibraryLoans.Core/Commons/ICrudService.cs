namespace LibraryLoans.Core.Commons;

public interface ICrudService<TId, TCreateUpdateDto, TDetailsDto>
    where TCreateUpdateDto : BaseCreateUpdateDto
    where TDetailsDto : BaseDetailsDto<TId>
{
    Task<TDetailsDto> CreateAsync(TCreateUpdateDto dto);

    Task<TDetailsDto> ReadAsync(TId id);

    Task<IEnumerable<TDetailsDto>> ReadAllAsync();

    Task<TDetailsDto> UpdateAsync(TId id, TCreateUpdateDto dto);

    Task DeleteAsync(TId id);
}
