using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountryDashboard.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CountryDashboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                logger.LogInformation("AutomaticMigration Enabled, attempting to migrate");
                var context = services.GetRequiredService<CountryDashboardContext>();

                if (context.Database.IsSqlServer())
                {
                    if (!context.Database.CanConnect())
                    {
                        logger.LogInformation("No database exists");
                    }

                    var pendingMigrations = context.Database
                        .GetPendingMigrations();
                    var pendingMigrationCount = pendingMigrations.Count();
                    if (pendingMigrationCount > 0)
                    {
                        var pending = pendingMigrations
                            .Aggregate((first, next) => first + "\n" + next);
                        logger.LogInformation("Pending Migrations: {Migrations}", pending);
                        logger.LogInformation("Applying {Count} Migrations", pendingMigrationCount);
                        context.Database.Migrate();
                    }
                    else
                    {
                        logger.LogInformation("No pending migrations found. Database is up to date.");
                    }
                }

                DataContextSeed.SeedDatabase(context, services);
            }

            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError("An error occurred while seeding the database");
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
