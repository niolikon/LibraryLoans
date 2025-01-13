using LibraryLoans.Api.BaseClasses;
using LibraryLoans.Core.Dtos;
using LibraryLoans.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryLoans.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : BaseController<int, MemberCreateUpdateDto, MemberDetailsDto>
{
    public MembersController(IMemberService service) : base(service) {}

    [HttpPost]
    public async override Task<IActionResult> CreateSingle([FromBody] MemberCreateUpdateDto dto)
    {
        dto.MembershipDate = DateTime.Now;
        return await base.CreateSingle(dto);
    }
}
