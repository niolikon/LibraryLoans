using LibraryLoans.Core.BaseInterfaces;
using LibraryLoans.Core.Dtos;
using LibraryLoans.Core.Entities;

namespace LibraryLoans.Core.Mappers;

public interface ILoanMapper : IBaseMapper<Loan, int, LoanCreateUpdateDto, LoanDetailsDto>
{
}
