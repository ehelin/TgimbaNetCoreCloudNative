﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Algorithms.Algorithms.Search;
using Algorithms.Algorithms.Search.Implementations;
using Algorithms.Algorithms.Sorting;
using Algorithms.Algorithms.Sorting.Implementations;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using APINetCore;
using BLLNetCore.helpers;
using BLLNetCore.Security;  // TODO - remove after namespaces changed to bllnetcore.helpers
using DALNetCore;
using DALNetCore.helpers;
using DALNetCore.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Shared.interfaces;
using Shared.misc;
using TgimbaNetCoreWebShared.Models;

namespace TgimbaNetCoreWebShared
{
    public class Utilities
	{
        public static string GetConfigValue(string key, IConfiguration config)
        {
            var section = config.GetSection(key);

            if (section == null)
            {
                throw new Exception("Config item " + key + " is null");
            }

            return section.Value;
        }

        public static void SetProductionEnvironmentalVariables(IConfiguration Configuration)
        {
            System.Console.WriteLine("SetProductionEnvironmentalVariables(args)");

            Environment.SetEnvironmentVariable("DbConnectionTest", null);
            Environment.SetEnvironmentVariable("DbConnection", null);
            Environment.SetEnvironmentVariable("JwtPrivateKey", null);
            Environment.SetEnvironmentVariable("JwtIssuer", null);
            Environment.SetEnvironmentVariable("ApiHost", null);

            var dbConn = Configuration.GetSection("DbConnection")?.Value;
            var jwtIssuer = Configuration.GetSection("JwtIssuer")?.Value;
            var apiHost = Configuration.GetSection("ApiHost")?.Value;

            dynamic jwtPrivateKeyObj = JsonConvert.DeserializeObject(GetSecret());
            var jwtPrivateKey = jwtPrivateKeyObj["JwtPrivateKey"]?.ToString();

            System.Console.WriteLine("DbConnection: {0}", dbConn);
            System.Console.WriteLine("JwtPrivateKey: {0}", jwtPrivateKey);
            System.Console.WriteLine("JwtIssuer: {0}", jwtIssuer);
            System.Console.WriteLine("ApiHost: {0}", apiHost);

            Environment.SetEnvironmentVariable("DbConnection", dbConn);
            Environment.SetEnvironmentVariable("JwtPrivateKey", jwtPrivateKey);
            Environment.SetEnvironmentVariable("JwtIssuer", jwtIssuer);
            Environment.SetEnvironmentVariable("ApiHost", apiHost);
        }

        private static string GetSecret()
        {
            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName("us-east-2"));
            GetSecretValueRequest request = new GetSecretValueRequest();
            request.SecretId = "TgimbaJwtPrivateKey";
            request.VersionStage = "AWSCURRENT";

            var response = client.GetSecretValueAsync(request).Result;
            var secret = response.SecretString;

            return secret;
        }

        public static void SetUpDI(IServiceCollection services, IConfiguration Configuration)
        {
            System.Console.WriteLine("Utilities.cs-SetUpDI(args)");

            var dbConn = EnvironmentalConfig.GetDbSetting();
            var jwtPrivateKey = EnvironmentalConfig.GetJwtPrivateKey();
            var apiHost = EnvironmentalConfig.GetApiHost();

            System.Console.WriteLine("Utilities.cs-SetUpDI(args) - dbConn: {0}", dbConn);
            System.Console.WriteLine("Utilities.cs-SetUpDI(args) - jwtPrivateKey: {0}", jwtPrivateKey);
            System.Console.WriteLine("Utilities.cs-SetUpDI(args) - apiHost: {0}", apiHost);

            services.AddSingleton<IWebClient>(new WebClient(EnvironmentalConfig.GetApiHost(), new TgimbaHttpClient()));
            services.AddSingleton<IUserHelper>(new UserHelper());

            // true load test db connection, false load prod db connection
            services.AddSingleton(new DALNetCore.Models.BucketListContext
            (
                //TODO - temporary test
                false
                //Configuration["Env"] != null
                //    && Configuration["Env"] == "Development"
                //        ? true
                //            : false
            ));
            services.AddSingleton<IBucketListData>(x =>
                new BucketListData(x.GetRequiredService<DALNetCore.Models.BucketListContext>(),
                                   x.GetRequiredService<IUserHelper>()
                                   ));
            services.AddSingleton<IPassword>(new PasswordHelper());
            services.AddSingleton<IGenerator>(new GeneratorHelper());
            services.AddSingleton<IString>(new StringHelper());

            services.AddSingleton(new AvailableSearchingAlgorithms(
                new List<ISearch>()
                {
                        new LinqSearch(),
                        new BinarySearch()
                 }
             ));

            services.AddSingleton(new AvailableSortingAlgorithms(
                new List<ISort>()
                {
                    new LinqSort(),
                    new BubbleSort(),
                    new InsertionSort()
                 }
             ));
            services.AddSingleton<ITgimbaService>(x =>
                new TgimbaService(x.GetRequiredService<IBucketListData>(),
                                   x.GetRequiredService<IPassword>(),
                                   x.GetRequiredService<IGenerator>(),
                                   x.GetRequiredService<IString>(),
                                   x.GetRequiredService<AvailableSortingAlgorithms>(),
                                   x.GetRequiredService<AvailableSearchingAlgorithms>()
                                   ));
            services.AddSingleton<IValidationHelper>(new ValidationHelper());
            services.AddSingleton<IConfiguration>(Configuration);
        }

        public static string GetHeaderValue(string headerKeyName, HttpRequest Request)
        {
            return Request?.Headers[headerKeyName];
        }

        public static bool IsMobile(string userAgent) 
		{
            // NOTE: Regex from http://detectmobilebrowsers.com/ 
            Regex b = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Regex v = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            if ((b.IsMatch(userAgent) || v.IsMatch(userAgent.Substring(0, 4))))
            {
                return true;
            }

            return false;
        }
		   
        public static string[] GetAvailableSortingAlgorithms()
        {
            var availableSortingAlgorithms = Enum.GetNames(typeof(Enums.SortAlgorithms));

            return availableSortingAlgorithms;
        }

        public static string[] GetAvailableSearchingAlgorithms()
        {
            var availableSearchingAlgorithms = Enum.GetNames(typeof(Enums.SearchAlgorithms));

            return availableSearchingAlgorithms;
        }

        public static string ConvertModelToString(SharedBucketListModel model) 
		{	   			
			string bucketListItem = null;

			if (model != null)
			{
				bucketListItem = "," + model.Name + ",";	  // leading comma is for tgimba service
				bucketListItem += model.DateCreated + ",";
				bucketListItem += model.BucketListItemType.ToString() + ",";
				bucketListItem += model.Completed == true ? "1,":"0,";
				bucketListItem += model.Latitude + ",";
				bucketListItem += model.Longitude + ",";
				bucketListItem += model.DatabaseId + ",";
				bucketListItem += model.UserName;	
			}

			return bucketListItem;					
		}

		public static Enums.BucketListItemTypes ConvertCategoryToEnum(string category) 
		{
			if(category == "Hot") 
			{
				return Enums.BucketListItemTypes.Hot;
			}
			else if(category == "Warm") 
			{
				return Enums.BucketListItemTypes.Warm;
			} 
			else if(category == "Cool") 
			{
				return Enums.BucketListItemTypes.Cool;
			}  
			else if(category == "Cold") 
			{
				return Enums.BucketListItemTypes.Cold;
			}
			else 
			{
				throw new Exception("Unknown category: " + category);
			}
		}
	}
}

