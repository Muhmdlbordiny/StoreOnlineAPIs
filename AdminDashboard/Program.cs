using AdminDashboard.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreCore.G02.Entites.Identity;
using StoreCore.G02.RepositriesContract;
using StoreRepositry.G02;
using StoreRepositry.G02.Data.Contexts;
using StoreRepositry.G02.identity.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StoreDbContext>
                (options =>
                {
                    options.UseSqlServer
                    (
                       builder.Configuration.GetConnectionString("DefaultConnection") 
                     );
                }
                );
   builder.Services.AddDbContext<StoreIdentityDbContext>
            (options =>
            {
                options.UseSqlServer
                (
                   builder.Configuration.GetConnectionString("IdentityConnection")
                 );
             }
            );
builder.Services.AddIdentity<Appuser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
        })
                .AddEntityFrameworkStores<StoreIdentityDbContext>();
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddAutoMapper(M=>M.AddProfile(new MapsProfile()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Login}/{id?}");

app.Run();
