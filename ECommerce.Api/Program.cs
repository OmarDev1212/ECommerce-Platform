using ECommerce.Api.Extensions;
using Microsoft.OpenApi.Models;
using Persistence;
using Service;
using Swashbuckle.AspNetCore.SwaggerGen;

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

            builder.Services.AddSwaggerGen(AddSwaggerOptions());
            //Infrastcture Registration
            builder.Services.AddInfrastructureServices(builder.Configuration);
            //Application services Registration
            builder.Services.AddApplicationServices(builder.Configuration);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("All", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                });
            });
            var app = builder.Build();

            await app.GetApplication();
        }

        private static Action<SwaggerGenOptions> AddSwaggerOptions()
        {
            return options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    Description = "Please Enter 'Bearer' Followed By Space then Your Token "
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                     {
                        {
                                new OpenApiSecurityScheme()
                                {
                                    Reference= new OpenApiReference()
                                    {
                                        Id="Bearer",
                                        Type= ReferenceType.SecurityScheme
                                    }
                                },
                                new string[]{}
                         }
             });
            };
        }
    }
}
