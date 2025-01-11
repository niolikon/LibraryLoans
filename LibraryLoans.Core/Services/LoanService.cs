using LibraryLoans.Core.BaseClasses;
using LibraryLoans.Core.Dtos;
using LibraryLoans.Core.Entities;
using LibraryLoans.Core.Mappers;
using LibraryLoans.Core.Repositories;

namespace LibraryLoans.Core.Services;

public class LoanService : BaseService<Loan, int, LoanCreateUpdateDto, LoanDetailsDto>, ILoanService
{
    private ILoanRepository _repository;

    private ILoanMapper _mapper;

    public LoanService(ILoanRepository repository, ILoanMapper mapper) : base(repository, mapper) 
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<LoanDetailsDto>> GetLoansByBookId(int bookId)
    {
        IEnumerable<Loan> loans = await _repository.FindAll( loan => loan.BookId == bookId );

        return loans.Select(loan => _mapper.MapEntityToDetailsDto(loan));
    }
}
