using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Domain
{
    public class Subscription_EnsureAccuracyVisitingAmount : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var subscription = validationContext.ObjectInstance as Subscription;

            if (!subscription.ValidateAccuracyVisitingAmount())
                return new ValidationResult("<<VisitingAmount>> should de more than 0");

            return ValidationResult.Success;
        }
    }
}
