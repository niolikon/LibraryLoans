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

    public override void CopyFrom(BaseEntity<int> other)
    {
        if (other == null)
        {
            return;
        }

        if (other is Loan l)
        {
            this.BookId = l.BookId;
            this.MemberId = l.MemberId;

            if (l.Book != null)
            {
                this.Book = l.Book;
            }
            if (l.Member != null)
            {
                this.Member = l.Member;
            }
            if (l.LoanDate != DateTime.MinValue)
            {
                this.LoanDate = l.LoanDate;
            }
            if (l.ReturnDate != DateTime.MinValue)
            {
                this.ReturnDate = l.ReturnDate;
            }
        }
    }
}
