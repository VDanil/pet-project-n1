using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Domain
{
    public class Subscription
    {
        [Key]
        public int SubscriptionId { get; set; }

        [Required]
        [Subscription_EnsureBuyDateBeforeStartDate]
        public DateTime? BuyDate { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        
        [Required]
        [Subscription_EnsureAccuracyDurationInDays]
        [Subscription_EnsureDurationInDaysIsProportionalToVisitingAmount]
        public int DurationInDays { get; set; }

        [Required]
        [Subscription_EnsureAccuracyVisitingAmount]
        public int VisitingAmount { get; set; }

        public bool IsFreezed { get; set; } = false;
        public DateTime? FreezeDate { get; set; }
        public int FreezeDaysAmount { get; set; } = 0;

        public int? Price { get; set; }
        // navigation properties
        public virtual int? GroupId { get; set; }
        public virtual Group? Group { get; set; }

        public virtual int? ClientId { get; set; }
        public virtual Client? Client { get; set; }

        public virtual List<Activity>? Activities { get; set; }
        public virtual List<Visit>? Visits { get; set; }

        // Validation
        /// <summary>
        /// Ensure that <<Buy Date>> of subscription is before or equal to <<Start Date>>
        /// </summary>
        public bool ValidateBuyDateBeforeStartDate()
        {
            if (!BuyDate.HasValue) return false;
            if (!StartDate.HasValue) return true;

            if (BuyDate.Value <= StartDate.Value) return true;
            else return false;
        }

        /// <summary>
        /// Ensure that number of <<DurationInDays>> is greater than 0 
        /// </summary>
        public bool ValidateAccuracyDurationInDays()
        {

            if (DurationInDays > 0) return true;
            else return false;
        }

        /// <summary>
        /// Ensure that number of <<VisitingAmount>> is greater than 0
        /// </summary>
        public bool ValidateAccuracyVisitingAmount()
        {
            if (VisitingAmount > 0) return true;
            else return false;
        }

        /// <summary>
        /// Ensure that subscription gives opportunity to full fill it, 
        /// i.e. duration of subscription is enough to make all visits
        /// </summary>
        public bool ValidateDurationInDaysIsProportionalToVisitingAmount()
        {
            if(DurationInDays >= VisitingAmount) return true;
            else return false;
        }
    }
}
