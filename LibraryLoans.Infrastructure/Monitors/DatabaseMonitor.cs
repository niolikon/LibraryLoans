using LibraryLoans.Core.Monitors;
using LibraryLoans.Infrastructure.Repositories;

namespace LibraryLoans.Infrastructure.Monitors;

public class DatabaseMonitor(AppDbContext dbContext) : IDatabaseMonitor
{
    public async Task<bool> IsDatabaseAlive()
    {
        return await dbContext.Database.CanConnectAsync();
    }
}
