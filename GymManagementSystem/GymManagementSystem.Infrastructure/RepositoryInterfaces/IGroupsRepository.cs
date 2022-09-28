using GymManagementSystem.Domain;

namespace GymManagementSystem.Infrastructure
{
    public interface IGroupsRepository
    {
        Task AddGroupAsync(Group group);
        Task DeleteGroupAsync(int groupId);
        Task<Group> GetGroupByIdAsync(int groupId);
        Task<List<Group>> GetGroupsAsync();
        Task UpdateGroupAsync(Group group);
    }
}