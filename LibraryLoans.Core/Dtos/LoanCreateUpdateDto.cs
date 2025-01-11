using LibraryLoans.Core.BaseClasses;
using System.ComponentModel.DataAnnotations;

namespace LibraryLoans.Core.Dtos;

public class LoanCreateUpdateDto : BaseCreateUpdateDto
{
    [Required]
    public int BookId { get; set; }

    [Required]
    public int MemberId { get; set; }

    public DateTime LoanDate { get; set; }

    public DateTime ReturnDate { get; set; }
}
