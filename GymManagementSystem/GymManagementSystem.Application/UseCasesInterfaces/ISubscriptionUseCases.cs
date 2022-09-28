using GymManagementSystem.Domain;

namespace GymManagementSystem.Application
{
    public interface ISubscriptionUseCases
    {
        Task AddSubscriptionAsync(Subscription subscription);
        Task<int> CalculateSubscriptionPriceAsync(Subscription subscription);
        Task DeleteSubscriptionAsync(int subscriptionId);
        Task EditSubscriptionAsync(Subscription subscription);
        Task<bool> FreezeSubscriptionAsync(int subscriptionId);
        Task<Subscription> GetSubscriptionByIdAsync(int subscriptionId);
        Task<List<Subscription>> GetSubscriptionsAsync();
        Task<bool> UnfreezeSubscriptionAsync(int subscriptionId);
    }
}