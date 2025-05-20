
using System.Diagnostics;

namespace Restaurants.API.Middlewares
{
    public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            Stopwatch stopwatch= Stopwatch.StartNew();
            await next.Invoke(context);
            stopwatch.Stop();
            //check if the request took more than 250ms
            if (stopwatch.ElapsedMilliseconds > 1000 / 4) {
                logger.LogInformation("Request [{Verb}] at [{Path}] took [{time}] ms", context.Request.Method,context.Request.Path,stopwatch.ElapsedMilliseconds);
            }


        }
    }
}
