using GymManagementSystem.Domain;

namespace GymManagementSystem.Application
{
    public interface ITimetableUseCases
    {
        Task<int> ClearGroupTimetableAsync(int groupId);
        Task DeleteTimetableAsync(List<Activity> activities);
        Task<List<Activity>> GetCoachTimetableAsync(int coachId);
        Task<List<Activity>> GetGlobalTimetableAsync();
        Task<List<Activity>> GetGroupTimetableAsync(int groupId);
        Task<bool> SetGroupTimetableAsync(int groupId, List<Activity> groupTimetable);
    }
}