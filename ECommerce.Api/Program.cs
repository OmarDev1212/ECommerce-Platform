using ECommerce.Api.Extensions;

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
            //Infrastcture Registration
            builder.Services.AddInfrastructureServices(builder.Configuration);
            //Application services Registration
            builder.Services.AddApplicationServices();

            var app = builder.Build();

            await app.GetApplication();
        }
    }
}
