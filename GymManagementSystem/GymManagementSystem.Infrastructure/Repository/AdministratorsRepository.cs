using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GymManagementSystem.Domain;

namespace GymManagementSystem.Infrastructure
{
    public class AdministratorsRepository : IAdministratorsRepository
    {
        private GymManagementContext db;

        public AdministratorsRepository(GymManagementContext db)
        {
            this.db = db;
        }

        public async Task AddAdministratorAsync(Administrator administrator)
        {
            db.Administrators.Add(administrator);
            await db.SaveChangesAsync();
        }

        public async Task<List<Administrator>> GetAdministratorsAsync()
        {
            return await db.Administrators.ToListAsync();
        }

        public async Task<Administrator> GetAdministratorByIdAsync(int administratorId)
        {
            return await db.Administrators.FindAsync(administratorId);
        }

        public async Task UpdateAdministratorAsync(Administrator administrator)
        {
            db.Administrators.Attach(administrator);

            db.Entry(administrator).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteAdministratorAsync(int administratorId)
        {
            var administrator = await db.Administrators.FindAsync(administratorId);

            if (administrator == null) return;

            db.Administrators.Remove(administrator);
            await db.SaveChangesAsync();
        }
    }
}
