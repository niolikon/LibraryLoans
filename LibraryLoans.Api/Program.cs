using LibraryLoans.Api.HealthChecks;
using LibraryLoans.Core.Mappers;
using LibraryLoans.Core.Monitors;
using LibraryLoans.Core.Repositories;
using LibraryLoans.Core.Services;
using LibraryLoans.Infrastructure.Monitors;
using LibraryLoans.Infrastructure.Repositories;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Api.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>( options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IBookMapper, BookMapper>();
builder.Services.AddScoped<ILoanMapper, LoanMapper>();
builder.Services.AddScoped<IMemberMapper, MemberMapper>();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IDatabaseMonitor, DatabaseMonitor>();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IDatabaseService, DatabaseService>();

builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>("Database", tags:["db"]);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseMiddleware<ControllerAdviceMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.EnsureCreatedAsync();
}

app.MapHealthChecks("/health");

app.MapHealthChecks("/health/database", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("db")
});

app.MapControllers();

app.Run();
