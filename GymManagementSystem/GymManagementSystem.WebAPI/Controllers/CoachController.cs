using GymManagementSystem.Application;
using GymManagementSystem.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GymManagementSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoachController : ControllerBase
    {
        private readonly ICoachUseCases coachUseCases;
        private readonly ITimetableUseCases timetableUseCases;
        private readonly IVisitorUseCases visitorUseCases;

        public CoachController(ICoachUseCases coachUseCases,
                               ITimetableUseCases timetableUseCases,
                               IVisitorUseCases visitorUseCases)
        {
            this.coachUseCases = coachUseCases;
            this.timetableUseCases = timetableUseCases;
            this.visitorUseCases = visitorUseCases;
        }

        [HttpPost]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> PostCoachAsync(Coach coach)
        {
            if (coach == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            await coachUseCases.AddCoachAsync(coach);

            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "Administrator")]
        [Route("{coachId:int}")]
        public async Task<IActionResult> GetCoachAsync(int coachId)
        {
            var coach = await coachUseCases.GetCoachByIdAsync(coachId);
            if (coach == null) return BadRequest();

            return Ok(coach);
        }

        [HttpGet]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> GetCoachsAsync()
        {
            var coachs = await coachUseCases.GetCoachesAsync();
            if (coachs == null) return Ok();

            return Ok(JsonSerializer.Serialize(coachs));
        }

        [HttpPut]
        [Authorize(Policy = "Administrator")]
        public async Task<ActionResult> PutCoachAsync(Coach coach)
        {
            if (coach == null) return BadRequest();

            await coachUseCases.EditCoachAsync(coach);

            return Ok();
        }

        [HttpDelete]
        [Authorize(Policy = "Administrator")]
        [Route("{coachId:int}")]
        public async Task<IActionResult> DeleteCoachAsync(int coachId)
        {
            await coachUseCases.DeleteCoachAsync(coachId);

            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "Administrator")]
        [Route("{coachId:int}/timetable")]
        public async Task<ActionResult> GetCoachTimetableAsync(int coachId)
        {
            var timetable = await timetableUseCases.GetCoachTimetableAsync(coachId);
            if (timetable == null) return BadRequest();

            return Ok(timetable);
        }

        [HttpGet]
        [Route("/api/visitor/coaches")]
        public async Task<IActionResult> GetCoachesForVisitorAsync()
        {
            var coaches = await visitorUseCases.GetCoachesGeneralInfoAsync();
            if (coaches.Count == 0) return Ok();

            return Ok(JsonSerializer.Serialize(coaches));
        }
    }
}
