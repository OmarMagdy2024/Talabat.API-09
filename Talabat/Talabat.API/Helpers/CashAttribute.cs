using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Talabat.Core.ServicesContract;

namespace Talabat.API.Helpers
{
    public class CashAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _expiretimesecond;

        public CashAttribute(int expiretimesecond)
        {
            _expiretimesecond = expiretimesecond;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cashservice = context.HttpContext.RequestServices.GetRequiredService<IcashService>();
            var cashkey = GenerateKey(context.HttpContext.Request);
            var response= await cashservice.GetCashAsync(cashkey);
            if (!string.IsNullOrEmpty(response))
            {
                var result = new ContentResult()
                {
                    Content= response,
                    ContentType = "application/json",
                    StatusCode=200
                };
                context.Result = result;
                return;
            }
            var excuted=await next.Invoke();
            if (excuted.Result is OkObjectResult result1)
            {
               await cashservice.CashAsync(cashkey,result1.Value,TimeSpan.FromSeconds(_expiretimesecond));
            }
        }

        private string GenerateKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path);
            foreach (var item in request.Query.OrderBy(x=>x.Key))
            {
                key.Append($"{item.Key}-{item.Value}");
            }
            return key.ToString();
        }
    }
}
