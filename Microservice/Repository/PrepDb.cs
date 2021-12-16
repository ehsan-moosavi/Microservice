using Microservice.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.Repository
{
    public static class PrepDb
    {
        public static void PrepPulation(IApplicationBuilder app,bool isprod)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(),isprod);
            }

        }
        private static void SeedData(AppDbContext context,bool isprod)
        {
            if (isprod)
            {
                Console.Write("---> Atemptting to Apply migrations");
                try { context.Database.Migrate(); }
                catch(Exception ex)
                {
                    Console.WriteLine($"----->Could not migration:{ex.Message}");
                }
               
            }
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
