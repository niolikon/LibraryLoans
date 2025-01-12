using LibraryLoans.Api.BaseClasses;
using LibraryLoans.Core.Dtos;
using LibraryLoans.Core.Exceptions;
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
	
    [HttpGet("{bookId}/loans")]
    public async Task<IActionResult> GetLoanHistory(int bookId)
    {
        IEnumerable<LoanDetailsDto> loans = await _loanService.GetLoansByBookId(bookId);
        return Ok(loans);
    }

    [HttpGet("{bookId}/loans/{loanId}")]
    public async Task<IActionResult> GetLoan(int loanId)
    {
        LoanDetailsDto loanInDb = await _loanService.GetAsync(loanId);

        return Ok(loanInDb);
    }

    [HttpPost("{bookId}/loans")]
    public async Task<IActionResult> ReserveBook(int bookId, [FromBody] LoanCreateUpdateDto loan)
    {
        if (!ModelState.IsValid)
        {
            throw new MalformedRestException("Could not process given dto");
        }

        if (bookId != loan.BookId)
        {
            throw new MalformedRestException("Could not reserve a book using another book");
        }

        LoanDetailsDto loanInDb = await _loanService.CreateAsync(loan);

        return CreatedAtAction(
            nameof(GetLoan),
            new
            {
                bookId = loanInDb.BookId,
                loanId = loanInDb.Id
            },
            loanInDb
        );
    }

    [HttpPut("{bookId}/loans/{loanId}")]
    public async Task<IActionResult> ReturnBook(int bookId, int loanId, [FromBody] LoanCreateUpdateDto loan)
    {
        if (!ModelState.IsValid)
        {
            throw new MalformedRestException("Could not process given dto");
        }

        if (bookId != loan.BookId)
        {
            throw new MalformedRestException("Could not reserve a book using another book");
        }

        await _loanService.UpdateAsync(loanId, loan);

        return Ok();
    }
}
