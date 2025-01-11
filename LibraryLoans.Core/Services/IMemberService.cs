using LibraryLoans.Core.BaseInterfaces;
using LibraryLoans.Core.Dtos;

namespace LibraryLoans.Core.Services;

public interface IMemberService : IBaseService<int, MemberCreateUpdateDto, MemberDetailsDto>
{
}
