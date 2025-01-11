using LibraryLoans.Core.BaseInterfaces;
using LibraryLoans.Core.Dtos;
using LibraryLoans.Core.Entities;

namespace LibraryLoans.Core.Mappers;

public class MemberMapper : IMemberMapper
{
    public Member MapCreateUpdateDtoToEntity(MemberCreateUpdateDto dto)
    {
        return new Member()
        {
            Name = dto.Name,
            Email = dto.Email,
            MembershipDate = dto.MembershipDate
        };
    }

    public MemberCreateUpdateDto MapEntityToCreateUpdateDto(Member entity)
    {
        return new MemberCreateUpdateDto()
        {
            Name = entity.Name,
            Email = entity.Email,
            MembershipDate = entity.MembershipDate
        };
    }

    public MemberDetailsDto MapEntityToDetailsDto(Member entity)
    {
        return new MemberDetailsDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            MembershipDate = entity.MembershipDate,
            Loans = entity.Loans.Select(loan =>
                new LoanDetailsDto()
                {
                    Id = loan.Id,
                    BookId = loan.BookId,
                    MemberId = loan.MemberId,
                    LoanDate = loan.LoanDate,
                    ReturnDate = loan.ReturnDate
                })
                .ToList(),
        };
    }
}
