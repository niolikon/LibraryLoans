using LibraryLoans.Core.Entities;
using LibraryLoans.Core.Repositories;
using LibraryLoans.Infrastructure.BaseClasses;

namespace LibraryLoans.Infrastructure.Repositories;

public class MemberRepository(AppDbContext dbContext) : BaseRepository<Member, int>(dbContext), IMemberRepository { }
