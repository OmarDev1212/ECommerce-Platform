using DomainLayer.Contracts;
using DomainLayer.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.Text.Json;

namespace Persistence.Data.Seed
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
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
                    await _dbContext.SaveChangesAsync();

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
                    await _dbContext.SaveChangesAsync();

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
                    await _dbContext.SaveChangesAsync();

                }

            }


        }
    }
}
