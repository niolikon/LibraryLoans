using LibraryLoans.Core.BaseInterfaces;
using LibraryLoans.Core.Entities;

namespace LibraryLoans.Core.Repositories;

public interface ILoanRepository : IBaseRepository<Loan, int>
{
    Task<bool> IsBookAvailableForLoan(int bookId);

    Task<Loan?> GetActiveLoanForBook(int bookId);
}
