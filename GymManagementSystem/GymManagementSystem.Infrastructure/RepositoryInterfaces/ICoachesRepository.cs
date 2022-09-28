using GymManagementSystem.Domain;

namespace GymManagementSystem.Infrastructure
{
    public interface ICoachesRepository
    {
        Task AddCoachAsync(Coach coach);
        Task DeleteCoachAsync(int coachId);
        Task<Coach> GetCoachByIdAsync(int coachId);
        Task<List<Coach>> GetCoachesAsync();
        Task UpdateCoachAsync(Coach coach);
    }
}