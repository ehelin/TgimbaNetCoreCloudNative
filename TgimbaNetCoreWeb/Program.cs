using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace TgimbaNetCoreWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            System.Console.WriteLine("Console-Main(args)");
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                // NOTE: This is not necessary if using environmental variables...leaving in for clarity
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    System.Console.WriteLine("Console-BuildWebHost(args)-env: {0}", env);

                    //load config from aws
                    if (env.EnvironmentName != "Development")
                    {
                        System.Console.WriteLine("Console-BuildWebHost(args)-loading config.AddSystemsManager(args)");
                        config.AddSystemsManager($"/", System.TimeSpan.FromMinutes(5));
                    } 
                    //load config from local appsettings.json
                    else
                    {
                        System.Console.WriteLine("Console-BuildWebHost(args)-loading else appsettings");
                        config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    }
                    config.AddEnvironmentVariables();
                })
                .UseStartup<Startup>()
                .Build();
    }
}
