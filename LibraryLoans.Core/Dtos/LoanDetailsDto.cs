using LibraryLoans.Core.Commons;
using System.Text.Json.Serialization;

namespace LibraryLoans.Core.Dtos;

public class LoanDetailsDto : BaseDetailsDto<int>
{
    public int BookId {  get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public BookDetailsDto Book { get; set; }

    public int MemberId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MemberDetailsDto Member { get; set; }

    public DateTime LoanDate { get; set; }

    public DateTime ReturnDate { get; set; }
}
