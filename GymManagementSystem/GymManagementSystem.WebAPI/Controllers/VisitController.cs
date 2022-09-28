using Microsoft.AspNetCore.Mvc;
using GymManagementSystem.Application;
using GymManagementSystem.Domain;
using Microsoft.AspNetCore.Authorization;

namespace GymManagementSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "Administrator")]
    public class VisitController : ControllerBase
    {
        private readonly IVisitUseCases visitUseCases;

        public VisitController(IVisitUseCases visitUseCases)
        {
            this.visitUseCases = visitUseCases;
        }

        [HttpPost]
        public async Task<IActionResult> PostVisitAsync(Visit visit)
        {
            if (visit == null) return BadRequest();

            await visitUseCases.AddVisitAsync(visit);

            return Ok();
        }
    }
}
