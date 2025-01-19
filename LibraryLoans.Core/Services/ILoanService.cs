using LibraryLoans.Core.Commons;
using LibraryLoans.Core.Dtos;

namespace LibraryLoans.Core.Services;

public interface ILoanService: ICrudService<int, LoanCreateUpdateDto, LoanDetailsDto>
{
    Task<IEnumerable<LoanDetailsDto>> GetLoansByBookId(int bookId);
}
