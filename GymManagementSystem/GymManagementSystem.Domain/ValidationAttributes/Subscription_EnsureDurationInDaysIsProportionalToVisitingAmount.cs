using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Domain
{
    public class Subscription_EnsureDurationInDaysIsProportionalToVisitingAmount : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var subscription = validationContext.ObjectInstance as Subscription;

            if (!subscription.ValidateDurationInDaysIsProportionalToVisitingAmount())
                return new ValidationResult("<<DurationInDays>> should de more or equal to <<VisitingAmount>>");

            return ValidationResult.Success;
        }
    }
}
