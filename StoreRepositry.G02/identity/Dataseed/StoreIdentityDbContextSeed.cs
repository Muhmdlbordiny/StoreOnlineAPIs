using Microsoft.AspNetCore.Identity;
using StoreCore.G02.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreRepositry.G02.identity.Dataseed
{
    public static class StoreIdentityDbContextSeed
    {
        public async static Task SeedAppUserAsync(UserManager<Appuser> _userManager)
        {
            if (_userManager.Users.Count()==0 )
            {
                var User = new Appuser
                {
                    Email = "Mohamed@gmail.com",
                    DisplayName = "Mohamed",
                    UserName = "MohamedAshraf",
                    PhoneNumber = "01278536501",
                    Address = new Address()
                    {
                        FName = "Mohamed",
                        LName = "Ashraf",
                        City = "Mahmodia",
                        Country = "egypt",
                        Street = "mbjdsn"

                    }
                };
               var result = await _userManager.CreateAsync(User, "P@ssword55");
                if (result.Succeeded)
                    Console.WriteLine("HelloWorld");
                
            }
        }
    }
}
