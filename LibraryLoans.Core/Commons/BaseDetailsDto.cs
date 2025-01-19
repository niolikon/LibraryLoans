using System.ComponentModel.DataAnnotations;

namespace LibraryLoans.Core.Commons;

public class BaseDetailsDto<TId>
{
    [Required]
    public required TId Id { get; set; }
}
