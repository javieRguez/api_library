using WebApiLibrary.Domain.Entities;

namespace WebApiLibrary.Domain.Interfaces.Repositories
{
    public interface IBookLoanRepository : IGenericRepository<BookLoan>
    {
        Task<BookLoan> GettBookLoanAsync(Guid clientId, Guid bookId);
    }
}
