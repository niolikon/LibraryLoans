using LibraryLoans.Core.BaseInterfaces;
using LibraryLoans.Core.Entities;

namespace LibraryLoans.Core.Repositories;

public interface IBookRepository : IBaseRepository<Book, int>
{
}
