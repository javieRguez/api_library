using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApiLibrary.Domain.Entities;
using WebApiLibrary.Domain.Entities.Generics;
using WebApiLibrary.Domain.Interfaces.Repositories;
using WebApiLibrary.Domain.Interfaces.Services;
using WebApiLibrary.Infrastructure.Repositories;

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
            try
            {
                if (!(book.Quantity > 0))
                {
                    throw new ArgumentException("La cantidad tiene que ser mayor a 0.");
                }
                if (!(book.Price > 0))
                {
                    throw new ArgumentException("El precio tiene que ser mayor a 0.");
                }
                await _bookRepository.AddAsync(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task DeleteBookAsync(Guid id)
        {
            try
            {
                var book = await _bookRepository.GetByIdAsync(id);
                if (book != null)
                {
                    await _bookRepository.DeleteAsync(book);
                }
                else
                {
                    throw new ArgumentException("El libro no fue encontrado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<Paginated<Book>> GetBooksPaginationAsync(int page, int pageSize, string queryTerm = null)
        {
            try
            {
                return await _bookRepository.GetPaginationAsync(page, pageSize, queryTerm);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            try
            {
                var clients = await _bookRepository.GetAllAsync();

                return clients;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
