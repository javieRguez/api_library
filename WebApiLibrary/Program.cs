using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApiLibrary.Domain.Entities;
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
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IBookLoanRepository, BookLoanRepository>();
builder.Services.AddScoped<IBookLoanService, BookLoanService>();
builder.Services.AddScoped<IClientService, ClientService>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
    using (var context = dbContextFactory.CreateDbContext())
    {
        context.Database.EnsureCreated();
        if (!context.Clients.Any())
        {
            context.Clients.AddRange(
                new Client
                {
                    Id = Guid.NewGuid(),
                    Name = "Javier Rodriguez",
                    BookLoans = new List<BookLoan>()
                },
               new Client
               {
                   Id = Guid.NewGuid(),
                   Name = "Francisco Iñiguez",
                   BookLoans = new List<BookLoan>(),
               }
            );

            context.SaveChanges();
            Console.WriteLine("Datos iniciales agregados correctamente.");
        }
    }
}

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
