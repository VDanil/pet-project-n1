using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Domain
{
    public class Activity_EnsureAccuracyStartTimeValue : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var activity = validationContext.ObjectInstance as Activity;

            if (!activity.ValidateAccuracyStartTimeValue())
                return new ValidationResult("<<Start Time>> forms is wrong. " +
                                            "Start time should be between (0:00 -- 23:59) o'clock");

            return ValidationResult.Success;
        }
    }
}
