using Microsoft.EntityFrameworkCore;
using WebApiLibrary.Domain.Entities.Generics;
using WebApiLibrary.Domain.Interfaces.Repositories;
using WebApiLibrary.Infrastructure.Data;

namespace WebApiLibrary.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public async Task<Paginated<T>> GetPaginationAsync(int page, int pageSize)
        {
            var items = await _context
                .Set<T>()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var count = await _context.Set<T>().CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            return new Paginated<T>(items, page, totalPages);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}
