using WebApiLibrary.Api.Exceptions;
using WebApiLibrary.Domain.Entities;
using WebApiLibrary.Domain.Interfaces.Repositories;
using WebApiLibrary.Domain.Interfaces.Services;

namespace WebApiLibrary.Infrastructure.Services
{
    public class BookLoanService : IBookLoanService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IBookLoanRepository _bookLoanRepository;
        private readonly IBookRepository _bookRepository;

        public BookLoanService(IClientRepository clientRepository, IBookLoanRepository bookLoanRepository, IBookRepository bookRepository)
        {
            _clientRepository = clientRepository;
            _bookLoanRepository = bookLoanRepository;
            _bookRepository = bookRepository;
        }

        public async Task SaveBookLoanAsync(Guid clientId, Guid bookId)
        {
            try
            {
                var client = await _clientRepository.GetByIdAsync(clientId);

                if (client == null)
                {
                    throw new NotFoundException("El cliente no fue encontrado.");
                }
                var book = await _bookRepository.GetByIdAsync(bookId);

                if(book == null)
                {
                    throw new NotFoundException("El libro no fue encontrado.");
                }

                if (book.Quantity > 0)
                {
                    var existBookLoan = await _bookLoanRepository.GettBookLoanAsync(clientId, bookId);
                    if (existBookLoan!= null)
                    {
                        throw new ArgumentException("Ya existe un prestamo con el libro.");
                    }
                    var newLoan = new BookLoan
                    {
                        Id = Guid.NewGuid(),
                        Client = client,
                        Book = book,
                    };
                    book.Quantity -= 1;
                    await _bookRepository.UpdateAsync(book);
                    await _bookLoanRepository.AddAsync(newLoan);

                }
                else
                {
                    throw new ArgumentException("No hay copias disponibles para prestar");
                }
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
        public async Task ReturnBookAsync(Guid clientId, Guid bookId)
        {
            try
            {
                var book = await _bookRepository.GetByIdAsync(bookId);
                if (book == null)
                {
                    throw new NotFoundException("El libro no fue encontrado.");
                }
                var bookLoan = await _bookLoanRepository.GettBookLoanAsync(clientId, bookId);

                if (bookLoan != null)
                {
                    book.Quantity += 1;
                    await _bookRepository.UpdateAsync(book);
                    await _bookLoanRepository.DeleteAsync(bookLoan);
                }
                else
                {
                    throw new NotFoundException("El prestamo no fue encontrado.");
                }
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
