using System;

namespace Shared.misc
{
    public class EnvironmentalConfig
    {
        public static string GetDbSetting(bool useTestDb = false)
        {
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
            var key = Environment.GetEnvironmentVariable("JwtPrivateKey");

            return key;
        }

        public static string GetJwtIssuer()
        {
            var key = Environment.GetEnvironmentVariable("JwtIssuer");

            return key;
        }
    }
}
