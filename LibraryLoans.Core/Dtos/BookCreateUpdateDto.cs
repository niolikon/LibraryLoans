using LibraryLoans.Core.BaseClasses;
using System.ComponentModel.DataAnnotations;

namespace LibraryLoans.Core.Dtos;

public class BookCreateUpdateDto : BaseCreateUpdateDto
{
    [Required]
    public required string Title { get; set; }

    [Required]
    public required string Author { get; set; }

    [Required]
    public required string ISBN { get; set; }

    public bool IsAvailable {  get; set; }
}
