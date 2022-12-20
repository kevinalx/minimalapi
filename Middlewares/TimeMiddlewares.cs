using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

public class TimeMiddlewares{
    readonly  RequestDelegate next;

    public TimeMiddlewares(RequestDelegate nextRequest)
    {
        next = nextRequest;

    }

    public async Task Invoke(HttpContext context)
    {

        await next(context);

        if (context.Request.Query.Any(p=> p.Key == "time"))
        {
            await context.Response.WriteAsync(DateTime.Now.ToShortTimeString());
        }

    }
}

 public static class TimeMiddlewareExtension
    {
        public static IApplicationBuilder UseTimeMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TimeMiddlewares>();
        }

    }
