using LibraryLoans.Core.Entities;
using LibraryLoans.Core.Repositories;
using LibraryLoans.Infrastructure.BaseClasses;

namespace LibraryLoans.Infrastructure.Repositories;

public class BookRepository(AppDbContext dbContext) : BaseRepository<Book, int>(dbContext), IBookRepository {}
