using WebApiLibrary.Domain.Entities;
using WebApiLibrary.Domain.Entities.Generics;
using WebApiLibrary.Domain.Interfaces.Repositories;
using WebApiLibrary.Domain.Interfaces.Services;

namespace WebApiLibrary.Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task AddBookAsync(Book book)
        {
            await _bookRepository.AddAsync(book);
        }

        public void DeleteBook(Guid id)
        {
            var book = _bookRepository.GetByIdAsync(id).Result;
            if (book != null)
            {
                _bookRepository.Delete(book);
            }
        }

        public async Task<Paginated<Book>> GetBooksPaginationAsync(int page, int pageSize)
        {
            return await _bookRepository.GetPaginationAsync(page, pageSize);
        }

        public async Task<Book> GetBookByIdAsync(Guid id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public void UpdateBook(Book book)
        {
            _bookRepository.Update(book);
        }
    }
}
