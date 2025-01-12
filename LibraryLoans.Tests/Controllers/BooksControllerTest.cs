using FluentAssertions;
using Moq;
using Microsoft.AspNetCore.Mvc;
using LibraryLoans.Api.Controllers;
using LibraryLoans.Core.Dtos;
using LibraryLoans.Core.Exceptions;
using LibraryLoans.Core.Services;

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
    public async Task Given_CorrectRestRequestForm_When_MemberRequestsReservation_Then_BookReservationIsRelayed()
    {
        int bookId = 1;
        LoanCreateUpdateDto loan = new LoanCreateUpdateDto() { BookId = bookId, MemberId = 2 };
        LoanDetailsDto loanCreated = new LoanDetailsDto() { Id = 1, BookId = loan.BookId, MemberId = loan.MemberId, LoanDate = DateTime.Now };
        _loanServiceMock.Setup(loanService => loanService.CreateAsync(loan)).ReturnsAsync(loanCreated);

        IActionResult result = await _bookController.ReserveBook(bookId, loan);

        _loanServiceMock.Verify(loanService => loanService.CreateAsync(loan), Times.Once());
    }

    [Fact]
    [Trait("Story", "LIB1")]
    public async Task Given_WrongBookRestRequestForm_When_MemberRequestsReservation_Then_BookReservationFailsWithMalformed()
    {
        int bookId = 1;
        int bookIdAnother = 2;
        LoanCreateUpdateDto loan = new LoanCreateUpdateDto() { BookId = bookId, MemberId = 1 };

        Func<Task> bookReservation = async () => await _bookController.ReserveBook(bookIdAnother, loan);

        await bookReservation.Should().ThrowAsync<MalformedRestException>();
        _loanServiceMock.Verify(loanService => loanService.CreateAsync(loan), Times.Never());
    }

    [Fact]
    [Trait("Story", "LIB1")]
    public async Task Given_CorrectRestRequestForm_When_MemberRequestsReturn_Then_BookReturnIsRelayed()
    {
        int bookId = 1;
        int loanId = 1;
        LoanCreateUpdateDto loan = new LoanCreateUpdateDto() { BookId = bookId, MemberId = 1 };

        IActionResult result = await _bookController.ReturnBook(bookId, loanId, loan);

        _loanServiceMock.Verify(loanService => loanService.UpdateAsync(loanId, loan), Times.Once());
    }

    [Fact]
    [Trait("Story", "LIB1")]
    public async Task Given_WrongBookRestRequestForm_When_MemberRequestsReturn_Then_BookReturnFailsWithMalformed()
    {
        int bookId = 1;
        int loanId = 1;
        int bookIdAnother = 2;
        LoanCreateUpdateDto loan = new LoanCreateUpdateDto() { BookId = bookId, MemberId = 1 };

        Func<Task> bookReturn = async () => await _bookController.ReturnBook(bookIdAnother, loanId, loan);

        await bookReturn.Should().ThrowAsync<MalformedRestException>();
        _loanServiceMock.Verify(loanService => loanService.UpdateAsync(loanId, loan), Times.Never());
    }
}
