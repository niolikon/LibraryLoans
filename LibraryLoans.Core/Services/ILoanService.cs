using LibraryLoans.Core.BaseInterfaces;
using LibraryLoans.Core.Dtos;

namespace LibraryLoans.Core.Services;

public interface ILoanService: IBaseService<int, LoanCreateUpdateDto, LoanDetailsDto>
{
    Task<IEnumerable<LoanDetailsDto>> GetLoansByBookId(int bookId);
}
