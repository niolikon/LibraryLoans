using LibraryLoans.Core.BaseInterfaces;
using LibraryLoans.Core.Dtos;
using LibraryLoans.Core.Entities;

namespace LibraryLoans.Core.Mappers;

public interface IMemberMapper : IBaseMapper<Member, int, MemberCreateUpdateDto, MemberDetailsDto>
{
}
