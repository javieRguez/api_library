using Microsoft.EntityFrameworkCore;
using WebApiLibrary.Domain.Entities;
using WebApiLibrary.Domain.Interfaces.Repositories;
using WebApiLibrary.Infrastructure.Data;

namespace WebApiLibrary.Infrastructure.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        protected new readonly AppDbContext _context;
        public ClientRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
