using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Domain
{
    public class Subscription_EnsureBuyDateBeforeStartDate : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var subscription = validationContext.ObjectInstance as Subscription;

            if (!subscription.ValidateBuyDateBeforeStartDate())
                return new ValidationResult("Buy date has to be before Report date.");

            return ValidationResult.Success;
        }
    }
}
