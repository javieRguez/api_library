using Microsoft.AspNetCore.Mvc;
using WebApiLibrary.Domain.Entities;
using WebApiLibrary.Domain.Interfaces.Services;

namespace WebApiLibrary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {

        private readonly IBookService _bookService;

        public LibraryController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet("GetBooksPaginationAsync")]
        public async Task<IActionResult> GetBooksPaginationAsync(int page, int pageSize)
        {
            var books = await _bookService.GetBooksPaginationAsync(page, pageSize);

            return Ok(books);
        }

        [HttpPost("AddBookAsync")]
        public async Task<IActionResult> AddProductAsync(Book book)
        {
            await _bookService.AddBookAsync(book);
            return Ok();
        }
    }
}
