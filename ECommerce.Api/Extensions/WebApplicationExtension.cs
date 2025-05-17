using DomainLayer.Contracts;
using ECommerce.Api.Middlewares;

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
            return app;
        }
    }
}
