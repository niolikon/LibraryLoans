using LibraryLoans.Core.Entities;
using LibraryLoans.Core.Repositories;
using LibraryLoans.Infrastructure.BaseClasses;
using Microsoft.EntityFrameworkCore;

namespace LibraryLoans.Infrastructure.Repositories;

public class LoanRepository : BaseRepository<Loan, int>, ILoanRepository 
{
    public LoanRepository(AppDbContext dbContext) : base(dbContext) { }

    public async Task<Loan?> GetActiveLoanForBook(int bookId)
    {
        IQueryable<Loan> loans = _dbContext.Set<Loan>();

        Loan? active = await (from loan in loans
                              where loan.BookId == bookId
                              where loan.LoanDate != default
                              where loan.ReturnDate == default
                              select loan).FirstOrDefaultAsync();

        return active;
    }
}
