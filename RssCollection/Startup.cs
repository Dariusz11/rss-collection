using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RssCollection.Models;
using RssCollection.Services;

namespace RssCollection
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

            services.AddSingleton<IHttp, Http>();

            //services.Configure<RssDatabaseSettings>(Configuration.GetSection(nameof(RssDatabaseSettings)));

            //services.AddSingleton<IRssDatabaseSettings>(sp =>sp.GetRequiredService<IOptions<IRssDatabaseSettings>>().Value);
            IRssDatabaseSettings rssDatabaseSettings = new RssDatabaseSettings();
            rssDatabaseSettings.CollectionName = Configuration["RssDatabaseSettings:CollectionName"];
            rssDatabaseSettings.ConnectionString = Configuration["RssDatabaseSettings:ConnectionString"];
            rssDatabaseSettings.DatabaseName = Configuration["RssDatabaseSettings:DatabaseName"];

            services.AddSingleton<IMailing, Mailing>();

            services.AddSingleton<IDataBase,DataBase>(sp=>new DataBase(rssDatabaseSettings));
            services.AddControllersWithViews();
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
                app.UseExceptionHandler("/home/error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
