using System;
using HW_26.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HW_26
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var sc = host.Services.CreateScope())
            {
                var ser = sc.ServiceProvider;

                try
                {
                    var context = ser.GetRequiredService<DateDb>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = ser.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Error migration");
                }
            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
