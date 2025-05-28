using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Service;
using Service.Profiles;
using ServiceAbstractions;
using Shared.Errors;
using System.Text;

namespace ECommerce.Api.Extensions
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(ProductProfile).Assembly);
            services.AddScoped<IServiceManager, ServiceManager>();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.ConfigureValidationErrorResponse();
            });
            AddAuthenticationWithJwtService(services, configuration);
            services.AddAuthorization();
            services.AddSwaggerServices();
            return services;
        }

        private static void AddAuthenticationWithJwtService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Audience = configuration["JWT:Audience"];
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!))

                };
            });
        }

        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
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
            });
            return services;
        }
    }
}