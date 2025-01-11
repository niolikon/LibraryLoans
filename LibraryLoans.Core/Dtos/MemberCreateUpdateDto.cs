using LibraryLoans.Core.BaseClasses;
using System.ComponentModel.DataAnnotations;

namespace LibraryLoans.Core.Dtos;

public class MemberCreateUpdateDto : BaseCreateUpdateDto
{
    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Email { get; set; }

    public DateTime MembershipDate { get; set; }
}
