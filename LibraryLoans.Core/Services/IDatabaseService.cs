using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LibraryLoans.Core.Services;

public interface IDatabaseService
{
    Task<HealthCheckResult> IsDatabaseAlive();
}
