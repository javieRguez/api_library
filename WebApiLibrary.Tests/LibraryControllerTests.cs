using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using WebApiLibrary.Api.Controllers;
using WebApiLibrary.Domain.Entities;
using WebApiLibrary.Domain.Entities.Generics;
using WebApiLibrary.Domain.Interfaces.Services;

namespace WebApiLibrary.Tests
{
    [TestFixture]
    public class LibraryControllerTests
    {
        private LibraryController _controller;
        private Mock<IClientService> _clientServiceMock;
        private Mock<IBookLoanService> _bookLoanServiceMock;
        private Mock<IBookService> _bookServiceMock;

        [SetUp]
        public void Setup()
        {
            _clientServiceMock = new Mock<IClientService>();
            _bookLoanServiceMock = new Mock<IBookLoanService>();
            _bookServiceMock = new Mock<IBookService>();

            _controller = new LibraryController(_bookServiceMock.Object, _bookLoanServiceMock.Object, _clientServiceMock.Object);

        }

        [Test]
        public async Task GetAllClientsAsync_ReturnsOkResult()
        {
            var expectedTypeResult = new List<Client>();

            _clientServiceMock.Setup(service => service.GetAllClientsAsync())
                .ReturnsAsync(expectedTypeResult);

            var result = await _controller.GetAllClientsAsync();

            var okResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, (int)okResult.StatusCode);

            var resultClients = (List<Client>)okResult.Value;
            CollectionAssert.AreEquivalent(expectedTypeResult, resultClients);
        }

        [Test]
        public async Task GetAllBooksAsync_ReturnsOkResult()
        {
            var expectedTypeResult = new List<Book>();

            _bookServiceMock.Setup(service => service.GetAllBooksAsync())
                .ReturnsAsync(expectedTypeResult);

            var result = await _controller.GetAllBooksAsync();

            var okResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, (int)okResult.StatusCode);

            var resultBooks = (List<Book>)okResult.Value;
            CollectionAssert.AreEquivalent(expectedTypeResult, resultBooks);
        }

        [Test]
        public async Task GetBooksPaginationAsync_ReturnsOkResult()
        {
            List<Book> items = new List<Book>();
            string queryTerm = null;
            int page = 1, pageSize = 10;

            var expectedTypeResult = new Paginated<Book>(items, page, pageSize);

            _bookServiceMock.Setup(service => service.GetBooksPaginationAsync(page, pageSize, queryTerm))
                .ReturnsAsync(expectedTypeResult);

            var result = await _controller.GetBooksPaginationAsync();

            var okResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, (int)okResult.StatusCode);

        }

        [Test]
        public async Task AddBookAsync_ReturnsOkResult()
        {
            var newBook = new Book
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Description = "Test",
                Price = 200,
                Quantity = 10,
                Gender = GenderEnum.FANTASY
            };

            var result = await _controller.AddBookAsync(newBook);

            var okResult = (OkResult)result;

            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Test]
        public async Task DeleteBookAsync_ReturnsOkResult()
        {
            var bookId = Guid.NewGuid();

            var result = await _controller.DeleteBookAsync(bookId);

            var okResult = (OkResult)result;

            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Test]
        public async Task SaveBookLoanAsync_ReturnsOkResult()
        {
            var bookId = Guid.NewGuid();
            var clientId = Guid.NewGuid();

            var result = await _controller.SaveBookLoanAsync(clientId, bookId);

            var okResult = (OkResult)result;

            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Test]
        public async Task ReturnBookAsync_ReturnsOkResult()
        {
            var bookId = Guid.NewGuid();
            var clientId = Guid.NewGuid();

            var result = await _controller.ReturnBookAsync(clientId, bookId);

            var okResult = (OkResult)result;

            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
        }
    }
}