namespace WebApiLibrary.Domain.Interfaces.Services
{
    public interface IBookLoanService
    {
        Task SaveBookLoanAsync(Guid clientId, Guid bookId);
        Task ReturnBookAsync(Guid clientId, Guid bookId);
    }
}
