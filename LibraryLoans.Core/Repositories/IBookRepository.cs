using LibraryLoans.Core.Commons;
using LibraryLoans.Core.Entities;

namespace LibraryLoans.Core.Repositories;

public interface IBookRepository : ICrudRepository<Book, int>
{
}
