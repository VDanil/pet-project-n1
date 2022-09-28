using GymManagementSystem.Domain;

namespace GymManagementSystem.Infrastructure
{
    public interface ISubscriptionsRepository
    {
        Task AddSubscriptionAsync(Subscription subscription);
        Task DeleteSubscriptionAsync(int subscriptionId);
        Task<Subscription> GetSubscriptionByIdAsync(int subscriptionId);
        Task<List<Subscription>> GetSubscriptionsAsync();
        Task UpdateSubscriptionAsync(Subscription subscription);
    }
}