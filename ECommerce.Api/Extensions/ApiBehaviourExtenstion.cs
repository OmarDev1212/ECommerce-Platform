using Microsoft.AspNetCore.Mvc;
using Shared.Errors;

namespace ECommerce.Api.Extensions
{
    public static class ApiBehaviourExtenstion
    {
        public static void ConfigureValidationErrorResponse(this ApiBehaviorOptions options)
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Any())
                    .SelectMany(e => e.Value.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                var response = new ValidationErrorResponse()
                {
                    Errors = errors,
                    StatusCode = 400
                };

                return new BadRequestObjectResult(response);
            };
        }
    }
}
