using WebApiLibrary.Domain.Entities;
using WebApiLibrary.Domain.Entities.Generics;

namespace WebApiLibrary.Domain.Interfaces.Services
{
    public interface IBookService
    {
        Task<Paginated<Book>> GetBooksPaginationAsync(int page, int pageSize);
        Task AddBookAsync(Book book);
        Task DeleteBookAsync(Guid id);
        Task<IEnumerable<Book>> GetAllBooksAsync();
    }
}
