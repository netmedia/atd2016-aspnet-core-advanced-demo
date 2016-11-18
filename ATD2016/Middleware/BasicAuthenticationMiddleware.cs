using ATD2016.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;

namespace ATD2016.Middleware
{
    public class BasicAuthenticationMiddleware
    {
        private readonly RequestDelegate next;

        public BasicAuthenticationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, IAuthenticationService authenticationService, IBasicAuthenticationParserService basicAuthenticationParserService)
        {
            try
            {
                var parsedResult = basicAuthenticationParserService.Parse(context);

                await authenticationService.AuthenticateAsync(parsedResult.Username, parsedResult.Password);
                await next(context);
            }
            catch (InvalidCredentialException)
            {
                context.Response.StatusCode = 401;
                context.Response.Headers.Add("WWW-Authenticate", new[] { "Basic" });
            }
        }
    }

    public static partial class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseBasicAuthentication(this IApplicationBuilder app)
        {
            return app.UseMiddleware<BasicAuthenticationMiddleware>();
        }
    }

    public static partial class ServicesCollectionExtensions
    {
        public static void AddBasicAuthentication(this IServiceCollection services)
        {
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IBasicAuthenticationParserService, BasicAuthenticationParserService>();
        }
    }
}






