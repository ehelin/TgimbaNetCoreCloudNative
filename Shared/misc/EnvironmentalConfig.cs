using System;

namespace Shared.misc
{
    public class EnvironmentalConfig
    {
        public static string GetDbSetting(bool useTestDb = false)
        {
            System.Console.WriteLine("Console-GetDbSetting(args)");
            System.Diagnostics.Debug.WriteLine("Debug-GetDbSetting(args)");
            System.Diagnostics.Trace.WriteLine("Trace-GetDbSetting(args)");

            string dbConn = string.Empty;

            if (useTestDb)
            {
                dbConn = Environment.GetEnvironmentVariable("DbConnectionTest");
            }
            else
            {
                dbConn = Environment.GetEnvironmentVariable("DbConnection");
            }

            return dbConn;
        }

        public static string GetJwtPrivateKey()
        {
            System.Console.WriteLine("Console-GetJwtPrivateKey()");
            System.Diagnostics.Debug.WriteLine("Debug-GetJwtPrivateKey()");
            System.Diagnostics.Trace.WriteLine("Trace-GetJwtPrivateKey()");

            var key = Environment.GetEnvironmentVariable("JwtPrivateKey");

            return key;
        }

        public static string GetJwtIssuer()
        {
            System.Console.WriteLine("Console-GetJwtIssuer()");
            System.Diagnostics.Debug.WriteLine("Debug-GetJwtIssuer()");
            System.Diagnostics.Trace.WriteLine("Trace-GetJwtIssuer()");

            var key = Environment.GetEnvironmentVariable("JwtIssuer");

            return key;
        }
    }
}
