using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Profiles;
using ServiceAbstractions;
using Shared.Errors;

namespace ECommerce.Api.Extensions
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductProfile).Assembly);
            services.AddScoped<IServiceManager, ServiceManager>();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.ConfigureValidationErrorResponse();
            });
            return services;
        }

    }
}
