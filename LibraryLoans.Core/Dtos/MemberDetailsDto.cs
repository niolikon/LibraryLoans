using LibraryLoans.Core.BaseClasses;
using System.ComponentModel.DataAnnotations;

namespace LibraryLoans.Core.Dtos;

public class MemberDetailsDto : BaseDetailsDto<int>
{
    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Email { get; set; }

    public DateTime MembershipDate { get; set; }

    public List<LoanDetailsDto> Loans { get; set; }
}
