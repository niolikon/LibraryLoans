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
        builder.Entity<Loan>()
            .HasOne<Member>(loan => loan.Member)
            .WithMany(member => member.Loans)
            .HasForeignKey(loan => loan.MemberId)
            .IsRequired();

        builder.Entity<Loan>()
            .HasOne<Book>(loan => loan.Book)
            .WithMany()
            .HasForeignKey(loan => loan.BookId)
            .IsRequired();
    }
}
