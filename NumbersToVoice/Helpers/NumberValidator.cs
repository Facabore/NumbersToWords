using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using NumbersToVoice.Entities;


namespace NumbersToVoice.Helpers;

public class NumberValidator
{
    public static IActionResult?   ValidateNumber(NumberRequest numberRequest)
    {
        var validationContext = new ValidationContext(numberRequest, null, null);
        var validationResults = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(numberRequest, validationContext, validationResults, true);

        if (isValid) return null; // No Errors
        
        // Show errors messagge
        var errorMessage = string.Join(", ", validationResults.Select(r => r.ErrorMessage));
        return new BadRequestObjectResult(errorMessage);

    }
}