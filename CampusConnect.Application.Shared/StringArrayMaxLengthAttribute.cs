using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CampusConnect.Application.Shared;

public class StringArrayMaxLengthAttribute(int maxLength) : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string[] stringArray)
        {
            int totalLength = stringArray.Sum(x => x?.Length ?? 0);

            if (totalLength > maxLength)
            {
                return new ValidationResult($"The total length of all webpages exceeds {maxLength} characters.");
            }
        }

        return ValidationResult.Success;
    }
}