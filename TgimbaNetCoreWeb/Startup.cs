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
            System.Console.WriteLine("Console-ConfigureServices(args)");
            System.Diagnostics.Debug.WriteLine("Debug-ConfigureServices(args)");
            System.Diagnostics.Trace.WriteLine("Trace-ConfigureServices(args)");
            Utilities.SetProductionEnvironmentalVariables(Configuration);
            Utilities.SetUpDI(services, Configuration);
            services.AddMvc();

            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            System.Console.WriteLine("Console-Configure(args)");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();      
            }
            else
            {
                //Utilities.SetProductionEnvironmentalVariables(Configuration);
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
