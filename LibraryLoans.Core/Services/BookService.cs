using LibraryLoans.Core.BaseClasses;
using LibraryLoans.Core.Dtos;
using LibraryLoans.Core.Entities;
using LibraryLoans.Core.Mappers;
using LibraryLoans.Core.Repositories;

namespace LibraryLoans.Core.Services;

public class BookService : BaseService<Book, int, BookCreateUpdateDto, BookDetailsDto>, IBookService
{
    public BookService(IBookRepository repository, IBookMapper mapper) : base(repository, mapper) { }
}
