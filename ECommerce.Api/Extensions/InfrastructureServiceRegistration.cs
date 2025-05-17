using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Data.Seed;
using Persistence.Repositories;
using StackExchange.Redis;

namespace ECommerce.Api.Extensions
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("StoreConnection"));
            });

            services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var connection = configuration.GetConnectionString("redis");
                return ConnectionMultiplexer.Connect(connection!);
            });
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IDataSeeding, DataSeeding>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
