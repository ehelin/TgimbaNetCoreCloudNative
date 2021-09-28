using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.AzureAppServices;

namespace Automation
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Console - TgimbaService.cs - ProcessUser(args) - Process is running!");
            System.Diagnostics.Debug.WriteLine("Debug-TgimbaService.cs - ProcessUser(args) - Process is running!");
            System.Diagnostics.Trace.WriteLine("Trace-TgimbaService.cs - ProcessUser(args) - Process is running!");

            Console.Read();
        }
    }
}
