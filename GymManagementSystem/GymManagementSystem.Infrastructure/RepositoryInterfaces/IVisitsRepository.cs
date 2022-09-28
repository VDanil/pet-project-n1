using GymManagementSystem.Domain;

namespace GymManagementSystem.Infrastructure
{
    public interface IVisitsRepository
    {
        Task AddVisitAsync(Visit visit);
        Task DeleteVisitAsync(int coachId);
        Task<Visit> GetVisitByIdAsync(int coachId);
        Task<List<Visit>> GetVisitsAsync();
        Task UpdateVisitAsync(Visit visit);
    }
}