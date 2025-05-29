using DomainLayer.Contracts;
using DomainLayer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Data.Seed;
using Persistence.Repositories;
using StackExchange.Redis;

namespace Persistence
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("StoreConnection"));
            });
            services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });

            services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var connection = configuration.GetConnectionString("redis");
                return ConnectionMultiplexer.Connect(connection!);
            });
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IDataSeeding, DataSeeding>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();
            return services;
        }
    }
}
