using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Domain
{
    public class Subscription_EnsureAccuracyDurationInDays : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var subscription = validationContext.ObjectInstance as Subscription;

            if (!subscription.ValidateAccuracyDurationInDays())
                return new ValidationResult("<<DurationInDays>> should de more than 0");

            return ValidationResult.Success;
        }
    }
}
