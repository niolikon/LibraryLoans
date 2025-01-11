using LibraryLoans.Core.Dtos;
using LibraryLoans.Core.Entities;
using LibraryLoans.Core.Repositories;
using LibraryLoans.Infrastructure.BaseClasses;

namespace LibraryLoans.Infrastructure.Repositories;

public class LoanRepository(AppDbContext dbContext) : BaseRepository<Loan, int>(dbContext), ILoanRepository 
{
    public async Task<Loan?> GetActiveLoanForBook(int bookId)
    {
        return null;
    }
}
