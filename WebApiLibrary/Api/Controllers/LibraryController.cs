using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApiLibrary.Api.Exceptions;
using WebApiLibrary.Domain.Entities;
using WebApiLibrary.Domain.Interfaces.Services;

namespace WebApiLibrary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {

        private readonly IBookService _bookService;
        private readonly IBookLoanService _bookLoanService;
        private readonly IClientService _clientService;

        public LibraryController(IBookService bookService, IBookLoanService bookLoanService, IClientService clientService)
        {
            _bookService = bookService;
            _bookLoanService = bookLoanService;
            _clientService = clientService;
        }

        [HttpGet("GetAllClientsAsync")]
        public async Task<IActionResult> GetAllClientsAsync()
        {
            try
            {
                var clients = await _clientService.GetAllClientsAsync();

                return Ok(clients);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción no controlada: {ex.Message}");

                return StatusCode((int)HttpStatusCode.InternalServerError, "Se ha producido un error interno.");
            }
        }
        [HttpGet("GetAllBooksAsync")]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            try
            {
                var clients = await _bookService.GetAllBooksAsync();

                return Ok(clients);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción no controlada: {ex.Message}");

                return StatusCode((int)HttpStatusCode.InternalServerError, "Se ha producido un error interno.");
            }
        }

        [HttpGet("GetBooksPaginationAsync")]
        public async Task<IActionResult> GetBooksPaginationAsync(int page = 1, int pageSize = 10, string queryTerm = null)
        {
            try
            {
                var books = await _bookService.GetBooksPaginationAsync(page, pageSize, queryTerm);

                return Ok(books);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción no controlada: {ex.Message}");

                return StatusCode((int)HttpStatusCode.InternalServerError, "Se ha producido un error interno.");
            }
        }

        [HttpPost("AddBookAsync")]
        public async Task<IActionResult> AddBookAsync(Book book)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _bookService.AddBookAsync(book);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción no controlada: {ex.Message}");

                return StatusCode((int)HttpStatusCode.InternalServerError, "Se ha producido un error interno.");
            }
        }

        [HttpPost("DeleteBookAsync")]
        public async Task<IActionResult> DeleteBookAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentException("El ID del libro es un parametro requerido.");
                }
                await _bookService.DeleteBookAsync(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción no controlada: {ex.Message}");

                return StatusCode((int)HttpStatusCode.InternalServerError, "Se ha producido un error interno.");
            }
        }
        [HttpPost("SaveBookLoanAsync")]
        public async Task<IActionResult> SaveBookLoanAsync(Guid clientId, Guid bookId)
        {
            try
            {
                if (clientId == Guid.Empty)
                {
                    throw new ArgumentException("El ID del cliente es un parametro requerido.");
                }
                if (bookId == Guid.Empty)
                {
                    throw new ArgumentException("El ID del libro es un parametro requerido.");
                }
                await _bookLoanService.SaveBookLoanAsync(clientId, bookId);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción no controlada: {ex.Message}");

                return StatusCode((int)HttpStatusCode.InternalServerError, "Se ha producido un error interno.");
            }
        }
        [HttpPost("ReturnBookAsync")]
        public async Task<IActionResult> ReturnBookAsync(Guid clientId, Guid bookId)
        {
            try
            {
                if (clientId == Guid.Empty)
                {
                    throw new ArgumentException("El ID del cliente es un parametro requerido.");
                }
                if (bookId == Guid.Empty)
                {
                    throw new ArgumentException("El ID del libro es un parametro requerido.");
                }
                await _bookLoanService.ReturnBookAsync(clientId, bookId);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción no controlada: {ex.Message}");

                return StatusCode((int)HttpStatusCode.InternalServerError, "Se ha producido un error interno.");
            }
        }
    }
}
