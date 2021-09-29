using System;
using System.Collections.Generic;
using Shared.dto;
using Shared.interfaces;

namespace DALBlobStorage
{
    public class BucketListData : IBucketListData
    {
        private string blobConnectionString = string.Empty;

        public BucketListData(string blobConnectionString)
        {
            this.blobConnectionString = blobConnectionString;
        }

        void IBucketListData.UpsertBucketListItem(BucketListItem bucketListItem, string userName)
        {
            throw new NotImplementedException();
        }

        IList<BucketListItem> IBucketListData.GetBucketList(string userName)
        {
            throw new NotImplementedException();
        }

        void IBucketListData.DeleteBucketListItem(long bucketListItemDbId)
        {
            throw new NotImplementedException();
        }

        #region Not Implemented

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

        #endregion
    }
}
