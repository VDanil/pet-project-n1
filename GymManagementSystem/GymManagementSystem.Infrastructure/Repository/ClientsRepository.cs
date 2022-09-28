using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystem.Domain;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure
{
    public class ClientsRepository : IClientsRepository
    {
        private GymManagementContext db;

        public ClientsRepository(GymManagementContext db)
        {
            this.db = db;
        }

        public async Task AddClientAsync(Client client)
        {
            db.Clients.Add(client);
            await db.SaveChangesAsync();
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            return await db.Clients.ToListAsync();
        }

        public async Task<Client> GetClientByIdAsync(int clientId)
        {
            return await db.Clients.FindAsync(clientId);
        }

        public async Task UpdateClientAsync(Client client)
        {
            db.Clients.Attach(client);

            db.Entry(client).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteClientAsync(int clientId)
        {
            var client = await db.Clients.FindAsync(clientId);

            if (client == null) return;

            db.Clients.Remove(client);
            await db.SaveChangesAsync();
        }
    }
}
