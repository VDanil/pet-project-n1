using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using GymManagementSystem.Application;
using GymManagementSystem.Domain;

namespace GymManagementSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitorController : ControllerBase
    {
        private readonly IVisitorUseCases visitorUseCases;
        private readonly ISubscriptionUseCases subscriptionUseCases;

        public VisitorController(IVisitorUseCases visitorUseCases,
                                 ISubscriptionUseCases subscriptionUseCases)
        {
            this.visitorUseCases = visitorUseCases;
            this.subscriptionUseCases = subscriptionUseCases;
        }

        [HttpGet]
        [Route("Groups")]
        public async Task<IActionResult> GetGroupsAsync()
        {
            var groups = await visitorUseCases.GetGroupsGeneralInfoAsync();
            if (groups.Count == 0) return Ok();

            return Ok(JsonSerializer.Serialize(groups));
        }

        [HttpGet]
        [Route("Coaches")]
        public async Task<IActionResult> GetCoachesAsync()
        {
            var coaches = await visitorUseCases.GetCoachesGeneralInfoAsync();
            if (coaches.Count == 0) return Ok();

            return Ok(JsonSerializer.Serialize(coaches));
        }

        [HttpPut]
        [Route("SubscriptionPrice")]
        public async Task<IActionResult> CalculateSubscriptionPriceAsync(Subscription subscription)
        {
            if (subscription == null) return Ok();

            subscription.Price = await subscriptionUseCases.CalculateSubscriptionPriceAsync(subscription);

            return Ok(JsonSerializer.Serialize(subscription));
        }
    }
}
