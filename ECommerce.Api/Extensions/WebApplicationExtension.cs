using DomainLayer.Contracts;
using ECommerce.Api.Middlewares;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json;

namespace ECommerce.Api.Extensions
{
    public static class WebApplicationExtension
    {
        public static async Task<WebApplication> GetApplication(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            using var scope = app.Services.CreateScope();

            var service = scope.ServiceProvider.GetRequiredService<IDataSeeding>();

            await service.SeedAsync();
            await service.SeedIdentityAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.EnablePersistAuthorization();
                    options.DocumentTitle = "E-Commerce.Api";
                    options.EnableFilter();
                    options.DocExpansion(DocExpansion.None);
                    options.EnableValidator();
                    options.DisplayRequestDuration();
                    options.JsonSerializerOptions = new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy= JsonNamingPolicy.CamelCase,

                    };
                    options.InjectStylesheet("\\swagger-ui\\SwaggerDark.css");
                });
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("All");
            app.UseAuthentication();
            app.UseAuthorization(); 

            app.MapControllers();

            app.Run();
            return app;
        }
    }
}
