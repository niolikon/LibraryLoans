using LibraryLoans.Core.BaseClasses;

namespace LibraryLoans.Core.Entities;

public class Loan : BaseEntity<int>
{
    public int BookId {  get; set; }

    public Book Book { get; set; }

    public int MemberId { get; set; }

    public Member Member { get; set; }

    public DateTime LoanDate { get; set; }

    public DateTime ReturnDate { get; set; }
}
