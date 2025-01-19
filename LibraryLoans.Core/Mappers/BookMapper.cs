using LibraryLoans.Core.Commons;
using LibraryLoans.Core.Dtos;
using LibraryLoans.Core.Entities;

namespace LibraryLoans.Core.Mappers;

public class BookMapper : IBookMapper
{
    public Book MapCreateUpdateDtoToEntity(BookCreateUpdateDto dto)
    {
        return new Book() { Title = dto.Title, Author = dto.Author, IsAvailable = dto.IsAvailable, ISBN = dto.ISBN };
    }

    public BookCreateUpdateDto MapEntityToCreateUpdateDto(Book entity)
    {
        return new BookCreateUpdateDto() { Title = entity.Title, Author = entity.Author, IsAvailable = entity.IsAvailable, ISBN = entity.ISBN };
    }

    public BookDetailsDto MapEntityToDetailsDto(Book entity)
    {
        return new BookDetailsDto() { Id = entity.Id, Title = entity.Title, Author = entity.Author, IsAvailable = entity.IsAvailable, ISBN = entity.ISBN };
    }
}
