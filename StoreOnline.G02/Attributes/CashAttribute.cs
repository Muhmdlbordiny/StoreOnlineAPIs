using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using StoreCore.G02.RepositriesContract;
using System.Text;

namespace StoreOnline.G02.Attributes
{
    public class CashAttribute:Attribute,IAsyncActionFilter
    {
        private readonly int _expireTime;

        public CashAttribute(int expireTime)
        {
            _expireTime = expireTime;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
          var cashservice =  context.HttpContext.RequestServices.GetRequiredService<ICashService>();
            var cashkey = GenerateCashKeyFromRequest(context.HttpContext.Request);
          var cashResponse = await cashservice.GetCashkeyAsync(cashkey);
            if (!string.IsNullOrEmpty(value: cashResponse))
            {
                var contentResult = new ContentResult()
                {
                    Content = cashResponse,
                    ContentType = "application/json",
                    StatusCode =200
                };
                context.Result = contentResult;
                return;
            }
           var executedContext =  await next();
            if (executedContext.Result is OkObjectResult response)
            {
               await cashservice.SetCashAsync(cashkey, response.Value,TimeSpan.FromHours(_expireTime));
            }
        }
        private string GenerateCashKeyFromRequest(HttpRequest request)
        {
            var cashkey = new StringBuilder();
            cashkey.Append($"{request.Path}");
            foreach(var (key,value) in request.Query.OrderBy(x=>x.Key))
            {
                cashkey.Append($"|{key}_{value}");
            }
            return cashkey.ToString();
        }
    }
}
