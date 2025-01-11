using FluentAssertions;
using LibraryLoans.Api.Controllers;
using LibraryLoans.Core.Dtos;
using LibraryLoans.Core.Exceptions;
using LibraryLoans.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LibraryLoans.Tests.Controllers;

public class BooksControllerTest
{
    private readonly Mock<IBookService> _bookServiceMock;
    private readonly Mock<ILoanService> _loanServiceMock;
    private readonly BooksController _bookController;

    public BooksControllerTest()
    {
        _bookServiceMock = new Mock<IBookService>();
        _loanServiceMock = new Mock<ILoanService>();
        _bookController = new BooksController(_bookServiceMock.Object, _loanServiceMock.Object);
    }

    [Fact]
    [Trait("Story", "LIB1")]
    public async Task Given_LoanForCorrectBook_When_MemberRequestsReservation_Then_BookIsReserved()
    {
        int bookId = 1;
        int memberId = 1;
        LoanCreateUpdateDto loan = new LoanCreateUpdateDto() { BookId = bookId, MemberId = memberId };
        LoanDetailsDto loanCreated = new LoanDetailsDto() { Id = 1, BookId = bookId, MemberId = memberId, LoanDate = DateTime.Now };
        _loanServiceMock.Setup(loanService => loanService.CreateAsync(loan)).ReturnsAsync(loanCreated);

        IActionResult result = await _bookController.ReserveBook(bookId, loan);

        _loanServiceMock.Verify(loanService => loanService.CreateAsync(loan), Times.Once());
    }

    [Fact]
    [Trait("Story", "LIB1")]
    public async Task Given_LoanForWrongBook_When_MemberRequestsReservation_Then_BookReservationFailsWithMalformed()
    {
        int bookId = 1;
        int memberId = 1;
        int wrongBookId = 2;
        LoanCreateUpdateDto loan = new LoanCreateUpdateDto() { BookId = bookId, MemberId = memberId };

        Func<Task> bookReservation = async () => await _bookController.ReserveBook(wrongBookId, loan);

        await bookReservation.Should().ThrowAsync<MalformedRestException>();
        _loanServiceMock.Verify(loanService => loanService.CreateAsync(loan), Times.Never());
    }

    [Fact]
    [Trait("Story", "LIB1")]
    public async Task Given_LoanForCorrectBook_When_MemberRequestsReturn_Then_BookIsReturned()
    {
        int bookId = 1;
        int memberId = 1;
        int loanId = 1;
        LoanCreateUpdateDto loan = new LoanCreateUpdateDto() { BookId = bookId, MemberId = memberId };

        IActionResult result = await _bookController.ReturnBook(bookId, loanId, loan);

        _loanServiceMock.Verify(loanService => loanService.UpdateAsync(loanId, loan), Times.Once());
    }

    [Fact]
    [Trait("Story", "LIB1")]
    public async Task Given_LoanForWrongBook_When_MemberRequestsReturn_Then_BookReturnFailsWithMalformed()
    {
        int bookId = 1;
        int memberId = 1;
        int loanId = 1;
        int wrongBookId = 2;
        LoanCreateUpdateDto loan = new LoanCreateUpdateDto() { BookId = wrongBookId, MemberId = memberId };

        Func<Task> bookReturn = async () => await _bookController.ReturnBook(bookId, loanId, loan);

        await bookReturn.Should().ThrowAsync<MalformedRestException>();
        _loanServiceMock.Verify(loanService => loanService.UpdateAsync(loanId, loan), Times.Never());
    }
}
