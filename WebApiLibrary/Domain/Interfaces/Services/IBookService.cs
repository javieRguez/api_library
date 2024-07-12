using WebApiLibrary.Domain.Entities;
using WebApiLibrary.Domain.Entities.Generics;

namespace WebApiLibrary.Domain.Interfaces.Services
{
    public interface IBookService
    {
        Task<Paginated<Book>> GetBooksPaginationAsync(int page, int pageSize);
        Task<Book> GetBookByIdAsync(Guid id);
        Task AddBookAsync(Book book);
        void UpdateBook(Book book);
        void DeleteBook(Guid id);
    }
}
