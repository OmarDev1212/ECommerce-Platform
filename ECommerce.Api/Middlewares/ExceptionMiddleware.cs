using Shared.Errors;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace ECommerce.Api.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate _next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);

            }
            catch (Exception ex)
            {
                //1.set status code
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                //2. change response type from string to json
                context.Response.ContentType = "application/json";
                //3. create custom response object
                var response = new ApiExceptionResponse()
                {
                    StatusCode = 500,
                    ErrorMessage = ex.Message
                };
                //4.return objec as json
                await context.Response.WriteAsync(JsonSerializer.Serialize(response, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }));
            }

        }
    }
}

