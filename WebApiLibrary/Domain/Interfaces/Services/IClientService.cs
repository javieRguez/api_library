using WebApiLibrary.Domain.Entities;

namespace WebApiLibrary.Domain.Interfaces.Services
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> GetAllClientsAsync();
    }
}
