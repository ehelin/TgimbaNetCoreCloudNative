using System;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Shared.dto.api;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shared.misc.testUtilities
{
    public class TestUtilities
    {
        public void CleanUpLocal(string host, string userName = "testUser", bool onlyDeleteBucketListItems = false)
        {
            EndPoint_UserDelete(host, userName, onlyDeleteBucketListItems);
        }

        private void EndPoint_UserDelete(string host, string userName, bool onlyDeleteBucketListItems = false)
        {
            var url = host + "api/TgimbaApi/deleteuserbucketlistitems/" + userName + "/" + onlyDeleteBucketListItems.ToString();
            var privateKey = EnvironmentalConfig.GetJwtPrivateKey();
            var result = Delete(url, Base64Encode(userName), Base64Encode(privateKey)).Result;
        }
        private async Task<string> Delete(string url, string userName, string privateKey)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("EncodedJwtPrivateKey", privateKey);

            var response = await client.DeleteAsync(url);

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        private string Base64Encode(string value)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(value);
            return System.Convert.ToBase64String(data);
        }

       

        #region Unit Test Environment Variables

        public static void ClearEnvironmentalVariablesForUnitTests()
        {
            Environment.SetEnvironmentVariable("JwtPrivateKey", null);
            Environment.SetEnvironmentVariable("JwtIssuer", null);
        }

        public static void SetEnvironmentalVariablesForUnitTests()
        {
            Environment.SetEnvironmentVariable("JwtPrivateKey", "123134123412341341AEARSERAserae54893475384983945vsdeausceauiseycauie");
            Environment.SetEnvironmentVariable("JwtIssuer", "IAmAJstIssuer");
        }

        #endregion

        #region Integration Test Environment Variables

        public static void ClearEnvironmentalVariablesForIntegrationTests()
        {
            Environment.SetEnvironmentVariable("JwtPrivateKey", null);
            Environment.SetEnvironmentVariable("JwtIssuer", null);
            Environment.SetEnvironmentVariable("DbConnectionTest", null);
            Environment.SetEnvironmentVariable("DbConnection", null);
        }

        public static void SetEnvironmentalVariablesForIntegrationTests()
        {
            var fileContents = System.IO.File.ReadAllText("Properties\\launchSettings.json");
            dynamic jsonValues = JsonConvert.DeserializeObject(fileContents);
            
            foreach (dynamic levelOne in jsonValues.profiles)
            {
                foreach (dynamic levelTwo in levelOne)
                {
                    foreach (dynamic environmentVariable in levelTwo.environmentVariables)
                    {
                        Environment.SetEnvironmentVariable(environmentVariable.Name, environmentVariable.Value.ToString());
                    }
                }
            }
        }

        #endregion
    }
}
