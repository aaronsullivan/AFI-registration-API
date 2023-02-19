using Lamar;
using Microsoft.AspNetCore.Diagnostics;

namespace Registration.API.Middleware;

public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder SetupExceptionHandling(
        this IApplicationBuilder app,
        IWebHostEnvironment env)
    {
        var genericError = "Sorry, something went wrong. Please try again later.";

        if (env.IsDevelopment())
        {
            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()?
                    .Error;

                context.Response.StatusCode = 500;
                var response = new { code = context.Response.StatusCode, messages = new List<string> { exception?.Message ?? genericError } };
                await context.Response.WriteAsJsonAsync(response);
            }));
        }
        else
        {
            app.UseExceptionHandler(c => c.Run(async context =>
            {
                context.Response.StatusCode = 500;

                var response = new { code = context.Response.StatusCode, messages = new List<string> { genericError } };
                await context.Response.WriteAsJsonAsync(response);
            }));
        }

        return app;
    }
}