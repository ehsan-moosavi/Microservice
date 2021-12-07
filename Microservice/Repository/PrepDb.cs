using Microservice.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.Repository
{
    public static class PrepDb
    {
        public static void PrepPulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }

        }
        private static void SeedData(AppDbContext context)
        {
            if (!context.Platforms.Any())
            {
                Console.WriteLine("--> Seeding Data");
                context.Platforms.AddRange(
                   new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "free" },
                   new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "free" },
                   new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "free" }
                    );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We Already have Data");
            }
        }
    }
}
