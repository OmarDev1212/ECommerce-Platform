using DomainLayer.Contracts;
using DomainLayer.Models.Identity;
using DomainLayer.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.Text.Json;

namespace Persistence.Data.Seed
{
    public class DataSeeding(StoreDbContext _dbContext,
                            UserManager<ApplicationUser> userManager,
                            RoleManager<IdentityRole> roleManager) : IDataSeeding
    {
        public async Task SeedAsync()
        {

            if (_dbContext.Database.GetPendingMigrations().Any())
            {
                await _dbContext.Database.MigrateAsync();
            }
            if (!_dbContext.ProductBrands.Any())
            {
                //1.ReadFiles

                var jsonBrands = File.ReadAllText(@"../Infrastrcture\Persistence\Data\Seed\JsonData\brands.json");
                //2.Converting json object to C# objects

                var brandsData = JsonSerializer.Deserialize<List<ProductBrand>>(jsonBrands);
                //3.add data to database

                if (brandsData != null && brandsData.Count > 0)
                {
                    await _dbContext.AddRangeAsync(brandsData);

                }
            }

            if (!_dbContext.ProductTypes.Any())
            {
                //1.ReadFiles

                var jsonTypes = File.ReadAllText(@"../Infrastrcture\Persistence\Data\Seed\JsonData\types.json");
                //2.Converting json object to C# objects

                var typesData = JsonSerializer.Deserialize<List<ProductType>>(jsonTypes);
                //3.add data to database

                if (typesData != null && typesData.Count > 0)
                {
                    await _dbContext.AddRangeAsync(typesData);

                }

            }

            if (!_dbContext.Products.Any())
            {
                //1.ReadFiles
                var proudcts = File.ReadAllText(@"../Infrastrcture\Persistence\Data\Seed\JsonData\products.json");
                //2.Converting json object to C# objects

                var productsData = JsonSerializer.Deserialize<List<Product>>(proudcts);
                //3.add data to database

                if (productsData != null && productsData.Count > 0)
                {
                    await _dbContext.AddRangeAsync(productsData);

                }

            }
            await _dbContext.SaveChangesAsync();

        }

        public async Task SeedIdentityAsync()
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }
            if (!userManager.Users.Any())
            {
                var user01 = new ApplicationUser()
                {
                    Email = "Mohamed@gmail.com",
                    PhoneNumber = "01234567890",
                    DisplayName = "MohamedAhmed",
                    UserName = "MohamedAhmed"
                };
                var user02 = new ApplicationUser()
                {
                    Email = "Salma@gmail.com",
                    PhoneNumber = "01234567890",
                    DisplayName = "SalmaAhmed",
                    UserName = "SalmaAhmed"
                };
                await userManager.CreateAsync(user01, "P@ssW0rd");
                await userManager.CreateAsync(user02, "P@ssW0rd");
                await userManager.AddToRoleAsync(user01, "Admin");
                await userManager.AddToRoleAsync(user02, "SuperAdmin");
            }

        }
    }
}
