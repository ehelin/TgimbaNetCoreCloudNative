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
        public void CleanUpLocal(string user, bool deleteBucketListItems = false)
        {
            var token = EndPoint_Login();
            EndPoint_UserDelete(token, userName);
        }

        // TODO - consolidate w/api test code that is similar
        private string userName = "testUser";
        private string password = "testUser23";
        private string email = "test@aol.com";
        private string host = "https://localhost:44394/api/TgimbaApi/";
        private void EndPoint_UserDelete(string token, string userName)
        {
            var url = host + "deleteuser/" + userName;
            var result = Delete(url, Base64Encode(userName), Base64Encode(token)).Result;
        }
        private async Task<string> Delete(string url, string userName, string token)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("EncodedUserName", userName);
            client.DefaultRequestHeaders.Add("EncodedToken", token);

            var response = await client.DeleteAsync(url);

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
        private string EndPoint_Login()
        {
            var request = new LoginRequest()
            {
                EncodedUserName = Base64Encode(Shared.Constants.DEMO_USER),
                EncodedPassword = Base64Encode(Shared.Constants.DEMO_USER_PASSWORD)
            };

            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var url = host + "processuser";
            var token = Post(url, content).Result;

            return token;
        }

        private async Task<string> Post(string url, StringContent content)
        {
            var client = new HttpClient();

            var response = await client.PostAsync(url, content);

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        private string Base64Encode(string value)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(value);
            return System.Convert.ToBase64String(data);
        }

        private void DeleteTestUser(string userName, string connectionString, bool deleteBucketListItems)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(connectionString);
                cmd = conn.CreateCommand();
                cmd.CommandText = "";//deleteBucketListItems ? DELETE_TEST_USER_BUCKET_LIST_ITEMS : DELETE_TEST_USER;
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.Add(new SqlParameter("@userName", userName));

                cmd.Connection.Open();

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }

                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }
            }
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
