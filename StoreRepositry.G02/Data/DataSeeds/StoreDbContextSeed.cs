using StoreCore.G02.Entites;
using StoreRepositry.G02.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StoreRepositry.G02.Data.DataSeeds
{
    public static class StoreDbContextSeed
    {
        public static async Task SeedAsync(StoreDbContext _context)
        {
           if(_context.Brands.Count() == 0)
            {
                //Brand
                //1.Read data from json file
                var brandData =  File.ReadAllText
                      (@"..\StoreRepositry.G02\Data\SeedsFiles\brands.json");
                //2. convert json string  to List<T>
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                //seed data to database
                if (brands is not null && brands.Count() > 0)
                {
                    await _context.Brands.AddRangeAsync(brands);
                    await _context.SaveChangesAsync();


                }
            }
            if (_context.Types.Count() == 0)
            {
                //Brand
                //1.Read data from json file
                var TypeData =  File.ReadAllText
                      (@"..\StoreRepositry.G02\Data\SeedsFiles\types.json");
                //2. convert json string  to List<T>
                var types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);
                //seed data to database
                if (types is not null && types.Count() > 0)
                {
                    await _context.Types.AddRangeAsync(types);
                    await _context.SaveChangesAsync();


                }
            }
            if (_context.Products.Count() == 0)
            {
              var productData=  File.ReadAllText
                    (@"..\StoreRepositry.G02\Data\SeedsFiles\products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productData);
                if(products is not null && products.Count() > 0)
                {
                   await _context.Products.AddRangeAsync(products);
                    await _context.SaveChangesAsync();

                }
            }

        }
    }
}
