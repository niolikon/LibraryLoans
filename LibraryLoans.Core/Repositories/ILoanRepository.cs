﻿using LibraryLoans.Core.Commons;
using LibraryLoans.Core.Entities;

namespace LibraryLoans.Core.Repositories;

public interface ILoanRepository : ICrudRepository<Loan, int>
{
    Task<bool> IsBookAvailableForLoan(int bookId);

    Task<Loan?> GetActiveLoanForBook(int bookId);

    Task<List<Loan>> GetActiveLoansForMember(int memberId);
}
