using System.ComponentModel.DataAnnotations;

namespace LibraryLoans.Core.BaseClasses;

public class BaseDetailsDto<TId>
{
    [Required]
    public required TId Id { get; set; }
}
