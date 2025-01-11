using LibraryLoans.Api.BaseClasses;
using LibraryLoans.Core.Dtos;
using LibraryLoans.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryLoans.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : BaseController<int, BookCreateUpdateDto, BookDetailsDto>
{
    private readonly IBookService _bookService;

    private readonly ILoanService _loanService;

    public BooksController(IBookService bookService, ILoanService loanService) : base(bookService) 
    {
        _bookService = bookService;
        _loanService = loanService;
    }
}
