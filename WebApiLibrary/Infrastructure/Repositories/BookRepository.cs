using WebApiLibrary.Domain.Entities;
using WebApiLibrary.Domain.Interfaces.Repositories;
using WebApiLibrary.Infrastructure.Data;

namespace WebApiLibrary.Infrastructure.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context)
        {
        }
    }
}
