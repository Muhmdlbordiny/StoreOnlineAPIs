
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreCore.G02.Mapping.Products;
using StoreCore.G02.RepositriesContract;
using StoreOnline.G02.Error;
using StoreOnline.G02.MiddleWares;
using StoreRepositry.G02;
using StoreRepositry.G02.Data.Contexts;
using StoreRepositry.G02.Data.DataSeeds;
using StoreService.G02.Services.Producted;
using static System.Runtime.InteropServices.JavaScript.JSType;
using StoreOnline.G02.Helper;
namespace StoreOnline.G02
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDependency(builder.Configuration);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            

            var app = builder.Build();
            await app.ConfigureMiddleWareAsync();
            app.Run();

        }
    }
}
