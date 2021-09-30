using System.Threading.Tasks;
using Shared.dto;

namespace Shared.interfaces
{
    public interface IBlobStorage
    {
        Task<int> UpsertBucketListItem(Shared.dto.BucketListItem bucketListItem, string userName);
        void DeleteBucketListItem(string blobStorageItemId);
        Task<BucketListItem> GetBucketList(string blobStorageItemId);
    }
}
