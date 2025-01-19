using LibraryLoans.Core.Commons;
using LibraryLoans.Core.Dtos;
using LibraryLoans.Core.Entities;
namespace LibraryLoans.Core.Mappers;

public interface IBookMapper : IMapper<Book, int, BookCreateUpdateDto, BookDetailsDto>
{
}
