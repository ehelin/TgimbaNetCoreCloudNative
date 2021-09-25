using System;
using System.Collections.Generic;
using System.Linq;
using DALNetCore.interfaces;
using Shared.dto;
using Shared.exceptions;
using Shared.interfaces;
using models = DALNetCore.Models;

namespace DALNetCore
{
    public class BucketListData : IBucketListData
    {
        private models.BucketListContext context = null;
        private IUserHelper userHelper = null;

        public BucketListData(models.BucketListContext context, IUserHelper userHelper)
        {
            this.userHelper = userHelper;
            this.context = context;
        }

        #region User 

        public void AddToken(long userId, string token)
        {
            var dbUser = this.context.User
                                   .Where(x => x.UserId == userId)
                                   .FirstOrDefault();
            if (dbUser == null)
            {
                throw new RecordDoesNotExistException("AddToken - User to have token added does not exist. userId - " + userId.ToString());
            }

            dbUser.Token = token;

            this.context.Update(dbUser);
            this.context.SaveChanges();
        }

        public User GetUser(long id)
        {
            var dbUser = this.context.User
                                   .Where(x => x.UserId == id)
                                   .FirstOrDefault();
            if (dbUser == null)
            {
                throw new RecordDoesNotExistException("GetUser - User does not exist. userId - " + id.ToString());
            }

            var user = this.userHelper.ConvertDbUserToUser(dbUser);

            return user;
        }

        // TODO - add test
        public List<User> GetUsers(string userName)
        {
            List<User> users = new List<User>();

            var dbUsers = this.context.User.Where(x => x.UserName == userName).ToList();
            foreach(var dbUser in dbUsers)
            {
                users.Add(this.userHelper.ConvertDbUserToUser(dbUser));
            }

            return users;
        }

        public User GetUser(string userName)
        {
            try 
            {
                var dbUser = this.context.User
                                .Where(x => x.UserName == userName)
                                .FirstOrDefault();

                // TODO - update tests
                if (dbUser == null) { return null; }

                var user = this.userHelper.ConvertDbUserToUser(dbUser);
          
                return user;
            } 
            catch(Exception ex)
            {
                var test = 1;
            }

            return null;
        }

        public long AddUser(User user)
        {
            var dbUser = new models.User
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                Salt = user.Salt,
                Created = DateTime.Now.ToUniversalTime(),
                CreatedBy = "Website",
                Modified = DateTime.Now.ToUniversalTime(),
                ModifiedBy = "Website"
            };
            this.context.User.Add(dbUser);
            this.context.SaveChanges();

            return dbUser.UserId;
        }

        public void DeleteUserBucketListItems(string userName, bool onlyDeleteBucketListItems)
        {
            var bucketListItems = GetBucketList(userName);

            foreach (var bucketListItem in bucketListItems)
            {
                DeleteBucketListItem(bucketListItem.Id.Value);
            }

            if (!onlyDeleteBucketListItems)
            {
                var users = this.context.User.Where(x => x.UserName == userName).ToList();
                foreach (var user in users)
                {
                    this.context.Remove(user);
                    this.context.SaveChanges();
                }
            }
        }

        public void DeleteUser(long userId)
        {
            var dbUser = this.context.User
                                   .Where(x => x.UserId == userId)
                                   .FirstOrDefault(); 
            if (dbUser == null)
            {
                throw new RecordDoesNotExistException("DeleteUser - User to delete does not exist. userId - " + userId.ToString());
            }

            this.context.Remove(dbUser);
            this.context.SaveChanges();
        }

        #endregion

        #region Misc

        public void LogMsg(string msg)
        {
            var logModel = new models.Log
            {
                LogMessage = msg,
                Created = DateTime.UtcNow
            };
            this.context.Logs.Add(logModel);
            this.context.SaveChanges();
        }

        public IList<SystemBuildStatistic> GetSystemBuildStatistics()
        {
            var buildStatistics = this.context.BuildStatistics
                                    .OrderByDescending(s => s.StartTime)
                                    .Take(2)
                                    .ToList();
            var systemBuildStatics = new List<SystemBuildStatistic>();

            if (buildStatistics != null)
            {
                foreach (var buildStatistic in buildStatistics)
                {
                    var systemBuildStatistic = new SystemBuildStatistic
                    {
                        Start = buildStatistic.StartTime.ToString(),
                        End = buildStatistic.EndTime.ToString(),
                        BuildNumber = buildStatistic.BuildNumber,
                        Status = buildStatistic.Status
                    };

                    systemBuildStatics.Add(systemBuildStatistic);
                }
            }

            return systemBuildStatics;
        }

        public IList<SystemStatistic> GetSystemStatistics()
        {
            System.Console.WriteLine("BucketListData-GetSystemStatistics()");

            var systemStatistics = this.context.SystemStatistics
                                    .OrderByDescending(s => s.Created)
                                    .Take(2)
                                    .ToList();
            var systemSystemStatics = new List<SystemStatistic>();

            if (systemStatistics != null)
            {
                foreach (var systemStatistic in systemStatistics)
                {
                    var systemSystemStatistic = new SystemStatistic
                    {
                        WebSiteIsUp = systemStatistic.WebsiteIsUp.HasValue ? systemStatistic.WebsiteIsUp.Value : false,
                        DatabaseIsUp = systemStatistic.DatabaseIsUp.HasValue ? systemStatistic.DatabaseIsUp.Value : false,
                        AzureFunctionIsUp = systemStatistic.AzureFunctionIsUp.HasValue ? systemStatistic.AzureFunctionIsUp.Value : false,
                        Created = systemStatistic.Created.ToString()
                    };

                    systemSystemStatics.Add(systemSystemStatistic);
                }
            }

            return systemSystemStatics;
        }

