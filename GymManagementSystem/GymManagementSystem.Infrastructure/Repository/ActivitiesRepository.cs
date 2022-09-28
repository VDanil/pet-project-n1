using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using GymManagementSystem.Domain;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure
{
    public class ActivitiesRepository : IActivitiesRepository
    {
        private GymManagementContext db;

        public ActivitiesRepository(GymManagementContext db)
        {
            this.db = db;
        }

        public async Task AddActivityAsync(Activity activity)
        {
            activity.ActivityId = (await db.Activities.ToListAsync()).Max(a => a.ActivityId) + 1;

            db.Activities.Add(activity);
            await db.SaveChangesAsync();
        }

        public async Task<List<Activity>> GetActivitiesAsync()
        {
            return await db.Activities.ToListAsync();
        }

        public async Task<Activity> GetActivityByIdAsync(int activityId)
        {
            return await db.Activities.FindAsync(activityId);
        }

        public async Task<List<Activity>> GetActivitiesByGroupIdAsync(int groupId)
        {
            return await db.Activities.Where(a => a.GroupId == groupId).ToListAsync();
        }

        public async Task UpdateActivityAsync(Activity activity)
        {
            db.Activities.Attach(activity);

            db.Entry(activity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteActivityAsync(int activityId)
        {
            var activity = await db.Activities.FindAsync(activityId);

            if (activity == null) return;

            db.Activities.Remove(activity);
            await db.SaveChangesAsync();
        }
    }
}
