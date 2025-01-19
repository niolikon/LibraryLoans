using LibraryLoans.Core.Commons;
using LibraryLoans.Core.Dtos;
using LibraryLoans.Core.Entities;
using LibraryLoans.Core.Exceptions;
using LibraryLoans.Core.Mappers;
using LibraryLoans.Core.Repositories;

namespace LibraryLoans.Core.Services;

public class MemberService : BaseCrudService<Member, int, MemberCreateUpdateDto, MemberDetailsDto>, IMemberService
{
    private ILoanRepository _loanRepository;

    public MemberService(IMemberRepository repository, IMemberMapper mapper, ILoanRepository loanRepository) : base(repository, mapper) 
    {
        _loanRepository = loanRepository;
    }

    public async override Task<MemberDetailsDto> ReadAsync(int id)
    {
        Member? entity = await _repository.ReadAsync(id);

        if (entity == null)
        {
            throw new NotFoundRestException("Could not fetch entity with specified id");
        }

        entity.Loans = await _loanRepository.GetActiveLoansForMember(id);

        return _mapper.MapEntityToDetailsDto(entity);
    }
}
