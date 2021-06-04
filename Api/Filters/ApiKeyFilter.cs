using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace TakeRepositoriesApi.Filters
{
    public class ApiKeyFilter : Attribute, IAsyncActionFilter
    {
        private const string ApiKeyName = "ApiKey";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyName, out var providedApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = configuration["ApiKey"];
            if (apiKey != providedApiKey)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}