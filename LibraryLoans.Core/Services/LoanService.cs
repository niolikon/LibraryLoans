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
        Loan? active = await _repository.GetActiveLoanForBook(dto.BookId);
        if (active != null)
        {
            throw new ConflictRestException("Could not reserve a book already reserved");
        }

        Loan model = _mapper.MapCreateUpdateDtoToEntity(dto);
        model.LoanDate = DateTime.Now;

        return _mapper.MapEntityToDetailsDto(await _repository.CreateAsync(model));
    }

    public async override Task UpdateAsync(int id, LoanCreateUpdateDto dto)
    {
        Loan? entity = await _repository.GetActiveLoanForBook(dto.BookId);

        if (entity == null)
        {
            throw new NotFoundRestException("Could not find entity with specified id");
        }
        else if (entity.Id != id)
        {
            throw new NotFoundRestException("Could not find entity with specified id");
        }
        else if (entity.MemberId != dto.MemberId)
        {
            throw new ConflictRestException("Could not return a book reserved by another member");
        }

        Loan updatedEntity = entity;
        updatedEntity.Id = id;
        updatedEntity.ReturnDate = DateTime.Now;

        await _repository.UpdateAsync(updatedEntity);
    }
}
