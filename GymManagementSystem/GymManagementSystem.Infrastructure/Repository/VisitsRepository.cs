using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystem.Domain;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure
{
    public class VisitsRepository : IVisitsRepository
    {
        private GymManagementContext db;

        public VisitsRepository(GymManagementContext db)
        {
            this.db = db;
        }

        public async Task AddVisitAsync(Visit visit)
        {
            db.Visits.Add(visit);
            await db.SaveChangesAsync();
        }

        public async Task<List<Visit>> GetVisitsAsync()
        {
            return await db.Visits.ToListAsync();
        }

        public async Task<Visit> GetVisitByIdAsync(int coachId)
        {
            return await db.Visits.FindAsync(coachId);
        }

        public async Task UpdateVisitAsync(Visit visit)
        {
            db.Visits.Attach(visit);

            db.Entry(visit).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteVisitAsync(int coachId)
        {
            var visit = await db.Visits.FindAsync(coachId);

            if (visit == null) return;

            db.Visits.Remove(visit);
            await db.SaveChangesAsync();
        }
    }
}
