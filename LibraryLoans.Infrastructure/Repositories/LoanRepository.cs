using LibraryLoans.Core.Entities;
using LibraryLoans.Core.Repositories;
using LibraryLoans.Infrastructure.Commons;
using Microsoft.EntityFrameworkCore;

namespace LibraryLoans.Infrastructure.Repositories;

public class LoanRepository : BaseCrudRepository<Loan, int>, ILoanRepository 
{
    public LoanRepository(AppDbContext dbContext) : base(dbContext) { }


    public async Task<bool> IsBookAvailableForLoan(int bookId)
    {
        IQueryable<Book> books = _dbContext.Set<Book>();

        Book? bookToCheck = await books
            .Where(book => book.Id == bookId)
            .FirstOrDefaultAsync();

        if (bookToCheck == null || !bookToCheck.IsAvailable)
        {
            return false;
        }

        return true;
    }

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

    public async Task<List<Loan>> GetActiveLoansForMember(int memberId)
    {
        IQueryable<Loan> loans = _dbContext.Set<Loan>();

        return await (from loan in loans
                      where loan.MemberId == memberId
                      where loan.LoanDate != default
                      where loan.ReturnDate == default
                      select loan).ToListAsync();
    }
}
