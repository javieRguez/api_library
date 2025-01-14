﻿using Microsoft.EntityFrameworkCore;
using WebApiLibrary.Domain.Entities;
using WebApiLibrary.Domain.Entities.Generics;
using WebApiLibrary.Domain.Interfaces.Repositories;
using WebApiLibrary.Infrastructure.Data;

namespace WebApiLibrary.Infrastructure.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        protected new readonly AppDbContext _context;

        public BookRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Paginated<Book>> GetPaginationAsync(int page, int pageSize, string queryTerm = null)
        {

            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(queryTerm))
            {
                query = query.Where(p => p.Name.ToLower().Contains(queryTerm.ToLower()));
            }
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var count = await _context.Set<Book>().CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            return new Paginated<Book>(items, page, totalPages);
        }
    }
}
