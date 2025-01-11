using LibraryLoans.Core.BaseInterfaces;
using LibraryLoans.Core.Entities;

namespace LibraryLoans.Core.Repositories;

public interface ILoanRepository : IBaseRepository<Loan, int>
{
    Task<Loan?> GetActiveLoanForBook(int bookId);
}
