using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystem.Domain;
using GymManagementSystem.Infrastructure;

namespace GymManagementSystem.Application
{
    public class SubscriptionUseCases : ISubscriptionUseCases
    {
        private readonly ISubscriptionsRepository subscriptionsRepository;

        public SubscriptionUseCases(ISubscriptionsRepository subscriptionsRepository)
        {
            this.subscriptionsRepository = subscriptionsRepository;
        }

        public async Task AddSubscriptionAsync(Subscription subscription)
        {
            if (subscription == null) return;

            var Subscriptions = await subscriptionsRepository.GetSubscriptionsAsync();
            subscription.SubscriptionId = (Subscriptions.Max(x => x.SubscriptionId) + 1);

            await subscriptionsRepository.AddSubscriptionAsync(subscription);
        }

        public async Task EditSubscriptionAsync(Subscription subscription)
        {
            if (subscription == null) return;

            await subscriptionsRepository.UpdateSubscriptionAsync(subscription);
        }

        public async Task DeleteSubscriptionAsync(int subscriptionId)
        {
            await subscriptionsRepository.DeleteSubscriptionAsync(subscriptionId);
        }

        public async Task<List<Subscription>> GetSubscriptionsAsync()
        {
            return await subscriptionsRepository.GetSubscriptionsAsync();
        }

        public async Task<Subscription> GetSubscriptionByIdAsync(int subscriptionId)
        {
            return await subscriptionsRepository.GetSubscriptionByIdAsync(subscriptionId);
        }

        public async Task<int> CalculateSubscriptionPriceAsync(Subscription subscription)
        {
            if (subscription == null) throw new Exception("Imposible to calculate subscription price, because subscription is null");

            int subscriptionPrice = subscription.VisitingAmount * 150;
            return subscriptionPrice;
        }

        public async Task<bool> FreezeSubscriptionAsync(int subscriptionId)
        {
            var subscription = await subscriptionsRepository.GetSubscriptionByIdAsync(subscriptionId);

            if (subscription.IsFreezed) return true;
            if (subscription.FreezeDaysAmount <= 0) return false;


            subscription.IsFreezed = true;
            subscription.FreezeDate = DateTime.Now;

            await subscriptionsRepository.UpdateSubscriptionAsync(subscription);
            return true;
        }

        public async Task<bool> UnfreezeSubscriptionAsync(int subscriptionId)
        {
            var subscription = await subscriptionsRepository.GetSubscriptionByIdAsync(subscriptionId);

            if (!subscription.IsFreezed) return true;

            subscription.IsFreezed = false;
            int PassedDays = Convert.ToInt32((DateTime.Now - subscription.FreezeDate.Value).TotalDays);

            if (PassedDays <= subscription.FreezeDaysAmount)
            {
                subscription.FreezeDaysAmount -= PassedDays;
                subscription.DurationInDays += PassedDays;
            }
            else if (PassedDays > subscription.FreezeDaysAmount)
            {
                subscription.DurationInDays += subscription.FreezeDaysAmount;
                subscription.FreezeDaysAmount = 0;
            }

            await subscriptionsRepository.UpdateSubscriptionAsync(subscription);
            return true;
        }
    }
}
