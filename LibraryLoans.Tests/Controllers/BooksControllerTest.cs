using FluentAssertions;
using LibraryLoans.Api.Controllers;
using LibraryLoans.Core.Dtos;
using LibraryLoans.Core.Exceptions;
using LibraryLoans.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using Moq;

namespace LibraryLoans.Tests.Controllers;

public class BooksControllerTest
{
    private Mock<IBookService> _bookServiceMock;
    private Mock<ILoanService> _loanServiceMock;
    private BooksController _bookController;

    public BooksControllerTest()
    {
        _bookServiceMock = new Mock<IBookService>();
        _loanServiceMock = new Mock<ILoanService>();
        _bookController = new BooksController(_bookServiceMock.Object, _loanServiceMock.Object);
    }
}
