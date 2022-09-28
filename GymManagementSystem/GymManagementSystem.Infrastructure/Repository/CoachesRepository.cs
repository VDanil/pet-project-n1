using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystem.Domain;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure
{
    public class CoachesRepository : ICoachesRepository
    {
        private GymManagementContext db;

        public CoachesRepository(GymManagementContext db)
        {
            this.db = db;
        }

        public async Task AddCoachAsync(Coach coach)
        {
            db.Coaches.Add(coach);
            await db.SaveChangesAsync();
        }

        public async Task<List<Coach>> GetCoachesAsync()
        {
            return await db.Coaches.ToListAsync();
        }

        public async Task<Coach> GetCoachByIdAsync(int coachId)
        {
            return await db.Coaches.FindAsync(coachId);
        }

        public async Task UpdateCoachAsync(Coach coach)
        {
            db.Coaches.Attach(coach);

            db.Entry(coach).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteCoachAsync(int coachId)
        {
            var coach = await db.Coaches.FindAsync(coachId);

            if (coach == null) return;

            db.Coaches.Remove(coach);
            await db.SaveChangesAsync();
        }
    }
}
