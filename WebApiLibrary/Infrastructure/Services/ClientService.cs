using WebApiLibrary.Domain.Entities;
using WebApiLibrary.Domain.Interfaces.Repositories;
using WebApiLibrary.Domain.Interfaces.Services;

namespace WebApiLibrary.Infrastructure.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            try
            {
                var clients = await _clientRepository.GetAllAsync();

                return clients;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
