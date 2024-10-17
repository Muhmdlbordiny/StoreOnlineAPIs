using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoreCore.G02.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreRepositry.G02.identity.Contexts
{
    public class StoreIdentityDbContext:IdentityDbContext<Appuser>
    {
        public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options):base(options)
        {
            
        }
        
    }
}
