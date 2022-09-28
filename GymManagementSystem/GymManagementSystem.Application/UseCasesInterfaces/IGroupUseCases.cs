using GymManagementSystem.Domain;

namespace GymManagementSystem.Application
{
    public interface IGroupUseCases
    {
        Task<int> AddGroupAsync(Group group);
        Task DeleteGroupAsync(int groupId);
        Task EditGroupAsync(Group group);
        Task<Group> GetGroupByIdAsync(int groupId);
        Task<List<Group>> GetGroupsAsync();
    }
}