using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystem.Infrastructure;
using GymManagementSystem.Domain;

namespace GymManagementSystem.Application
{
    public class TimetableUseCases : ITimetableUseCases
    {
        private readonly IActivitiesRepository activitiesRepository;
        private readonly ISubscriptionsRepository subscriptionsRepository;
        private readonly IGroupsRepository groupsRepository;

        public TimetableUseCases(IActivitiesRepository activitiesRepository,
                                 ISubscriptionsRepository subscriptionsRepository,
                                 IGroupsRepository groupsRepository)
        {
            this.activitiesRepository = activitiesRepository;
            this.subscriptionsRepository = subscriptionsRepository;
            this.groupsRepository = groupsRepository;
        }

        public async Task<List<Activity>> GetCoachTimetableAsync(int coachId)
        {
            var coachGroups = (await groupsRepository.GetGroupsAsync()).Where((group) => group.CoachId == coachId).ToList();

            var coachTimetable = new List<Activity>();
            if (coachGroups != null)
                foreach (var group in coachGroups)
                {
                    coachTimetable.AddRange(await this.GetGroupTimetableAsync(group.GroupId));
                }

            return coachTimetable;
        }

        public async Task<List<Activity>> GetGroupTimetableAsync(int groupId)
        {
            return await activitiesRepository.GetActivitiesByGroupIdAsync(groupId);
        }

        public async Task<bool> SetGroupTimetableAsync(int groupId, List<Activity> groupTimetable)
        {
            if (groupId == null || groupTimetable == null) throw new Exception("Group Timetable is null.");

            if (!(ExamineTimetableСorrectness(groupTimetable)))
                throw new Exception("Group Timetable has overlap of activities.");



            var coach = (await groupsRepository.GetGroupByIdAsync(groupId)).Coach;
            if (coach != null)
            {
                var coacheTimetable = await this.GetCoachTimetableAsync(coach.CoachId);
                // Remove old group timetable from coach timetable
                coacheTimetable = coacheTimetable.Where((activity) => activity.GroupId != groupId).ToList();
                // Add new group timetable to coach timetable
                coacheTimetable.AddRange(groupTimetable);
                // Validating is it correct timetable
                if (!ExamineTimetableСorrectness(coacheTimetable))
                    throw new Exception("If set GROUP timetable, group's COACH timetable will have overlap of activities.");
            }

            var oldTimetable = await activitiesRepository.GetActivitiesByGroupIdAsync(groupId);

            if (oldTimetable.Count > 0)
                foreach (var activity in oldTimetable)
                    await activitiesRepository.DeleteActivityAsync(activity.ActivityId);

            if (groupTimetable.Count > 0)
                foreach (var activity in groupTimetable)
                    await activitiesRepository.AddActivityAsync(activity);

            return true;
        }

        public async Task<List<Activity>> GetGlobalTimetableAsync()
        {
            return await activitiesRepository.GetActivitiesAsync();
        }

        public async Task<int> ClearGroupTimetableAsync(int groupId)
        {
            var groupTimetable = await GetGroupTimetableAsync(groupId);
            await DeleteTimetableAsync(groupTimetable);
            return groupId;
        }

        public async Task DeleteTimetableAsync(List<Activity> activities)
        {
            if (activities == null) return;

            foreach (var activity in activities)
            {
                if (activity != null)
                    await this.activitiesRepository.DeleteActivityAsync(activity.ActivityId);
            }
        }

        private bool ExamineTimetableСorrectness(List<Activity> timetable)
        {
            bool valid = true;

            var activitiesDayes = timetable.GroupBy(a => a.WeekdayId).Select(activityGroup => activityGroup.Key);
            if (activitiesDayes == null) return valid;
            if (activitiesDayes.Count() == 0) return valid;

            foreach (var day in activitiesDayes)
            {
                var dayTimetable = timetable.Where(a => a.WeekdayId == day).OrderBy(a => a.StartTime).ToList();
                var dayTimetableArr = dayTimetable.ToArray();

                foreach (var activity in dayTimetableArr)
                    if (activity.StartTime > activity.EndTime) return valid = false;

                for (int i = 0; i < dayTimetableArr.Length; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (dayTimetableArr[i].StartTime < dayTimetableArr[j].EndTime)
                            return valid = false;
                    }
                }
            }

            return valid = true;
        }
    }
}
