using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Newtonsoft.Json;
using Shared.dto;
using Shared.interfaces;

namespace DALBlobStorage
{
    public class BucketListData : IBlobStorage
    {
        private BlobContainerClient blobStorageContainerClient = null;

        public BucketListData(BlobContainerClient blobStorageContainerClient)
        {
            this.blobStorageContainerClient = blobStorageContainerClient;
        }

        async Task IBlobStorage.UpsertBucketListItem(BucketListItem bucketListItem, string userName)
        {
            string localPath = "./";
            string fileName = "bucketListItem" + userName + ".txt";
            string localFilePath = Path.Combine(localPath, fileName);

            var json = JsonConvert.SerializeObject(bucketListItem);

            await File.WriteAllTextAsync(localFilePath, json);

            BlobClient blobClient = this.blobStorageContainerClient.GetBlobClient(fileName);

            Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

            await blobClient.UploadAsync(localFilePath, true);

            File.Delete(localFilePath);
        }

        async Task<BucketListItem> IBlobStorage.GetBucketList(string blobStorageItemId)
        {
            BucketListItem bucketListItem = null;
            var blobClient = this.blobStorageContainerClient.GetBlobClient(blobStorageItemId);
            if (await blobClient.ExistsAsync())
            {
                var result = await blobClient.DownloadAsync();
                string resultJson = "";

                using (StreamReader streamReader = new StreamReader(result.Value.Content))
                {
                    resultJson = streamReader.ReadToEnd();
                }

                bucketListItem = JsonConvert.DeserializeObject<BucketListItem>(resultJson);
            }

            return bucketListItem;
        }

        void IBlobStorage.DeleteBucketListItem(string blobStorageItemId)
        {
            var blobClient = this.blobStorageContainerClient.GetBlobClient(blobStorageItemId);

            this.blobStorageContainerClient.GetBlobClient(blobStorageItemId).DeleteIfExists();
        }
    }
}
