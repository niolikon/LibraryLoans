using LibraryLoans.Core.Monitors;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LibraryLoans.Core.Services;

public class DatabaseService(IDatabaseMonitor monitor) : IDatabaseService
{
    public async Task<HealthCheckResult> IsDatabaseAlive()
    {
        if (await monitor.IsDatabaseAlive())
        {
            return HealthCheckResult.Healthy("Database alive");
        }
        else
        {
            return HealthCheckResult.Unhealthy("Database not alive");

        }
    }
}
