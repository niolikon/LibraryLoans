using LibraryLoans.Core.Entities;
using LibraryLoans.Core.Repositories;
using LibraryLoans.Infrastructure.Commons;

namespace LibraryLoans.Infrastructure.Repositories;

public class MemberRepository(AppDbContext dbContext) : BaseCrudRepository<Member, int>(dbContext), IMemberRepository { }
