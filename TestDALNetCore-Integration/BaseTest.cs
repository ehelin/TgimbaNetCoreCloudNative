﻿using System;
using System.Linq;
using DALNetCore;
using Shared.misc;
using DALNetCore.interfaces;
using DALNetCore.helpers;
using dto = Shared.dto;
using models = DALNetCore.Models;

namespace TestDALNetCore_Integration
{
    public class BaseTest
    {
        protected string UserName = "user";
        protected string Password = "password";
        protected string Token = "token";
        protected IUserHelper userHelper = new UserHelper();

        protected void RemoveTestUser() 
        {
            var dbContext = GetDbContext(true);

            var user = dbContext.User.Where(x => x.UserName == this.UserName)
                                        .FirstOrDefault();

            if (user != null)
            {
                var dbBucketListItems = from bli in dbContext.BucketListItems
                                        join blu in dbContext.BucketListUsers on bli.BucketListItemId equals blu.BucketListItemId
                                        join u in dbContext.User on blu.UserId equals u.UserId
                                        where u.UserName == this.UserName
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
            // TODO - add boolean back in (temp hack)
            //var dbContext = new DALNetCore.Models.BucketListContext(useTestDb);
            var dbContext = new DALNetCore.Models.BucketListContext();
            return dbContext;
        }

        protected models.User GetDbUser(string token)
        {
            var user = new models.User()
            {
                UserName = this.UserName,
                Salt = "salt",
                Password = this.Password,
                Email = "user@email.com",
                Token = token
            };

            return user;
        }

        protected dto.User GetUser(string token)
        {
            var user = new dto.User()
            {
                UserName = this.UserName,
                Salt = "salt",
                Password = this.Password,
                Email = "user@email.com",
                Token = token
            };

            return user;
        }
    }
}