using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystem.Domain;
using GymManagementSystem.Infrastructure;

namespace GymManagementSystem.Application
{
    public class CoachUseCases : ICoachUseCases
    {
        private readonly ICoachesRepository coachesRepository;

        public CoachUseCases(ICoachesRepository coachesRepository)
        {
            this.coachesRepository = coachesRepository;
        }

        public async Task AddCoachAsync(Coach coach)
        {
            if (coach == null) return;

            var Coaches = await coachesRepository.GetCoachesAsync();
            coach.CoachId = Coaches.Max(x => x.CoachId) + 1;

            await coachesRepository.AddCoachAsync(coach);
        }

        public async Task EditCoachAsync(Coach coach)
        {
            if (coach == null) return;

            await coachesRepository.UpdateCoachAsync(coach);
        }

        public async Task<List<Coach>> GetCoachesAsync()
        {
            return await coachesRepository.GetCoachesAsync();
        }

        public async Task<Coach> GetCoachByIdAsync(int coachId)
        {
            return await coachesRepository.GetCoachByIdAsync(coachId);
        }

        public async Task DeleteCoachAsync(int coachId)
        {
            await coachesRepository.DeleteCoachAsync(coachId);
        }
    }
}
