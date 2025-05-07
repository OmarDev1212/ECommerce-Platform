
using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Data.Seed;
using Persistence.Repositories;
using Service;
using Service.Profiles;
using ServiceAbstractions;
using System.Threading.Tasks;

namespace ECommerce.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
            });
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            var app = builder.Build();

            using var scope = app.Services.CreateScope();

            var service = scope.ServiceProvider.GetRequiredService<IDataSeeding>();

            await service.SeedAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
