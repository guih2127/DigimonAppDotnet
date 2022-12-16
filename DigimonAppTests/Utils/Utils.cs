using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DigimonAppTests.Utils
{
    public static class Utils
    {
        public static void ValidateResourceForTests<T>(T resource, Controller controller)
        {
            if (resource is not null)
            {
                var validationContext = new ValidationContext(resource, null, null);
                var validationResults = new List<ValidationResult>();
                Validator.TryValidateObject(resource, validationContext, validationResults, true);
                foreach (var validationResult in validationResults)
                {
                    if (validationResult.ErrorMessage is not null)
                        controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
                }
            }
        }
    }
}
