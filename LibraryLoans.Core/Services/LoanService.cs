using LibraryLoans.Core.BaseClasses;
using LibraryLoans.Core.Dtos;
using LibraryLoans.Core.Entities;
using LibraryLoans.Core.Exceptions;
using LibraryLoans.Core.Mappers;
using LibraryLoans.Core.Repositories;

namespace LibraryLoans.Core.Services;

public class LoanService : BaseService<Loan, int, LoanCreateUpdateDto, LoanDetailsDto>, ILoanService
{
    protected new ILoanRepository _repository;
    protected new ILoanMapper _mapper;

    public LoanService(ILoanRepository repository, ILoanMapper mapper) : base(repository, mapper) 
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<LoanDetailsDto>> GetLoansByBookId(int bookId)
    {
        IEnumerable<Loan> loans = await _repository.FindAll(loan => loan.BookId == bookId);

        return loans.Select(loan => _mapper.MapEntityToDetailsDto(loan));
    }

    public async override Task<LoanDetailsDto> CreateAsync(LoanCreateUpdateDto dto)
    {
        bool bookIsNotAvailable = ! (await _repository.IsBookAvailableForLoan(dto.BookId));
        if (bookIsNotAvailable)
        {
            throw new ConflictRestException("The book is currently not available");
        }

        Loan? activeLoan = await _repository.GetActiveLoanForBook(dto.BookId);
        bool bookIsNotLendable = (activeLoan != null);
        if (bookIsNotLendable)
        {
            throw new ConflictRestException("The book is currently not lendable");
        }

        Loan model = _mapper.MapCreateUpdateDtoToEntity(dto);
        model.LoanDate = DateTime.Now;

        return _mapper.MapEntityToDetailsDto(await _repository.CreateAsync(model));
    }

    public async override Task<LoanDetailsDto> UpdateAsync(int id, LoanCreateUpdateDto dto)
    {
        Loan? entity = await _repository.GetActiveLoanForBook(dto.BookId);

        bool bookNotReserved = (entity == null);
        if (bookNotReserved)
        {
            throw new NotFoundRestException("The book has not been reserved");
        }

        bool bookReservedByAnotherMember = (entity.MemberId != dto.MemberId);
        if (bookReservedByAnotherMember)
        {
            throw new ConflictRestException("The book has not been reserved by you");
        }

        Loan updatedEntity = entity;
        updatedEntity.Id = id;
        updatedEntity.ReturnDate = DateTime.Now;

        await _repository.UpdateAsync(updatedEntity);

        return _mapper.MapEntityToDetailsDto(updatedEntity);
    }
}
