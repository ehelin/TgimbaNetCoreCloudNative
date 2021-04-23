using System;
using System.Linq;
using System.Collections.Generic;
using Shared.misc;
using DALNetCore.interfaces;
using DALNetCore.helpers;
using dto = Shared.dto;
using models = DALNetCore.Models;

namespace TestDALNetCore_Integration
{
    public class BaseTest
    {
        protected const string UserName = "user";
        protected string Password = "password";
        protected string Token = "token";
        protected IUserHelper userHelper = new UserHelper();

        protected void RemoveTestUser() 
        {
            var dbContext = GetDbContext(true);

            var user = dbContext.User.Where(x => x.UserName == UserName)
                                        .FirstOrDefault();

            if (user != null)
            {
                var dbBucketListItems = from bli in dbContext.BucketListItems
                                        join blu in dbContext.BucketListUsers on bli.BucketListItemId equals blu.BucketListItemId
                                        join u in dbContext.User on blu.UserId equals u.UserId
                                        where u.UserName == UserName
                                        select bli;

                foreach(var dbBucketListItem in dbBucketListItems)
                {
                    var dbBucketListUser = dbContext.BucketListUsers
                                                    .Where(x => x.BucketListItemId == dbBucketListItem.BucketListItemId)
                                                    .FirstOrDefault();

                    dbContext.BucketListUsers.Remove(dbBucketListUser);
                    dbContext.BucketListItems.Remove(dbBucketListItem);
                    dbContext.SaveChanges();
                }

                dbContext.User.Remove(user);
                dbContext.SaveChanges();
            }
        }

        protected List<dto.BucketListItem> GetBucketListItems()
        {
            List< dto.BucketListItem> bucketListItems = new List<dto.BucketListItem>();

            bucketListItems.Add(GetBucketListItem("Item1"));
            bucketListItems.Add(GetBucketListItem("Item2"));
            bucketListItems.Add(GetBucketListItem("Item3"));
            bucketListItems.Add(GetBucketListItem("Item4"));
            bucketListItems.Add(GetBucketListItem("Item5"));

            return bucketListItems;
        }

        protected dto.BucketListItem GetBucketListItem(string name = "I am a bucket list item")
        {
            var bucketListItem = new dto.BucketListItem
            {
                Name = name,
                Created = DateTime.Now,
                Category = Enums.BucketListItemTypes.Hot.ToString(),
                Achieved = false,
                Latitude = (decimal)81.12,
                Longitude = (decimal)41.34
            };

            return bucketListItem;
        }

        protected DALNetCore.Models.BucketListContext GetDbContext(bool useTestDb = false)
        {
            var dbContext = new DALNetCore.Models.BucketListContext(useTestDb);
            return dbContext;
        }

        protected models.User GetDbUser(string token)
        {
            var user = new models.User()
            {
                UserName = UserName,
                Salt = "salt",
                Password = this.Password,
                Email = "user@email.com",
                Token = token
            };

            return user;
        }

        protected dto.User GetUser(string token, string userName = UserName)
        {
            var user = new dto.User()
            {
                UserName = userName,
                Salt = "salt",
                Password = this.Password,
                Email = "user@email.com",
                Token = token
            };

            return user;
        }
    }
}
