using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Domain
{
    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }

        [Required]
        [Activity_EnsureAccuracyStartTimeValue]
        public TimeSpan StartTime { get; set; }
        
        [Required]
        [Activity_EnsureAccuracyEndTimeValue]
        [Activity_EnsureEndTimeAfterStartTime]
        public TimeSpan EndTime { get; set; } // Remove TimeSpan set int
        
        [Required]
        public int WeekdayId { get; set; }

        // Navigation properties
        public virtual int? GroupId { get; set; }
        public virtual Group? Group { get; set; }

        public virtual int? SubscriptionId { get; set; }
        public virtual Subscription? Subscription { get; set; }

        // Validation

        /// <summary>
        /// Ensure that start time of activity is between (0:00 -- 23:59) o'clock
        /// </summary>
        public bool ValidateAccuracyStartTimeValue()
        {
            var minutesInDay = 1440;
            if (StartTime.TotalMinutes < minutesInDay) return true;
            else return false;
        }

        /// <summary>
        /// Ensure that end time of activity is between (0:00 -- 23:59) o'clock
        /// </summary>
        public bool ValidateAccuracyEndTimeValue()
        {
            var minutesInDay = 1440;
            if (EndTime.TotalMinutes < minutesInDay) return true;
            else return false;
        }

        /// <summary>
        /// Ensure that "start time" is before "end time"
        /// </summary>
        public bool ValidateEndTimeAfterStartTime()
        {
            if (StartTime.TotalMinutes < EndTime.TotalMinutes) return true;
            else return false;
        }
    }
}
