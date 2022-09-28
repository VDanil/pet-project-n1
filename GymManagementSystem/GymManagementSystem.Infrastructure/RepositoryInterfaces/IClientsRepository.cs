using GymManagementSystem.Domain;

namespace GymManagementSystem.Infrastructure
{
    public interface IClientsRepository
    {
        Task AddClientAsync(Client client);
        Task DeleteClientAsync(int clientId);
        Task<Client> GetClientByIdAsync(int clientId);
        Task<List<Client>> GetClientsAsync();
        Task UpdateClientAsync(Client client);
    }
}