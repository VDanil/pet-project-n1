using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using GymManagementSystem.Application;
using GymManagementSystem.Domain;
using Microsoft.AspNetCore.Authorization;

namespace GymManagementSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "Administrator")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionUseCases subscriptionUseCases;

        public SubscriptionController(ISubscriptionUseCases subscriptionUseCases)
        {
            this.subscriptionUseCases = subscriptionUseCases;
        }

        [HttpPost]
        public async Task<IActionResult> PostSubscriptionAsync(Subscription subscription)
        {
            if (subscription == null) return BadRequest();

            await subscriptionUseCases.AddSubscriptionAsync(subscription);

            return Ok();
        }

        [HttpGet]
        [Route("{subscriptionId:int}")]
        public async Task<IActionResult> GetSubscriptionAsync(int subscriptionId)
        {
            var subscription = await subscriptionUseCases.GetSubscriptionByIdAsync(subscriptionId);
            if(subscription == null) return BadRequest();

            return Ok(subscription);
        }

        [HttpGet]
        public async Task<IActionResult> GetSubscriptionsAsync()
        {
            var subscriptions = await subscriptionUseCases.GetSubscriptionsAsync();
            if(subscriptions == null) return BadRequest();

            return Ok(subscriptions);
        }

        [HttpGet]
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
    }
}
