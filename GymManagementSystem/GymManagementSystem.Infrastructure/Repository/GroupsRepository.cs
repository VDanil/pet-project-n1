using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystem.Domain;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure
{
    public class GroupsRepository : IGroupsRepository
    {
        private GymManagementContext db;

        public GroupsRepository(GymManagementContext db)
        {
            this.db = db;
        }

        public async Task AddGroupAsync(Group group)
        {
            var g = db.Groups.Add(group);
            if (g.IsKeySet) await db.SaveChangesAsync();
        }

        public async Task<List<Group>> GetGroupsAsync()
        {
            return await db.Groups.ToListAsync();
        }

        public async Task<Group> GetGroupByIdAsync(int groupId)
        {
            return await db.Groups.FindAsync(groupId);
        }

        public async Task UpdateGroupAsync(Group group)
        {
            db.Groups.Attach(group);

            db.Entry(group).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteGroupAsync(int groupId)
        {
            var group = await db.Groups.FindAsync(groupId);

            if (group == null) return;

            db.Groups.Remove(group);
            await db.SaveChangesAsync();
        }
    }
}
