using GymManagementSystem.Domain;

namespace GymManagementSystem.Application
{
    public interface IClientUseCases
    {
        Task AddClientAsync(Client client);
        Task DeleteClientAsync(int clientId);
        Task EditClientAsync(Client client);
        Task<Client> GetClientByIdAsync(int clientId);
        Task<List<Client>> GetClientsAsync();
    }
}