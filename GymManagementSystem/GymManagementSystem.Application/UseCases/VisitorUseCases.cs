using System.Collections.Generic;
using GymManagementSystem.Domain;
using GymManagementSystem.Infrastructure;

namespace GymManagementSystem.Application
{
    public class VisitorUseCases : IVisitorUseCases // TODO: Wrewrite this class to keep principle Information expert
    {
        private readonly IGroupsRepository groupsRepository;
        private readonly ICoachesRepository coachesRepository;

        public VisitorUseCases(IGroupsRepository groupsRepository,
                               ICoachesRepository coachesRepository)
        {
            this.groupsRepository = groupsRepository;
            this.coachesRepository = coachesRepository;
        }

        public async Task<List<Group>> GetGroupsGeneralInfoAsync()
        {
            var groups = await groupsRepository.GetGroupsAsync();

            foreach (var group in groups)
            {
                group.Subscriptions = null;
                group.Activities = null;
            }

            return groups;
        }

        public async Task<List<Coach>> GetCoachesGeneralInfoAsync()
        {
            var coaches = await coachesRepository.GetCoachesAsync();
            return coaches;
        }
    }
}