using LibraryLoans.Core.Entities;
using LibraryLoans.Core.Repositories;
using LibraryLoans.Infrastructure.Commons;

namespace LibraryLoans.Infrastructure.Repositories;

public class BookRepository(AppDbContext dbContext) : BaseCrudRepository<Book, int>(dbContext), IBookRepository {}
