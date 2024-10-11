using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using StoreOnline.G02.MiddleWares;
using StoreRepositry.G02.Data.Contexts;
using StoreRepositry.G02.Data.DataSeeds;

namespace StoreOnline.G02.Helper
{
    public static class ConfigureMiddleware
    {
        public static async Task<WebApplication> ConfigureMiddleWareAsync (this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<StoreDbContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await context.Database.MigrateAsync();
                await StoreDbContextSeed.SeedAsync(context);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "There are  problems applying Migrations!");
            }
            app.UseMiddleware<ExceptionMiddleware>();//Configure User-Defined MiddleWare

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            return app;

        }
    }
}
