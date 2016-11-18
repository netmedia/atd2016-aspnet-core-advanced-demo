using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace ATD2016
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }















    public class StartupSimple
    {
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("1. middleware request" + Environment.NewLine + Environment.NewLine);

                await next();

                await context.Response.WriteAsync("1. middleware response" + Environment.NewLine + Environment.NewLine);
            });

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("2. middleware request" + Environment.NewLine + Environment.NewLine);

                await next();

                await context.Response.WriteAsync("2. middleware response" + Environment.NewLine + Environment.NewLine);
            });

            app.Run(async (context) =>
            {

                foreach (var address in app.ServerFeatures.Get<IServerAddressesFeature>().Addresses)
                {
                    await context.Response.WriteAsync(address + Environment.NewLine);
                }

                var config = new ConfigurationBuilder().AddEnvironmentVariables().Build();

                await context.Response.WriteAsync("ASPNETCORE_PORT: " + config["ASPNETCORE_PORT"] + Environment.NewLine);
                await context.Response.WriteAsync("ASPNETCORE_APPL_PATH: " + config["ASPNETCORE_APPL_PATH"] + Environment.NewLine);
                await context.Response.WriteAsync("ASPNETCORE_TOKEN: " + config["ASPNETCORE_TOKEN"] + Environment.NewLine);

                await context.Response.WriteAsync(Environment.NewLine);
            });
        }
    }
}
