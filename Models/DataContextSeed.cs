using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CountryDashboard.Models
{
    public class DataContextSeed
    {
        public static void SeedDatabase(CountryDashboardContext context, IServiceProvider services)
        {
            // Get a logger
            var logger = services.GetRequiredService<ILogger<DataContextSeed>>();

            if (!context.Countries.Any())
            {
                var countries = new List<Countries>();

                countries.Add(new Countries()
                {
                    Name = "Brazil",
                    CountryCode = "BR",
                });
                countries.Add(new Countries()
                {
                    Name = "Egypt",
                    CountryCode = "EG",
                });
                countries.Add(new Countries()
                {
                    Name = "Italy",
                    CountryCode = "IT",
                });
                countries.Add(new Countries()
                {
                    Name = "Monaco",
                    CountryCode = "MC",
                });
                countries.Add(new Countries()
                {
                    Name = "South Africa",
                    CountryCode = "BR",
                });
                countries.Add(new Countries()
                {
                    Name = "United States",
                    CountryCode = "US",
                });

                context.Countries.AddRange(countries);
                context.SaveChanges();
                logger.LogInformation("Database successfully seeded");
            }
        }
    }
}
