using GymManagementSystem.Domain;

namespace GymManagementSystem.Infrastructure
{
    public interface IActivitiesRepository
    {
        Task AddActivityAsync(Activity activity);
        Task DeleteActivityAsync(int activityId);
        Task<List<Activity>> GetActivitiesAsync();
        Task<List<Activity>> GetActivitiesByGroupIdAsync(int groupId);
        Task<Activity> GetActivityByIdAsync(int activityId);
        Task UpdateActivityAsync(Activity activity);
    }
}