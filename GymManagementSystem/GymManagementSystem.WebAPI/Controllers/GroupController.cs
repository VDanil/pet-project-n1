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
    public class GroupController : ControllerBase
    {
        private readonly IGroupUseCases groupUseCases;
        private readonly ITimetableUseCases timetableUseCases;

        public GroupController(IGroupUseCases groupUseCases, ITimetableUseCases timetableUseCases)
        {
            this.groupUseCases = groupUseCases;
            this.timetableUseCases = timetableUseCases;
        }

        [HttpPost]
        public async Task<IActionResult> PostGroupAsync(Group group)
        {
            if (group == null) return BadRequest();
            group.Activities = null;
            
            var groupId = await groupUseCases.AddGroupAsync(group);
            group.GroupId = groupId;

            return Ok(JsonSerializer.Serialize(group));
        }

        [HttpGet]
        [Route("{groupId:int}")]
        public async Task<IActionResult> GetGroupAsync(int groupId)
        {
            var group = await groupUseCases.GetGroupByIdAsync(groupId);
            if (group == null) return BadRequest();

            return Ok(JsonSerializer.Serialize(group));
        }

        [HttpGet]
        public async Task<IActionResult> GetGroupsAsync()
        {
            var groups = await groupUseCases.GetGroupsAsync();
            if (groups == null) return Ok();

            return Ok(JsonSerializer.Serialize(groups));
        }

        [HttpPut]
        public async Task<IActionResult> PutGroupAsync(Group group)
        {
            if (group == null) return BadRequest();

            await groupUseCases.EditGroupAsync(group);
            group.Activities = null;

            return Ok(JsonSerializer.Serialize(group));
        }

        [HttpDelete]
        [Route("{groupId:int}")]
        public async Task<IActionResult> DeleteGroupAsync(int groupId)
        {
            await groupUseCases.DeleteGroupAsync(groupId);

            var timetable = await timetableUseCases.GetGroupTimetableAsync(groupId);
            if (timetable != null)
                await timetableUseCases.DeleteTimetableAsync(timetable);

            return Ok();
        }

        [HttpGet]
        [Route("{groupId:int}/timetable")]
        public async Task<ActionResult> GetGroupTimetableAsync(int groupId)
        {
            var timetable = await timetableUseCases.GetGroupTimetableAsync(groupId);
            if (timetable == null) return BadRequest();

            return Ok(JsonSerializer.Serialize(timetable));
        }

        [HttpPut]
        [Route("{groupId:int}/timetable")]
        public async Task<ActionResult> PutGroupTimetableAsync(List<Activity> activities,int groupId)
        {
            if (activities == null) return BadRequest();

            await timetableUseCases.ClearGroupTimetableAsync(groupId);

            await timetableUseCases.SetGroupTimetableAsync(groupId, activities);

            return Ok();
        }
    }
}
