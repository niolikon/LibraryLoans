using LibraryLoans.Core.Services;
using LibraryLoans.Core.Dtos;
using LibraryLoans.Core.Entities;
using LibraryLoans.Core.Mappers;
using LibraryLoans.Core.Repositories;
using Moq;
using FluentAssertions;
using LibraryLoans.Core.Exceptions;

namespace LibraryLoans.Tests.Services;

public class LoanServiceTest
{
    private readonly Mock<ILoanRepository> _repositoryMock;
    private readonly Mock<ILoanMapper> _mapperMock;
    private readonly LoanService _loanService;

    public LoanServiceTest()
    {
        _repositoryMock = new Mock<ILoanRepository>();
        _mapperMock = new Mock<ILoanMapper>();
        _loanService = new LoanService(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    [Trait("Story", "LIB1")]
    public async Task Given_BookNotReserved_When_MemberRequestsReservation_Then_BookIsReserved()
    {
        int bookId = 1;
        int memberId = 1;
        LoanCreateUpdateDto loanFromUser = new LoanCreateUpdateDto() { BookId = bookId, MemberId = memberId };
        Loan dummyLoanFromUserAsEntity = new Mock<Loan>().Object;
        Loan loanCreated = new Loan() { Id = 1, BookId = loanFromUser.BookId, MemberId = loanFromUser.MemberId, LoanDate = DateTime.Now };
        LoanDetailsDto dummyLoanCreatedAsDto = new Mock<LoanDetailsDto>().Object;
        _repositoryMock.Setup(repository => repository.GetActiveLoanForBook(loanFromUser.BookId)).ReturnsAsync((Loan?) null);
        _mapperMock.Setup(mapper => mapper.MapCreateUpdateDtoToEntity(loanFromUser)).Returns(dummyLoanFromUserAsEntity);
        _repositoryMock.Setup(repository => repository.CreateAsync(dummyLoanFromUserAsEntity)).ReturnsAsync(loanCreated);
        _mapperMock.Setup(mapper => mapper.MapEntityToDetailsDto(loanCreated)).Returns(dummyLoanCreatedAsDto);

        LoanDetailsDto result = await _loanService.CreateAsync(loanFromUser);

        _repositoryMock.Verify(repository => repository.GetActiveLoanForBook(bookId), Times.Once);
        _repositoryMock.Verify(repository => repository.CreateAsync(dummyLoanFromUserAsEntity), Times.Once);
        result.Should().Be(dummyLoanCreatedAsDto);
    }

    [Fact]
    [Trait("Story", "LIB1")]
    public async Task Given_BookReserved_When_MemberRequestsReservation_Then_BookIsNotReserved()
    {
        int bookId = 1;
        int memberId = 1;
        int MemberIdAnother = 5;
        LoanCreateUpdateDto loanFromUser = new LoanCreateUpdateDto() { BookId = bookId, MemberId = memberId };
        Loan loanActiveForBook = new Loan() { BookId = loanFromUser.BookId, MemberId = MemberIdAnother, LoanDate = DateTime.Now.AddDays(-7) };
        Loan dummyLoanFromUserAsEntity = new Mock<Loan>().Object;
        _repositoryMock.Setup(repository => repository.GetActiveLoanForBook(loanFromUser.BookId)).ReturnsAsync(loanActiveForBook);
        _mapperMock.Setup(mapper => mapper.MapCreateUpdateDtoToEntity(loanFromUser)).Returns(dummyLoanFromUserAsEntity);
        
        Func<Task> bookReservationForUnavailableBook = async () => await _loanService.CreateAsync(loanFromUser);

        await bookReservationForUnavailableBook.Should().ThrowAsync<ConflictRestException>();
        _repositoryMock.Verify(repository => repository.GetActiveLoanForBook(bookId), Times.Once);
        _repositoryMock.Verify(repository => repository.CreateAsync(It.IsAny<Loan>()), Times.Never);
    }

    [Fact]
    [Trait("Story", "LIB1")]
    public async Task Given_BookReserved_When_MemberRequestsReturn_Then_BookIsReturned()
    {
        int loanId = 1;
        int bookId = 1;
        int memberId = 1;
        LoanCreateUpdateDto loanFromUser = new LoanCreateUpdateDto() { BookId = bookId, MemberId = memberId };
        Loan loanActiveForBook = new Loan() { Id = 1, BookId = loanFromUser.BookId, MemberId = memberId, LoanDate = DateTime.Now.AddDays(-7) };
        _repositoryMock.Setup(repository => repository.GetActiveLoanForBook(loanFromUser.BookId)).ReturnsAsync(loanActiveForBook);
        
        await _loanService.UpdateAsync(loanId, loanFromUser);

        _repositoryMock.Verify(repository => repository.GetActiveLoanForBook(bookId), Times.Once);
        _repositoryMock.Verify(repository => repository.UpdateAsync(loanActiveForBook), Times.Once);
        loanActiveForBook.ReturnDate.Should().BeAfter(DateTime.MinValue);
    }

    [Fact]
    [Trait("Story", "LIB1")]
    public async Task Given_BookNotReserved_When_MemberRequestsReturn_Then_BookIsNotReturned()
    {
        int loanId = 1;
        int bookId = 1;
        int memberId = 1;
        LoanCreateUpdateDto loanFromUser = new LoanCreateUpdateDto() { BookId = bookId, MemberId = memberId };
        _repositoryMock.Setup(repository => repository.GetActiveLoanForBook(loanFromUser.BookId)).ReturnsAsync((Loan?) null);
        
        Func<Task> bookReturnForAvailableBook = async () => await _loanService.UpdateAsync(loanId, loanFromUser);

        await bookReturnForAvailableBook.Should().ThrowAsync<NotFoundRestException>();
        _repositoryMock.Verify(repository => repository.GetActiveLoanForBook(bookId), Times.Once);
        _repositoryMock.Verify(repository => repository.UpdateAsync(It.IsAny<Loan>()), Times.Never);
    }

    [Fact]
    [Trait("Story", "LIB1")]
    public async Task Given_BookReservedFromAnotherMember_When_MemberRequestsReturn_Then_BookIsNotReturned()
    {
        int loanId = 1;
        int bookId = 1;
        int memberId = 1;
        int memberIdAnother = 4;
        LoanCreateUpdateDto loanFromUser = new LoanCreateUpdateDto() { BookId = bookId, MemberId = memberId };
        Loan loanActiveForBook = new Loan() { Id = 1, BookId = loanFromUser.BookId, MemberId = memberIdAnother, LoanDate = DateTime.Now.AddDays(-7) };
        _repositoryMock.Setup(repository => repository.GetActiveLoanForBook(loanFromUser.BookId)).ReturnsAsync(loanActiveForBook);
        
        Func<Task> bookReturnFromDifferentMember = async () => await _loanService.UpdateAsync(loanId, loanFromUser);

        await bookReturnFromDifferentMember.Should().ThrowAsync<ConflictRestException>();
        _repositoryMock.Verify(repository => repository.GetActiveLoanForBook(bookId), Times.Once);
        _repositoryMock.Verify(repository => repository.UpdateAsync(It.IsAny<Loan>()), Times.Never);
    }
}
