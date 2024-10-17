using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using StoreCore.G02.Entites.Identity;
using StoreCore.G02.Mapping.Basket;
using StoreCore.G02.Mapping.Products;
using StoreCore.G02.RepositriesContract;
using StoreOnline.G02.Error;
using StoreRepositry.G02;
using StoreRepositry.G02.Data.Contexts;
using StoreRepositry.G02.identity.Contexts;
using StoreRepositry.G02.Repositries;
using StoreService.G02.Services.Basket;
using StoreService.G02.Services.Cashes;
using StoreService.G02.Services.Producted;
using StoreService.G02.Services.Token;
using StoreService.G02.Services.User;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using StoreCore.G02.Mapping.Auth;

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
            services.AddIdentityService(); 
            services.AddAuthenticatinService(configuration);
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
            services.AddDbContext<StoreIdentityDbContext>
                (options =>
                {
                    options.UseSqlServer
                    (
                        configuration.
                        GetConnectionString("IdentityConnection")
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
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<ICashService,CashService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
        private static IServiceCollection AddAutoMapperService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAutoMapper(M =>
           M.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(M => M.AddProfile(new BasketProfile()));
            services.AddAutoMapper(M=>M.AddProfile(new AuthoProfile()));
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
        private static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            services.AddIdentity<Appuser, IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();
            //services.AddAuthentication();// usermanger,signinmanager ,RoleManger
            return services;
        }
        private static IServiceCollection AddAuthenticatinService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // install pakage jwtbarrer
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // install pakage jwtbarrer
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            })
                ;// usermanger,signinmanager ,RoleManger
            return services;
        }

    }
}
