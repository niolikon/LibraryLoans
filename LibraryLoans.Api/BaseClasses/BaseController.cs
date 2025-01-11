using LibraryLoans.Core.BaseClasses;
using LibraryLoans.Core.BaseInterfaces;
using LibraryLoans.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LibraryLoans.Api.BaseClasses;

public class BaseController<TId, TCreateUpdateDto, TDetailsDto>(IBaseService<TId, TCreateUpdateDto, TDetailsDto> service) : ControllerBase 
    where TCreateUpdateDto : BaseCreateUpdateDto
    where TDetailsDto : BaseDetailsDto<TId>
{

    [HttpPost]
    public async Task<IActionResult> CreateSingle([FromBody] TCreateUpdateDto dto)
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
    public async Task<IActionResult> GetSingle(TId id)
    {
        TDetailsDto dto = await service.GetAsync(id);
        return Ok(dto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<TDetailsDto> dtos = await service.GetAllAsync();
        return Ok(dtos);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSingle(TId id, [FromBody] TCreateUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            throw new MalformedRestException("Could not process given dto");
        }

        await service.UpdateAsync(id, dto);
        return Ok(dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSingle(TId id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}