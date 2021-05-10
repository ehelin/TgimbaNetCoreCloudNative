using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TgimbaNetCoreWebShared;

namespace TgimbaNetCoreWeb
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
            Utilities.SetUpDI(services, Configuration);
            services.AddMvc();

            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        private void SetProductionEnvironmentalVariables(IConfiguration Configuration)
        {
            var dbConn = Configuration.GetSection("DbConnection").Value;
            var dbConnTest = Configuration.GetSection("DbConnectionTest").Value;
            string jwtPrivateKey = null;// TODO - get from secret manager
            var jwtIssuer = Configuration.GetSection("JwtIssuer").Value;

            Environment.SetEnvironmentVariable("DbConnection", dbConn);
            Environment.SetEnvironmentVariable("DbConnectionTest", dbConnTest);
            Environment.SetEnvironmentVariable("JwtPrivateKey", null);
            Environment.SetEnvironmentVariable("JwtIssuer", jwtIssuer);
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
                SetProductionEnvironmentalVariables(Configuration);
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();  // TODO - add local flag to handle
            
            app.UseSession();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Welcome}/{action=Index}/{id?}");
            });
        }
    }
}
