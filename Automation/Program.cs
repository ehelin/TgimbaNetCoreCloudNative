using System;
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

        private static void RunJob()
        {
            var blobConnectionString = EnvironmentalConfig.GetBlobStorageConnectionString();
            IBucketListData blobClient = new BucketListData(blobConnectionString);
            string userName = "";

            SaveBucketListItem(blobClient, userName);

            var bucketListItem = VerifyRecordExists(blobClient, userName);

            Cleanup(blobClient, userName, bucketListItem);
        }

        private static void SaveBucketListItem(IBucketListData blobClient, string userName)
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

            blobClient.UpsertBucketListItem(newBucketList, userName);
        }

        private static BucketListItem VerifyRecordExists(IBucketListData blobClient, string userName)
        {
            Console.WriteLine("TgimbaService.cs - VerifyRecordExists(args) - Retrieving saved bucket list item from blob storage.");
            var bucketListItems = blobClient.GetBucketList(userName);
            var bucketListItem = bucketListItems[0];

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

        private static void Cleanup(IBucketListData blobClient, string userName, BucketListItem bucketListItem)
        {
            Console.WriteLine("TgimbaService.cs - Cleanup(args) - Deleting saved bucket list item from blob storage.");
            blobClient.DeleteBucketListItem(bucketListItem.Id.Value);

            Console.WriteLine("TgimbaService.cs - Cleanup(args) - Verifying bucket list item was deleted from blob storage.");
            var bucketListItemsAfterDeletion = blobClient.GetBucketList(userName);

            if (bucketListItemsAfterDeletion != null || bucketListItemsAfterDeletion.Count > 0)
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
