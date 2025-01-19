using LibraryLoans.Core.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LibraryLoans.Api.HealthChecks;

public class DatabaseHealthCheck(IDatabaseService service) : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return service.IsDatabaseAlive();
    }
}
