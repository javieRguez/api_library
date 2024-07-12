using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApiLibrary.Domain.Interfaces.Repositories;
using WebApiLibrary.Domain.Interfaces.Services;
using WebApiLibrary.Infrastructure.Data;
using WebApiLibrary.Infrastructure.Repositories;
using WebApiLibrary.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextFactory<AppDbContext>(options => options.UseInMemoryDatabase("Library"));


builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();


var app = builder.Build();

//Configure cors
var urlAceptadas = builder.Configuration.GetSection("AllowedHosts").Value?.Split(",");
app.UseCors(builder => builder.WithOrigins(urlAceptadas ?? [])
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      );

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
