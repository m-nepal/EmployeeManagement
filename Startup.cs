using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));
            //services.AddMvc();
            services.AddMvc().AddXmlSerializerFormatters();
            //services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                DeveloperExceptionPageOptions doptions = new DeveloperExceptionPageOptions
                {
                    SourceCodeLineCount = 2
                };
                app.UseDeveloperExceptionPage(doptions);
            }
            else
            {
                app.UseExceptionHandler("/Error");
                //app.UseStatusCodePages("/Error/{0}");
                //app.UseStatusCodePagesWithRedirects("/Error/{0}");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            //else if (env.IsStaging() || env.IsProduction() || env.IsEnvironment("UAT"))
            //{
            //    app.UseExceptionHandler("/Error");
            //}

            app.UseStaticFiles();
            //app.UseMvc();
            // app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
           {

              // routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
               routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
           });




        }
    }
}
