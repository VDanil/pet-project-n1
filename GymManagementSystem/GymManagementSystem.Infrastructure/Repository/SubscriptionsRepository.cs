using GymManagementSystem.Domain;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure
{
    public class SubscriptionsRepository : ISubscriptionsRepository
    {
        private GymManagementContext db;

        public SubscriptionsRepository(GymManagementContext db)
        {
            this.db = db;
        }

        public async Task AddSubscriptionAsync(Subscription subscription)
        {
            db.Subscriptions.Add(subscription);
            await db.SaveChangesAsync();
        }

        public async Task<List<Subscription>> GetSubscriptionsAsync()
        {
            return await db.Subscriptions.ToListAsync();
        }

        public async Task<Subscription> GetSubscriptionByIdAsync(int subscriptionId)
        {
            return await db.Subscriptions.FindAsync(subscriptionId);
        }

        public async Task UpdateSubscriptionAsync(Subscription subscription)
        {
            db.Subscriptions.Attach(subscription);

            db.Entry(subscription).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteSubscriptionAsync(int subscriptionId)
        {
            var subscription = await db.Subscriptions.FindAsync(subscriptionId);

            if (subscription == null) return;

            db.Subscriptions.Remove(subscription);
            await db.SaveChangesAsync();
        }
    }
}