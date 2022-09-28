using GymManagementSystem.Domain;

namespace GymManagementSystem.Infrastructure
{
    public interface IAdministratorsRepository
    {
        Task AddAdministratorAsync(Administrator administrator);
        Task DeleteAdministratorAsync(int administratorId);
        Task<Administrator> GetAdministratorByIdAsync(int administratorId);
        Task<List<Administrator>> GetAdministratorsAsync();
        Task UpdateAdministratorAsync(Administrator administrator);
    }
}