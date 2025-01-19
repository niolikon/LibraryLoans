using LibraryLoans.Core.Commons;
using System.Xml.Linq;

namespace LibraryLoans.Core.Entities;

public class Member : BaseEntity<int>
{
    public string Name { get; set; }

    public string Email { get; set; }

    public DateTime MembershipDate {  get; set; }

    public List<Loan> Loans { get; set; }

    public override void CopyFrom(BaseEntity<int> other)
    {
        if (other == null)
        {
            return;
        }

        if (other is Member m)
        {
            if (! string.IsNullOrEmpty(m.Name))
            {
                this.Name = m.Name;
            }
            if (! string.IsNullOrEmpty(m.Email))
            {
                this.Email = m.Email;
            }
            if (m.MembershipDate != DateTime.MinValue)
            {
                this.MembershipDate = m.MembershipDate;
            }
            if (m.Loans != null)
            {
                this.Loans = m.Loans;
            }
        }
    }
}
