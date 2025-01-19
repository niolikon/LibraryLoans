using LibraryLoans.Core.Commons;
using LibraryLoans.Core.Dtos;

namespace LibraryLoans.Core.Services;

public interface IMemberService : ICrudService<int, MemberCreateUpdateDto, MemberDetailsDto>
{
}
