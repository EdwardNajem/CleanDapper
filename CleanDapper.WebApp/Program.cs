using System.Data;
using CleanDapper.Application.Services;
using CleanDapper.Infrastructure.Repository;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

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


builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
