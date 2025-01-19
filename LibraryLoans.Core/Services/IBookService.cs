using LibraryLoans.Core.Commons;
using LibraryLoans.Core.Dtos;

namespace LibraryLoans.Core.Services;

public interface IBookService : ICrudService<int, BookCreateUpdateDto, BookDetailsDto>
{
}
