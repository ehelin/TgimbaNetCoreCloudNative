using System.Collections.Generic;
using Shared.dto;
using System.Threading.Tasks;

namespace Shared.interfaces
{
    public interface IBucketListData
    {
        IList<Shared.dto.BucketListItem> GetBucketList(string userName);
        void UpsertBucketListItem(Shared.dto.BucketListItem bucketListItem, string userName);
        void DeleteBucketListItem(long bucketListItemDbId);
        void LogMsg(string msg);
        IList<SystemStatistic> GetSystemStatistics();
        IList<SystemBuildStatistic> GetSystemBuildStatistics();

        void AddToken(long userId, string token);
        User GetUser(long id);
        User GetUser(string userName);
        long AddUser(User user);
        void DeleteUser(long userId);
        void DeleteUserBucketListItems(string userName, bool onlyDeleteBucketListItems);
        List<User> GetUsers(string userName);

        // NEW for blob storage data client (refactor interface is blob storage is used)
        void DeleteBucketListItem(string blobStorageItemId);
        Task<IList<BucketListItem>> GetBucketListBlobStorage(string blobStorageItemId);
    }
}
