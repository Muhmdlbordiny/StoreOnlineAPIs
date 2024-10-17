using Microsoft.AspNetCore.Identity;
using StoreCore.G02.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.RepositriesContract
{
    public interface ITokenService
    {
        Task <string>  CreateTokenAsync(Appuser user,UserManager<Appuser> userManager);
    }
}
