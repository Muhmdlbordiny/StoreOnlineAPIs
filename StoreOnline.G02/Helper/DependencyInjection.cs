using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using StoreCore.G02.Mapping.Basket;
using StoreCore.G02.Mapping.Products;
using StoreCore.G02.RepositriesContract;
using StoreOnline.G02.Error;
using StoreRepositry.G02;
using StoreRepositry.G02.Data.Contexts;
using StoreRepositry.G02.Repositries;
using StoreService.G02.Services.Producted;

namespace StoreOnline.G02.Helper
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependency(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddBuiltinService();
            services.AddSwagerService();
            services.AddDbcontextService(configuration);
            services.AddUserDefinedService();
            services.AddAutoMapperService(configuration);
            services.ConfigureInvaildModelStateResponse(configuration);
            services.AddRedisService(configuration);
            return services;
        }

        private static IServiceCollection AddBuiltinService(this IServiceCollection services)
        {
            services.AddControllers();

            return services;
        }
        private static IServiceCollection AddSwagerService(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        private static IServiceCollection AddDbcontextService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>
                (options =>
                {
                    options.UseSqlServer
                    (
                        configuration.
                        GetConnectionString("DefaultConnection")
                     );
                }
                );
            return services;
        }
        private static IServiceCollection AddUserDefinedService(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepositry, BasketRepositry>();
            return services;
        }
        private static IServiceCollection AddAutoMapperService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAutoMapper(M =>
           M.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(M => M.AddProfile(new BasketProfile()));
            return services;
        }
        private static IServiceCollection ConfigureInvaildModelStateResponse
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.
                    Where(p => p.Value.Errors.Count() > 0)
                                            .SelectMany(p => p.Value.Errors)
                                            .Select(E => E.ErrorMessage).ToArray();
                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            });
            return services;
        }

        private static IServiceCollection AddRedisService (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>((serviceprovider) =>
            {
                var connection = configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            }

            );
            
            return services;
        }

    }
}
