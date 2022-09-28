using GymManagementSystem.Domain;

namespace GymManagementSystem.Application
{
    public interface IVisitUseCases
    {
        Task AddVisitAsync(Visit visit);
        Task DeleteVisitAsync(int visitId);
        Task EditVisitAsync(Visit visit);
        Task<Visit> GetVisitByIdAsync(int visitId);
        Task<List<Visit>> GetVisitsAsync();
    }
}