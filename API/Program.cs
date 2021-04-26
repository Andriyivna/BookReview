using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace API
{
    public class Program
    {
<<<<<<< HEAD
        public static async Task Main(string[] args)
=======
      public static void Main(string[] args)
>>>>>>> ccae311812251f519d42b999b10c866d9fb3c0df
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = services.GetRequiredService<AppDbContext>();
                    await context.Database.MigrateAsync();

                    var userManager = services.GetRequiredService<UserManager<User>>();
                    await AppDbContextSeed.SeedUsersAsync(userManager);
<<<<<<< HEAD
                    await AppDbContextSeed.SeedDataAsync(context, loggerFactory);
=======
                    await AppDbContextSeed.SeedDataAsync(context, userManager, loggerFactory);
>>>>>>> ccae311812251f519d42b999b10c866d9fb3c0df
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occured during migration");
                }
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
