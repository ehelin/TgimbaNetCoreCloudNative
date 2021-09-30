using System;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using DALBlobStorage;
using Shared.dto;
using Shared.interfaces;
using Shared.misc;

namespace Automation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("TgimbaService.cs - ProcessUser(args) - Process is starting!");

            Console.WriteLine("TgimbaService.cs - ProcessUser(args) - Process is running!");
            RunJob();
            
            Console.WriteLine("TgimbaService.cs - ProcessUser(args) - Process is stopped!");
        }

        private static async Task<BlobContainerClient> CreateAzureBlobStorageContainerClient()
        {
            var blobConnectionString = EnvironmentalConfig.GetBlobStorageConnectionString();
            var blobServiceClient = new BlobServiceClient(blobConnectionString);
            string containerName = "tgimba" + Guid.NewGuid().ToString();
            BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

            return containerClient;
        }

        public async static void RunJob()
        {
            var blobStorageContainerClient = CreateAzureBlobStorageContainerClient().Result;
            IBlobStorage blobClient = new BucketListData(blobStorageContainerClient);
            string userName = "bucketUserSeeTheWorld";

            await SaveBucketListItem(blobClient, userName);

            var bucketListItem = VerifyRecordExists(blobClient, userName).Result;

            Cleanup(blobClient, userName, bucketListItem);

            blobStorageContainerClient.Delete();
        }

        private async static Task SaveBucketListItem(IBlobStorage blobClient, string userName)
        {
            Console.WriteLine("TgimbaService.cs - SaveBucketListItem(args) - Creating/Saving bucket list item to blob storage.");

            var newBucketList = new BucketListItem()
            {
                Name = "IAmABucketListItem",
                Created = DateTime.UtcNow,
                Category = Enums.BucketListItemTypes.Hot.ToString(),
                Achieved = false,
                Id = 728, //Normally set by some incrementer
                Latitude = (decimal)1.2,
                Longitude = (decimal)2.1,
            };

            await blobClient.UpsertBucketListItem(newBucketList, userName);
        }

        private async static Task<BucketListItem> VerifyRecordExists(IBlobStorage blobClient, string userName)
        {
            Console.WriteLine("TgimbaService.cs - VerifyRecordExists(args) - Retrieving saved bucket list item from blob storage.");

            // NOTE: With this current design, each user could only have one saved bucket list item.  If this were for real, an Id would need be to included in the id
            var blobStorageItemId = "bucketListItem" + userName + ".txt";

            var bucketListItem = await blobClient.GetBucketList(blobStorageItemId);
            if (bucketListItem != null)
            {
                Console.WriteLine("TgimbaService.cs - VerifyRecordExists(args) - Bucket List item exists - Name {0}", bucketListItem.Name);
            }
            else
            {
                Console.WriteLine("TgimbaService.cs - VerifyRecordExists(args) - Bucket List item save fails...no record retrieved");
            }

            return bucketListItem;
        }

        private static void Cleanup(IBlobStorage blobClient, string userName, BucketListItem bucketListItem)
        {
            Console.WriteLine("TgimbaService.cs - Cleanup(args) - Deleting saved bucket list item from blob storage.");
            var blobStorageItemId = "bucketListItem" + userName + ".txt";
            blobClient.DeleteBucketListItem(blobStorageItemId);

            Console.WriteLine("TgimbaService.cs - Cleanup(args) - Verifying bucket list item was deleted from blob storage.");
            var bucketListItemsAfterDeletion = blobClient.GetBucketList(userName).Result;

            if (bucketListItemsAfterDeletion != null)
            {
                Console.WriteLine("TgimbaService.cs - Cleanup(args) - Deleting bucket list item from blob storage failed.");
            }
            else
            {
                Console.WriteLine("TgimbaService.cs - Cleanup(args) - Deleting bucket list item from blob storage succeeded.");
            }
        }
    }
}
