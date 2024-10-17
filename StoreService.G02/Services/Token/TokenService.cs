using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StoreCore.G02.Entites.Identity;
using StoreCore.G02.RepositriesContract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StoreService.G02.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(Appuser user, UserManager<Appuser> userManager)
        {
            //1.header (Algorithm,type)
            //2.payload claims
            //.3 signture
                 var auhoclamis = new List<Claim>()
                 {
                     new Claim(ClaimTypes.Email,user.Email),
                     new Claim(ClaimTypes.GivenName,user.DisplayName),
                     new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),
                 };
                 var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                auhoclamis.Add(new Claim(ClaimTypes.Role, role));
            }
                var secuirtykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    expires: DateTime.Now.AddDays(double.Parse(_configuration["Jwt:DurationInDays"])),
                claims: auhoclamis,
                    signingCredentials: new SigningCredentials(secuirtykey, SecurityAlgorithms.HmacSha256Signature)
                    );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        
    }
}
