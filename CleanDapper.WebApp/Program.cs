using System.Data;
using CleanDapper.Application.Services;
using CleanDapper.Infrastructure.Repository;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    return new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IStudentRepository>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    return new NpgsqlStudentRepository(configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IStudentServices, StudentServices>();

// Add controllers
builder.Services.AddControllers();

// Add other services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
