using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.RepositriesContract
{
    public interface ICashService
    {
        Task SetCashAsync(string key, object response, TimeSpan expiretime);
        Task<string>GetCashkeyAsync(string  key);
    }
}
