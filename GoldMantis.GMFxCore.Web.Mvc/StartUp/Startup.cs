using GoldMantis.GMFxCore.Core.Configuration;
using GoldMantis.GMFxCore.Web.Resources;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger.Model;

namespace GoldMantis.GMFxCore.Web.StartUp
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            Configuration = env.GetAppConfiguration();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            //??? add identity

            //Swagger - Enable this line and the related lines in Configure method to enable swagger UI
            services.AddSwaggerGen();

            //geetest service

            //Hangfire (Enable to use Hangfire instead of default job manager)
            //services.AddHangfire(config =>
            //{
            //    config.UseSqlServerStorage(Configuration.GetConnectionString("Default"));
            //});

            //Web Resource Manager
            services.AddScoped<IWebResourceManager, WebResourceManager>();

            //??? add module and dependency injection

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //??? Initialize the framework

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // don't use the browser link. so fuck
                //app.UseBrowserLink();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("~/Error?statusCode={0}");
                app.UseExceptionHandler("/Error");
            }


            AuthorizationConfigurer.Configure(app, Configuration);

            app.UseStaticFiles();

            //Hangfire dashboard & server (Enable to use Hangfire instead of default job manager)
            //app.UseHangfireDashboard("/hangfire", new DashboardOptions
            //{
               
            //});
            //app.UseHangfireServer();

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "defaultWithArea",
                    template: "{area}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            //Enable middleware to serve swagger - ui assets(HTML, JS, CSS etc.)
            app.UseSwaggerUi(); //URL: /swagger
        }
    }
}
