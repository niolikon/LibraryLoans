using LibraryLoans.Core.BaseClasses;
using System.Reflection.Metadata.Ecma335;

namespace LibraryLoans.Core.Entities;

public class Book : BaseEntity<int>
{

    public required string Title { get; set; }

    public required string Author { get; set; }

    public required string ISBN { get; set; }

    public required bool IsAvailable {  get; set; }

    public override void CopyFrom(BaseEntity<int> other)
    {
        if (other == null)
        {
            return;
        }

        if (other is Book b)
        {
            this.IsAvailable = b.IsAvailable;

            if (! string.IsNullOrEmpty(b.Title))
            {
                this.Title = b.Title;
            }
            if (! string.IsNullOrEmpty(b.Author))
            {
                this.Author = b.Author;
            }
            if (! string.IsNullOrEmpty(b.ISBN))
            {
                this.ISBN = b.ISBN;
            }
        }
    }
}
