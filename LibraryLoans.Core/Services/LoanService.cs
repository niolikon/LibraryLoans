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
}
