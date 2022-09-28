using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Domain
{
    public class Activity_EnsureEndTimeAfterStartTime : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var activity = validationContext.ObjectInstance as Activity;

            if (!activity.ValidateEndTimeAfterStartTime())
                return new ValidationResult("<<Start Time>> should be before <<End Time>>.");

            return ValidationResult.Success;
        } 
    }
}
