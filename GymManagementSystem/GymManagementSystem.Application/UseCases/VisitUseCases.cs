using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystem.Domain;
using GymManagementSystem.Infrastructure;

namespace GymManagementSystem.Application
{
    public class VisitUseCases : IVisitUseCases
    {
        private readonly IVisitsRepository visitsRepository;

        public VisitUseCases(IVisitsRepository visitsRepository)
        {
            this.visitsRepository = visitsRepository;
        }

        public async Task AddVisitAsync(Visit visit)
        {
            if (visit == null) return;

            var Visits = await visitsRepository.GetVisitsAsync();
            visit.VisitId = Visits.Max(x => x.VisitId) + 1;

            await visitsRepository.AddVisitAsync(visit);
        }

        public async Task EditVisitAsync(Visit visit)
        {
            if (visit == null) return;

            await visitsRepository.UpdateVisitAsync(visit);
        }

        public async Task DeleteVisitAsync(int visitId)
        {
            await visitsRepository.DeleteVisitAsync(visitId);
        }

        public async Task<List<Visit>> GetVisitsAsync()
        {
            return await visitsRepository.GetVisitsAsync();
        }

        public async Task<Visit> GetVisitByIdAsync(int visitId)
        {
            return await visitsRepository.GetVisitByIdAsync(visitId);
        }
    }
}
