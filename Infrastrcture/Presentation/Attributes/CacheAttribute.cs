using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstractions;
using System.Text;

namespace ECommerce.Api.Attributes
{
    public class CacheAttribute(int TimeToLiveInSeconds=90) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //create cache key
            //search for value with cache key
            //if value is found, return value if not null
            //  next.Invoke();
            //set value in cache with cache key

            var service = context.HttpContext.RequestServices.GetService<ICachingService>();
            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cacheValue = await service!.GetAsync(cacheKey);
            if (cacheValue is not null)
            {
                //return cached value
                context.Result = new ContentResult
                {
                    Content = cacheValue,
                    ContentType = "application/json",
                    StatusCode= StatusCodes.Status200OK   
                };
                return;
            }

            var executedEndpointContext = await next.Invoke(); //execute endpoint
            if (executedEndpointContext.Result is OkObjectResult okObjectResult)
            {
                await service.SetAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(TimeToLiveInSeconds));
            }
        }
        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append(request.Path+'?');

            foreach (var item in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"{item.Key}={item.Value}");
            }
            return keyBuilder.ToString();
        }
    }

}



