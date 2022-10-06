using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartSchool.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(cfg =>
            {
                //TODO: Configuração para funcionar o profile do EF Core
                cfg.AddConsole();
            });

            ILoggerFactory factory = LoggerFactory.Create(builder=> builder.AddConsole());

            services.AddDbContext<SmartContext>(
                context => {
                    context.UseSqlite(Configuration.GetConnectionString("Default"));

                    //TODO: Configuração para funcionar o profile do EF Core
                    context.EnableSensitiveDataLogging(true).UseLoggerFactory(factory);
                }
            );

            services.AddScoped<IRepository, Repository>();

            services.AddRazorPages().AddNewtonsoftJson(
                opt => opt.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers(); //Routes for my API controllers
            });
        }
    }
}
