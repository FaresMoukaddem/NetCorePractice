using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TechParts.API.Data;
using Microsoft.Extensions.Configuration;

namespace TechParts.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Development.json", optional: false)
            .Build();

            var host = CreateHostBuilder(args).Build();

            if(config.GetValue<bool>("GlobalBooleans:SeedRandomData"))
            {
                using(var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    try
                    {
                        var context = services.GetRequiredService<DataContext>();
                        Seeder.SeedData(context);
                    }
                    catch (Exception e)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(e, "An error occured during seeding");
                    }
                }
            }
            else
            {
                System.Console.WriteLine("Not seeding data...");
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
