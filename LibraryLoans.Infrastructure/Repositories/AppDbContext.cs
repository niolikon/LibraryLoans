using LibraryLoans.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryLoans.Infrastructure.Repositories;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; }

    public DbSet<Loan> Loans { get; set; }

    public DbSet<Member> Members { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Member>()
            .HasMany<Loan>(member => member.Loans)
            .WithOne(loan => loan.Member)
            .HasForeignKey(loan => loan.MemberId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Book>()
            .HasMany<Loan>()
            .WithOne(book => book.Book)
            .HasForeignKey(loan => loan.BookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
