using GymManagementSystem.Application;
using GymManagementSystem.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GymManagementSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "Administrator")]
    public class CoachController : ControllerBase
    {
        private readonly ICoachUseCases coachUseCases;
        private readonly ITimetableUseCases timetableUseCases;

        public CoachController(ICoachUseCases coachUseCases, ITimetableUseCases timetableUseCases)
        {
            this.coachUseCases = coachUseCases;
            this.timetableUseCases = timetableUseCases;
        }

        [HttpPost]      
        public async Task<IActionResult> PostCoachAsync(Coach coach)
        {
            if (coach == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);         

            await coachUseCases.AddCoachAsync(coach);

            return Ok();
        }

        [HttpGet]
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
        public async Task<ActionResult> PutCoachAsync(Coach coach)
        {
            if (coach == null) return BadRequest();

            await coachUseCases.EditCoachAsync(coach);

            return Ok();
        }

        [HttpDelete]
        [Route("{coachId:int}")]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> DeleteCoachAsync(int coachId)
        {
            await coachUseCases.DeleteCoachAsync(coachId);

            return Ok();
        }

        [HttpGet]
        [Route("{coachId:int}/timetable")]
        public async Task<ActionResult> GetCoachTimetableAsync(int coachId)
        {
            var timetable = await timetableUseCases.GetCoachTimetableAsync(coachId);
            if (timetable == null) return BadRequest();

            return Ok(timetable);
        }
    }
}
