using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystem.Domain;
using GymManagementSystem.Infrastructure;

namespace GymManagementSystem.Application
{
    public class GroupUseCases : IGroupUseCases
    {
        private readonly IGroupsRepository groupsRepository;

        public GroupUseCases(IGroupsRepository groupsRepository)
        {
            this.groupsRepository = groupsRepository;
        }

        public async Task<int> AddGroupAsync(Group group)
        {
            if (group == null) throw new ArgumentNullException("AddGroupAsync(Group group) – group == null");

            var Groups = await groupsRepository.GetGroupsAsync();
            group.GroupId = (Groups.Max(x => x.GroupId) + 1);

            await groupsRepository.AddGroupAsync(group);

            return group.GroupId;
        }

        public async Task EditGroupAsync(Group group)
        {
            if (group == null) return;

            await groupsRepository.UpdateGroupAsync(group);
        }

        public async Task DeleteGroupAsync(int groupId)
        {
            await groupsRepository.DeleteGroupAsync(groupId);
        }

        public async Task<List<Group>> GetGroupsAsync()
        {
            return await groupsRepository.GetGroupsAsync();
        }

        public async Task<Group> GetGroupByIdAsync(int groupId)
        {
            return await groupsRepository.GetGroupByIdAsync(groupId);
        }
    }
}
