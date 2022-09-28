using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Domain
{
    public class Activity_EnsureAccuracyEndTimeValue : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var activity = validationContext.ObjectInstance as Activity;

            if (!activity.ValidateAccuracyEndTimeValue())
                return new ValidationResult("<<End Time>> forms is wrong. " +
                                            "End time should be between (0:00 -- 23:59) o'clock");

            return ValidationResult.Success;
        }
    }
}