        #endregion

        #region BucketList

        public void UpsertBucketListItem(Shared.dto.BucketListItem bucketListItem, string userName)
        {
            var existingBucketListItem = this.context.BucketListItems
                                                            .Where(x => x.BucketListItemId == bucketListItem.Id)
                                                            .FirstOrDefault();

            if (existingBucketListItem != null)
            {
                UpdateBucketListItem(existingBucketListItem, bucketListItem);
            }
            else
            {
                InsertBucketListItem(bucketListItem, userName);
            }
        }

        public IList<Shared.dto.BucketListItem> GetBucketList(string userName)
        {
            var dbBucketListItems = from bli in this.context.BucketListItems
                                    join blu in this.context.BucketListUsers on bli.BucketListItemId equals blu.BucketListItemId
                                    join u in this.context.User on blu.UserId equals u.UserId
                                    where u.UserName == userName
                                    select bli;

            var bucketListItems = new List<Shared.dto.BucketListItem>();
            foreach (var dbBucketListItem in dbBucketListItems)
            {
                var bucketListItem = new Shared.dto.BucketListItem
                {
                    Name = dbBucketListItem.ListItemName.Trim(),
                    Created = dbBucketListItem.Created.Value.ToLocalTime(),
                    Category = dbBucketListItem.Category,
                    Achieved = dbBucketListItem.Achieved.HasValue
                                    ? dbBucketListItem.Achieved.Value : false,
                    Id = dbBucketListItem.BucketListItemId,
                    Latitude = dbBucketListItem.Latitude.HasValue ? (decimal)dbBucketListItem.Latitude : (decimal)0,
                    Longitude = dbBucketListItem.Longitude.HasValue ? (decimal)dbBucketListItem.Longitude : (decimal)0
                };

                bucketListItems.Add(bucketListItem);
            }

            return bucketListItems;
        }

        public void DeleteBucketListItem(long bucketListItemDbId)
        {
            var bucketListItemToDelete = this.context.BucketListItems
                                                        .Where(x => x.BucketListItemId == bucketListItemDbId)
                                                        .FirstOrDefault();
            var bucketListItemUserToDelete = this.context.BucketListUsers
                                                        .Where(x => x.BucketListItemId == bucketListItemDbId)
                                                        .FirstOrDefault();

            if (bucketListItemToDelete == null)
            {
                throw new RecordDoesNotExistException("Bucket list item to be deleted does not exist - id: " + bucketListItemDbId.ToString());
            }

            this.context.BucketListUsers.Remove(bucketListItemUserToDelete);
            this.context.BucketListItems.Remove(bucketListItemToDelete);
            this.context.SaveChanges();
        }

        #endregion

        #region Private Methods

        private void UpdateBucketListItem
        (
            models.BucketListItem existingBucketListItem,
            Shared.dto.BucketListItem bucketListItem
        )
        {
            existingBucketListItem.ListItemName = bucketListItem.Name;
            existingBucketListItem.Created = bucketListItem.Created.ToUniversalTime();
            existingBucketListItem.Category = bucketListItem.Category;
            existingBucketListItem.Achieved = bucketListItem.Achieved;
            existingBucketListItem.Latitude = bucketListItem.Latitude;
            existingBucketListItem.Longitude = bucketListItem.Longitude;

            this.context.Update(existingBucketListItem);
            this.context.SaveChanges();
        }

        private void InsertBucketListItem(Shared.dto.BucketListItem bucketListItem, string userName)
        {
            var user = this.context.User
                                .Where(x => x.UserName == userName)
                                .FirstOrDefault();

            if (user == null)
            {
                throw new RecordDoesNotExistException("InsertBucketListItem - User does not exist. UserName: " + userName);
            }

            var bucketListItemToSave = new models.BucketListItem
            {
                ListItemName = bucketListItem.Name,
                Created = bucketListItem.Created.ToUniversalTime(),
                Category = bucketListItem.Category,
                Achieved = bucketListItem.Achieved,
                Latitude = bucketListItem.Latitude,
                Longitude = bucketListItem.Longitude
            };

            this.context.BucketListItems.Add(bucketListItemToSave);
            this.context.SaveChanges();

            var bucketListItemUser = new models.BucketListUser
            {
                BucketListItemId = bucketListItemToSave.BucketListItemId,
                UserId = user.UserId
            };

            this.context.BucketListUsers.Add(bucketListItemUser);
            this.context.SaveChanges();
        }

        private IQueryable<models.BucketListItem> Search(IQueryable<models.BucketListItem> bucketListItems, string srchTerm)
        {
            IQueryable<models.BucketListItem> searchedBucketListItems = bucketListItems
                                                                              .Where(x => x.ListItemName.Contains(srchTerm));

            return searchedBucketListItems;
        }

        #endregion
    }
}
