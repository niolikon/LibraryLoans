using LibraryLoans.Core.BaseInterfaces;
using LibraryLoans.Core.Dtos;

namespace LibraryLoans.Core.Services;

public interface IBookService : IBaseService<int, BookCreateUpdateDto, BookDetailsDto>
{
}
