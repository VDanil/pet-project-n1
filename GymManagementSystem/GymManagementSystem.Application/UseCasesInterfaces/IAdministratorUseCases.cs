using GymManagementSystem.Domain;

namespace GymManagementSystem.Application
{
    public interface IAdministratorUseCases
    {
        Task AddAdministratorAsync(Administrator administrator);
        Task DeleteAdministratorAsync(int administratorId);
        Task EditAdministratorAsync(Administrator administrator);
        Task<Administrator> GetAdministratorByIdAsync(int administratorId);
        Task<List<Administrator>> GetAdministratorsAsync();
    }
}