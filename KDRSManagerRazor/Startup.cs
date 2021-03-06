using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Hanssens.Net;
using KDRSManagerRazor.Data;
using KDRSManagerRazor.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebEssentials.AspNetCore.Pwa;

namespace KDRSManagerRazor
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddProgressiveWebApp(new PwaOptions
            {
                RegisterServiceWorker = true,
                RoutesToPreCache = "/",
                Strategy = ServiceWorkerStrategy.NetworkFirst
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //// initialize, with default settings
            //var storage = new LocalStorage();

            ////// ... or initialize with a custom configuration
            ////var config = new LocalStorageConfiguration()
            ////{
            ////    // see the section "Configuration" further on
            ////};

            ////var storage = new LocalStorage(config);

            //// store any object, or collection providing only a 'key'
            //var key = "whatever";
            //var value = "...";

            //storage.Store(key, value);

            //Server srv1 = new Server("91.192.221.234", 999, "Fuglekasser");
            //Server srv2 = new Server("91.192.221.162", 999, "Fuglekasser");
            //StoredData.SetServer(srv1);
            //StoredData.SetServer(srv2);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}