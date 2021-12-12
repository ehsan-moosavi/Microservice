using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microservice.Repository;
using Microsoft.Extensions.Configuration;
using Microservice.Interfaces;
using AutoMapper;
using Microservice.SyncDataServices.Http;

namespace Microservice
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            configuration = Configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            var CommandService = Configuration["CommandService"];
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
            services.AddControllers();
            services.AddScoped<IPlatformRepo, PlatformRepo>();
            services.AddHttpClient<ICommadDataClient, HttpCommandDataClient>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
           Console.WriteLine($"----->Command Service Endpoint");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllers();
                });
            PrepDb.PrepPulation(app);
        }
    }
}
