using System;
using System.ComponentModel.DataAnnotations;

namespace CampusConnect.Application.Shared;

public class ValidUrlArrayAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var urls = value as string[];
        if (urls == null || urls.Length == 0)
        {
            return ValidationResult.Success;
        }

        foreach (var url in urls)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uriResult) ||
                !(uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            {
                return new ValidationResult($"The URL '{url}' is not valid.");
            }
        }

        return ValidationResult.Success;
    }
}