using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using GymManagementSystem.Application;
using GymManagementSystem.Domain;
using Microsoft.AspNetCore.Authorization;

namespace GymManagementSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionUseCases subscriptionUseCases;

        public SubscriptionController(ISubscriptionUseCases subscriptionUseCases)
        {
            this.subscriptionUseCases = subscriptionUseCases;
        }

        [HttpPost]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> PostSubscriptionAsync(Subscription subscription)
        {
            if (subscription == null) return BadRequest();

            await subscriptionUseCases.AddSubscriptionAsync(subscription);

            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "Administrator")]
        [Route("{subscriptionId:int}")]
        public async Task<IActionResult> GetSubscriptionAsync(int subscriptionId)
        {
            var subscription = await subscriptionUseCases.GetSubscriptionByIdAsync(subscriptionId);
            if(subscription == null) return BadRequest();

            return Ok(subscription);
        }

        [HttpGet]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> GetSubscriptionsAsync()
        {
            var subscriptions = await subscriptionUseCases.GetSubscriptionsAsync();
            if(subscriptions == null) return BadRequest();

            return Ok(subscriptions);
        }

        [HttpGet]
        [Authorize(Policy = "Administrator")]
        [Route("{subscriptionId:int}/freeze")]
        public async Task<IActionResult> FreezeSubscriptionAsync(int subscriptionId)
        {
            var subscription = await subscriptionUseCases.GetSubscriptionByIdAsync(subscriptionId);

            if (subscription == null) return BadRequest();

            var IsFreezed = await subscriptionUseCases.FreezeSubscriptionAsync(subscriptionId);

            if (IsFreezed)
                return Ok(JsonSerializer.Serialize(IsFreezed));
            else
                return Ok(JsonSerializer.Serialize(IsFreezed));
        }

        [HttpGet]
        [Authorize(Policy = "Administrator")]
        [Route("{subscriptionId:int}/unfreeze")]
        public async Task<IActionResult> UnfreezeSubscriptionAsync(int subscriptionId)
        {
            var subscription = await subscriptionUseCases.GetSubscriptionByIdAsync(subscriptionId);

            if (subscription == null) return BadRequest();

            var IsUnfreezed = await subscriptionUseCases.UnfreezeSubscriptionAsync(subscriptionId);

            if (IsUnfreezed)
                return Ok(JsonSerializer.Serialize(IsUnfreezed));
            else
                return Ok(JsonSerializer.Serialize(IsUnfreezed));
        }

        [HttpPut]
        [Route("/api/visitor/SubscriptionPrice")]
        public async Task<IActionResult> CalculateSubscriptionPriceAsync(Subscription subscription)
        {
            if (subscription == null) return Ok();

            subscription.Price = await subscriptionUseCases.CalculateSubscriptionPriceAsync(subscription);

            return Ok(JsonSerializer.Serialize(subscription));
        }
    }
}
