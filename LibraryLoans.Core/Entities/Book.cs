using LibraryLoans.Core.BaseClasses;

namespace LibraryLoans.Core.Entities;

public class Book : BaseEntity<int>
{

    public required string Title { get; set; }

    public required string Author { get; set; }

    public required string ISBN { get; set; }

    public required bool IsAvailable {  get; set; }
}
