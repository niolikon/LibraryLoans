using LibraryLoans.Infrastructure.Repositories;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace LibraryLoans.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication EnsureDatabaseCreated(this WebApplication webApplication)
    {
        using (var scope = webApplication.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.EnsureCreated();
        }

        return webApplication;
    }
    public static WebApplication MapApplicationHealthChecks(this WebApplication webApplication)
    {
        webApplication.MapHealthChecks("/health");

        webApplication.MapHealthChecks("/health/database", new HealthCheckOptions
        {
            Predicate = check => check.Tags.Contains("db")
        });

        return webApplication;
    }
}
