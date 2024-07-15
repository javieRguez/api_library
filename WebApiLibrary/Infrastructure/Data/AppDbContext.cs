using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using WebApiLibrary.Domain.Entities;

namespace WebApiLibrary.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<BookLoan> BookLoans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasData(
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

            modelBuilder.Entity<BookLoan>()
                 .HasKey(l => new { l.ClientId, l.BookId });

            modelBuilder.Entity<BookLoan>()
                .HasOne(l => l.Client)
                .WithMany(x => x.BookLoans)
                .HasForeignKey(c => c.ClientId);

            modelBuilder.Entity<BookLoan>()
               .HasOne(l => l.Book)
               .WithMany(x => x.BookLoans)
               .HasForeignKey(c => c.BookId);
        }
    }
}
