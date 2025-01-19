using LibraryLoans.Core.Commons;
using LibraryLoans.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LibraryLoans.Api.Commons;

public class BaseCrudController<TId, TCreateUpdateDto, TDetailsDto>(ICrudService<TId, TCreateUpdateDto, TDetailsDto> service) : ControllerBase 
    where TCreateUpdateDto : BaseCreateUpdateDto
    where TDetailsDto : BaseDetailsDto<TId>
{

    [HttpPost]
    public async virtual Task<IActionResult> CreateSingle([FromBody] TCreateUpdateDto dto)
    {
        if (! ModelState.IsValid)
        {
            throw new MalformedRestException("Could not process given dto");
        }

        TDetailsDto dtoCreated = await service.CreateAsync(dto);
        return CreatedAtAction(
            nameof(GetSingle), 
            new {id = dtoCreated.Id}, 
            dtoCreated
        );
    }

    [HttpGet("{id}")]
    public async virtual Task<IActionResult> GetSingle(TId id)
    {
        TDetailsDto dto = await service.ReadAsync(id);
        return Ok(dto);
    }

    [HttpGet]
    public async virtual Task<IActionResult> GetAll()
    {
        IEnumerable<TDetailsDto> dtos = await service.ReadAllAsync();
        return Ok(dtos);
    }

    [HttpPut("{id}")]
    public async virtual Task<IActionResult> UpdateSingle(TId id, [FromBody] TCreateUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            throw new MalformedRestException("Could not process given dto");
        }

        TDetailsDto updatedDto = await service.UpdateAsync(id, dto);
        return Ok(updatedDto);
    }

    [HttpDelete("{id}")]
    public async virtual Task<IActionResult> DeleteSingle(TId id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}