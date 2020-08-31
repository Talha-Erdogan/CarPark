using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarPark.API.Business;
using CarPark.API.Business.Interfaces;
using CarPark.API.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CarPark.API
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
            // business service ve interface DI container tanimlari
            services.AddTransient<IAuthService, AuthService>();
           

            services.AddSingleton<IConfiguration>(Configuration); //add Configuration to our services collection
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //config helper'ý configure etmek için
            Common.ConfigHelper.Configure(Configuration);

            // token helper
            TokenHelper.Configure(app.ApplicationServices);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
