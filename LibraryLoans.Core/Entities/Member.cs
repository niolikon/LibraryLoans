using LibraryLoans.Core.BaseClasses;

namespace LibraryLoans.Core.Entities;

public class Member : BaseEntity<int>
{
    public string Name { get; set; }

    public string Email { get; set; }

    public DateTime MembershipDate {  get; set; }

    public List<Loan> Loans { get; set; }
}
