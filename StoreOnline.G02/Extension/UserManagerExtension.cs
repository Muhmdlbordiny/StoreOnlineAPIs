using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreCore.G02.Entites.Identity;
using StoreOnline.G02.Error;
using System.Security.Claims;

namespace StoreOnline.G02.Extension
{
    public static class UserManagerExtension
    {
         public static async Task<Appuser> FindByEmailWithAddressAsync(this UserManager<Appuser> usermanager,ClaimsPrincipal User)
        {
            var useremail = User.FindFirstValue(ClaimTypes.Email);
            if (useremail is null) return null;
            var user =await usermanager.Users.Include(u => u.Address)
                               .FirstOrDefaultAsync(u => u.Email == useremail);
            if (user is null) return null;
            return user;
        }
    }
}
