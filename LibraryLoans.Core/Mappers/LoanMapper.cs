using LibraryLoans.Core.BaseInterfaces;
using LibraryLoans.Core.Dtos;
using LibraryLoans.Core.Entities;

namespace LibraryLoans.Core.Mappers;

public class LoanMapper : ILoanMapper
{
    public Loan MapCreateUpdateDtoToEntity(LoanCreateUpdateDto dto)
    {
        return new Loan() { BookId = dto.BookId, MemberId = dto.MemberId, LoanDate = dto.LoanDate, ReturnDate = dto.ReturnDate };
    }

    public LoanCreateUpdateDto MapEntityToCreateUpdateDto(Loan entity)
    {
        return new LoanCreateUpdateDto() { BookId = entity.BookId, MemberId = entity.MemberId, LoanDate = entity.LoanDate, ReturnDate = entity.ReturnDate };
    }

    public LoanDetailsDto MapEntityToDetailsDto(Loan entity)
    {
        return new LoanDetailsDto() { Id = entity.Id, BookId = entity.BookId, MemberId = entity.MemberId, LoanDate = entity.LoanDate, ReturnDate = entity.ReturnDate };
    }
}
