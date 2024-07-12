using WebApiLibrary.Domain.Entities.Generics;

namespace WebApiLibrary.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<Paginated<T>> GetPaginationAsync(int page, int pageSize);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
