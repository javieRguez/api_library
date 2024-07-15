using WebApiLibrary.Domain.Entities;
using WebApiLibrary.Domain.Entities.Generics;

namespace WebApiLibrary.Domain.Interfaces.Repositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<Paginated<Book>> GetPaginationAsync(int page, int pageSize);
    }
}
