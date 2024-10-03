
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreCore.G02.Mapping.Products;
using StoreCore.G02.RepositriesContract;
using StoreRepositry.G02;
using StoreRepositry.G02.Data.Contexts;
using StoreRepositry.G02.Data.DataSeeds;
using StoreService.G02.Services.Producted;

namespace StoreOnline.G02
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
            builder.Services.AddDbContext<StoreDbContext>
                (options => 
                {
                    options.UseSqlServer
                    (
                        builder.Configuration.
                        GetConnectionString("DefaultConnection")
                     );
                }
                );
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(M =>
            M.AddProfile(new ProductProfile()));

            var app = builder.Build();
           using var scope = app.Services.CreateScope();
            var services= scope.ServiceProvider;
            var context = services.GetRequiredService<StoreDbContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await context.Database.MigrateAsync();
                await StoreDbContextSeed.SeedAsync(context);
            }catch(Exception ex)
            {
               var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "There are  problems applying Migrations!");
            }

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
