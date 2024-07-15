using Microsoft.EntityFrameworkCore;
using WebApiLibrary.Domain.Entities;
using WebApiLibrary.Domain.Interfaces.Repositories;
using WebApiLibrary.Infrastructure.Data;

namespace WebApiLibrary.Infrastructure.Repositories
{
    public class BookLoanRepository : GenericRepository<BookLoan>, IBookLoanRepository
    {
        protected readonly AppDbContext _context;

        public BookLoanRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<BookLoan> GettBookLoanAsync(Guid clientId, Guid bookId)
        {

           return await _context.BookLoans
                .Where(w => w.ClientId == clientId && w.BookId == bookId)
                .FirstOrDefaultAsync();
        }
    }
}
