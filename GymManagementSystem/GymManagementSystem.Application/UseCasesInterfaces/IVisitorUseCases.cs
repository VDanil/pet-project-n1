using GymManagementSystem.Domain;

namespace GymManagementSystem.Application
{
    public interface IVisitorUseCases
    {
        Task<List<Coach>> GetCoachesGeneralInfoAsync();
        Task<List<Group>> GetGroupsGeneralInfoAsync();
    }
}