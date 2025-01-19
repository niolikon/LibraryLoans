using LibraryLoans.Core.Commons;
using System.ComponentModel.DataAnnotations;

namespace LibraryLoans.Core.Dtos;

public class BookDetailsDto : BaseDetailsDto<int>
{
    [Required]
    public required string Title { get; set; }

    [Required]
    public required string Author { get; set; }

    [Required]
    public required string ISBN { get; set; }

    public bool IsAvailable {  get; set; }
}
