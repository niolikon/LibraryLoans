using LibraryLoans.Api.HealthChecks;
using LibraryLoans.Core.Mappers;
using LibraryLoans.Core.Monitors;
using LibraryLoans.Core.Repositories;
using LibraryLoans.Core.Services;
using LibraryLoans.Infrastructure.Monitors;
using LibraryLoans.Infrastructure.Repositories;

namespace LibraryLoans.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
    {
        #region Mappers
        services.AddScoped<IBookMapper, BookMapper>();
        services.AddScoped<ILoanMapper, LoanMapper>();
        services.AddScoped<IMemberMapper, MemberMapper>();
        #endregion

        #region Repositories
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<ILoanRepository, LoanRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        #endregion

        #region Monitors
        services.AddScoped<IDatabaseMonitor, DatabaseMonitor>();
        #endregion

        #region Services
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<ILoanService, LoanService>();
        services.AddScoped<IMemberService, MemberService>();
        services.AddScoped<IDatabaseService, DatabaseService>();
        #endregion

        return services;
    }

    public static IServiceCollection AddApplicationHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck<DatabaseHealthCheck>("Database", tags: ["db"]);

        return services;
    }
}
