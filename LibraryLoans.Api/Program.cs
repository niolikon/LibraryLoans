using LibraryLoans.Api.Middlewares;
using LibraryLoans.Api.Extensions;
using LibraryLoans.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>( options => options.UseSqlServer(connectionString));

builder.Services.ConfigureDependencies();

builder.Services.AddApplicationHealthChecks();

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

app.EnsureDatabaseCreated();

app.MapApplicationHealthChecks();

app.MapControllers();

app.Run();
