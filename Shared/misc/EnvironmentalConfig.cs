using System;

namespace Shared.misc
{
    public class EnvironmentalConfig
    {
        public static string GetBlobStorageConnectionString()
        {
            var blobConnectionString = Environment.GetEnvironmentVariable("BlobStorageConnectionString");
            
            return blobConnectionString;
        }

        public static string GetApiHost()
        {
            System.Console.WriteLine("EnvironmentalConfig-GetApiHost()");

            string apiHost = Environment.GetEnvironmentVariable("ApiHost");
            System.Console.WriteLine("EnvironmentalConfig-GetApiHost()-ApiHost: {0}", apiHost);

            return apiHost;
        }

        public static string GetDbSetting(bool useTestDb = false)
        {
            System.Console.WriteLine("EnvironmentalConfig-GetDbSetting(args)");

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
            System.Console.WriteLine("EnvironmentalConfig-GetJwtPrivateKey()");

            var key = Environment.GetEnvironmentVariable("JwtPrivateKey");

            return key;
        }

        public static string GetJwtIssuer()
        {
            System.Console.WriteLine("EnvironmentalConfig-GetJwtIssuer()");

            var key = Environment.GetEnvironmentVariable("JwtIssuer");

            return key;
        }
    }
}
