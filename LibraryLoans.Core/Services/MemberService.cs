using LibraryLoans.Core.BaseClasses;
using LibraryLoans.Core.Dtos;
using LibraryLoans.Core.Entities;
using LibraryLoans.Core.Mappers;
using LibraryLoans.Core.Repositories;

namespace LibraryLoans.Core.Services;

public class MemberService : BaseService<Member, int, MemberCreateUpdateDto, MemberDetailsDto>, IMemberService
{
    public MemberService(IMemberRepository repository, IMemberMapper mapper) : base(repository, mapper) { }
}
