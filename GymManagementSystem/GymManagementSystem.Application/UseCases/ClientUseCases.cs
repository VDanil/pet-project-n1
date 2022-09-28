using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystem.Domain;
using GymManagementSystem.Infrastructure;

namespace GymManagementSystem.Application
{
    public class ClientUseCases : IClientUseCases
    {
        private readonly IClientsRepository clientsRepository;

        public ClientUseCases(IClientsRepository clientsRepository)
        {
            this.clientsRepository = clientsRepository;
        }

        public async Task AddClientAsync(Client client)
        {
            if (client == null) return;

            var Clients = await clientsRepository.GetClientsAsync();
            client.ClientId = (Clients.Max(x => x.ClientId) + 1);

            await clientsRepository.AddClientAsync(client);
        }

        public async Task EditClientAsync(Client client)
        {
            if (client == null) return;

            await clientsRepository.UpdateClientAsync(client);
        }

        public async Task DeleteClientAsync(int clientId)
        {
            await clientsRepository.DeleteClientAsync(clientId);
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            return await clientsRepository.GetClientsAsync();
        }

        public async Task<Client> GetClientByIdAsync(int clientId)
        {
            return await clientsRepository.GetClientByIdAsync(clientId);
        }
    }
}
