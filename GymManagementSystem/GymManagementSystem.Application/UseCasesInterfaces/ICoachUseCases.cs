using GymManagementSystem.Domain;

namespace GymManagementSystem.Application
{
    public interface ICoachUseCases
    {
        Task AddCoachAsync(Coach coach);
        Task DeleteCoachAsync(int coachId);
        Task EditCoachAsync(Coach coach);
        Task<Coach> GetCoachByIdAsync(int coachId);
        Task<List<Coach>> GetCoachesAsync();
    }
}