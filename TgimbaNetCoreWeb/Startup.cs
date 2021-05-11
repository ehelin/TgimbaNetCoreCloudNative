using System;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
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
            var dbConn = Configuration.GetSection("DbConnection")?.Value;
            var dbConnTest = Configuration.GetSection("DbConnectionTest")?.Value;
            var jwtIssuer = Configuration.GetSection("JwtIssuer")?.Value;

            dynamic jwtPrivateKeyObj = JsonConvert.DeserializeObject(GetSecret());
            var jwtPrivateKey = jwtPrivateKeyObj["JwtPrivateKey"]?.ToString();

            Environment.SetEnvironmentVariable("DbConnection", dbConn);
            Environment.SetEnvironmentVariable("DbConnectionTest", dbConnTest);
            Environment.SetEnvironmentVariable("JwtPrivateKey", jwtPrivateKey);
            Environment.SetEnvironmentVariable("JwtIssuer", jwtIssuer);
        }

        public string GetSecret()
        {
            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName("us-east-2"));
            GetSecretValueRequest request = new GetSecretValueRequest();
            request.SecretId = "TgimbaJwtPrivateKey";
            request.VersionStage = "AWSCURRENT"; 

            var response = client.GetSecretValueAsync(request).Result;
            var secret = response.SecretString;

            return secret;
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
