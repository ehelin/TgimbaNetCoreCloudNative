using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Shared.dto;
using Shared.interfaces;

namespace DALBlobStorage
{
    public class BucketListData : IBucketListData
    {
        private BlobContainerClient blobStorageContainerClient = null;

        public BucketListData(BlobContainerClient blobStorageContainerClient)
        {
            this.blobStorageContainerClient = blobStorageContainerClient;
        }

        async void IBucketListData.UpsertBucketListItem(BucketListItem bucketListItem, string userName)
        {
            string fileName = "bucketListItem" + userName + ".txt";

            // Write text to the file
            //await File.WriteAllTextAsync(localFilePath, "Hello, World!");

            // Get a reference to a blob
            BlobClient blobClient = this.blobStorageContainerClient.GetBlobClient(fileName);

            Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

            await blobClient.UploadAsync(fileName, true);
        }

        // NEW for blob storage data client
        async Task<IList<BucketListItem>> IBucketListData.GetBucketListBlobStorage(string blobStorageItemId)
        {
            var blobClient = this.blobStorageContainerClient.GetBlobClient(blobStorageItemId);
            var result = await blobClient.DownloadToAsync(blobStorageItemId);

            // TODO - convert result to the saved bucket list item
            //return result;
            return null;
        }

        // NEW for blob storage data client
        async void DeleteBucketListItem(string blobStorageItemId)
        {
            var blobClient = this.blobStorageContainerClient.GetBlobClient(blobStorageItemId);

            this.blobStorageContainerClient.GetBlobClient(blobStorageItemId).DeleteIfExists();
        }

        #region Not Implemented

        void IBucketListData.DeleteBucketListItem(long bucketListItemDbId)
        {
            throw new NotImplementedException();
        }

        void IBucketListData.AddToken(long userId, string token)
        {
            throw new NotImplementedException();
        }

        long IBucketListData.AddUser(User user)
        {
            throw new NotImplementedException();
        }

        void IBucketListData.DeleteUser(long userId)
        {
            throw new NotImplementedException();
        }

        void IBucketListData.DeleteUserBucketListItems(string userName, bool onlyDeleteBucketListItems)
        {
            throw new NotImplementedException();
        }

        IList<SystemBuildStatistic> IBucketListData.GetSystemBuildStatistics()
        {
            throw new NotImplementedException();
        }

        IList<SystemStatistic> IBucketListData.GetSystemStatistics()
        {
            throw new NotImplementedException();
        }

        User IBucketListData.GetUser(long id)
        {
            throw new NotImplementedException();
        }

        User IBucketListData.GetUser(string userName)
        {
            throw new NotImplementedException();
        }

        List<User> IBucketListData.GetUsers(string userName)
        {
            throw new NotImplementedException();
        }

        void IBucketListData.LogMsg(string msg)
        {
            throw new NotImplementedException();
        }

        public IList<BucketListItem> GetBucketList(string userName)
        {
            throw new NotImplementedException();
        }

        void IBucketListData.DeleteBucketListItem(string blobStorageItemId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
