using DALNetCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.interfaces;
using Shared.exceptions;
using System.Collections.Generic;
using Shared.dto;
using Shared.misc.testUtilities;
using models = DALNetCore.Models;

namespace TestDALNetCore_Integration
{
    [TestClass]
    public class UserTests : BaseTest
    {
        [TestCleanup]
        public void Cleanup()
        {
            TestUtilities.ClearEnvironmentalVariablesForIntegrationTests();
        }

        [TestInitialize]
        public void SetUp()
        {
            TestUtilities.SetEnvironmentalVariablesForIntegrationTests();
        }

        [TestMethod]
        public void User_HappyPath_Test()
        {
            RemoveTestUser();

            var token = "token";
            var user = GetUser(token);
            IBucketListData bd = new BucketListData(this.GetDbContext(true), this.userHelper);


            var userId = bd.AddUser(user);
            bd.AddToken(userId, token);

            var savedUser = bd.GetUser(userId);

            Assert.AreEqual(user.UserName, savedUser.UserName);
            Assert.AreEqual(user.Password, savedUser.Password);
            Assert.AreEqual(user.Salt, savedUser.Salt);
            Assert.AreEqual(user.Email, savedUser.Email);
            Assert.AreEqual(token, savedUser.Token);

            bd.DeleteUser(savedUser.UserId);
        }

        [TestMethod]
        public void User_DeleteUser_UserName_DeleteMultiple_Test()
        {
            var userName = "testUser";
            IBucketListData bd = new BucketListData(this.GetDbContext(true), this.userHelper);

            bd.AddToken(bd.AddUser(GetUser("token1", userName)), "token1");
            bd.AddToken(bd.AddUser(GetUser("token2", userName)), "token2");
            bd.AddToken(bd.AddUser(GetUser("token3", userName)), "token3");

            AssertUsersExist(true, false, bd, userName);

            bd.DeleteUserBucketListItems(userName, false);

            AssertUsersExist(false, false, bd, userName);
        }

        [TestMethod]
        public void User_DeleteUserBucketListItems_UserName_DeleteMultiple_Test()
        {
            var userName = "testUser";
            IBucketListData bd = new BucketListData(this.GetDbContext(true), this.userHelper);
            List<User> users = new List<User>();

            var userId1 = bd.AddUser(GetUser("token1", userName));
            var userId2 = bd.AddUser(GetUser("token2", userName));
            var userId3 = bd.AddUser(GetUser("token3", userName));
            bd.AddToken(userId1, "token1");
            bd.AddToken(userId2, "token2");
            bd.AddToken(userId3, "token3");

            var bucketListItems = GetBucketListItems();
            foreach(var bucketListItem in bucketListItems) 
            {
                bd.UpsertBucketListItem(bucketListItem, userName);
            }

            AssertUsersExist(true, true, bd, userName);

            bd.DeleteUserBucketListItems(userName, false);

            AssertUsersExist(false, true, bd, userName);
        }

        [TestMethod]
        public void User_DeleteUserBucketListItems_OnlyDeleteBucketListItems_Test()
        {
            var userName = "testUser";
            IBucketListData bd = new BucketListData(this.GetDbContext(true), this.userHelper);
            List<User> users = new List<User>();

            var userId1 = bd.AddUser(GetUser("token1", userName));
            var userId2 = bd.AddUser(GetUser("token2", userName));
            var userId3 = bd.AddUser(GetUser("token3", userName));
            bd.AddToken(userId1, "token1");
            bd.AddToken(userId2, "token2");
            bd.AddToken(userId3, "token3");

            var bucketListItems = GetBucketListItems();
            foreach (var bucketListItem in bucketListItems)
            {
                bd.UpsertBucketListItem(bucketListItem, userName);
            }

            AssertUsersExist(true, true, bd, userName);

            bd.DeleteUserBucketListItems(userName, true);
            
            var bucketListItemSaved = bd.GetBucketList(userName);
            Assert.IsFalse(bucketListItemSaved.Count > 0);
            var savedUsers = bd.GetUsers("testUser");
            Assert.IsTrue(savedUsers.Count > 0);
        }

        private void AssertUsersExist(bool shouldExist, 
            bool evaluateBucketListItems,
            IBucketListData bd, 
            string userName)
        {
            var savedUsers = bd.GetUsers("testUser");

            if (shouldExist) 
            {
                if (evaluateBucketListItems)
                {
                    var bucketListItems = bd.GetBucketList(userName);
                    Assert.IsTrue(bucketListItems.Count > 0);
                }

                Assert.IsTrue(savedUsers.Count > 0);
            }
            else
            {
                if (evaluateBucketListItems)
                {
                    var bucketListItems = bd.GetBucketList(userName);
                    Assert.IsFalse(bucketListItems.Count > 0);
                }

                Assert.IsFalse(savedUsers.Count > 0);
            }
        }

        [TestMethod]
        public void User_ConvertDbUserToUser_Test()
        {
            var token = "token";
            var dbUser = GetDbUser(token);

            Assert.IsInstanceOfType(dbUser, typeof(models.User));

            var user = this.userHelper.ConvertDbUserToUser(dbUser);

            Assert.IsInstanceOfType(user, typeof(User));
            Assert.IsNotNull(user);
            Assert.AreEqual(user.UserName, dbUser.UserName);
            Assert.AreEqual(user.Password, dbUser.Password);
            Assert.AreEqual(user.Salt, dbUser.Salt);
            Assert.AreEqual(user.Email, dbUser.Email);
            Assert.AreEqual(token, dbUser.Token);
        }

        [TestMethod]
        [ExpectedException(typeof(RecordDoesNotExistException))]
        public void User_AddToken_UserDoesNotExist_Test()
        {
            RemoveTestUser();

            var unknownUserId = -12521;
            IBucketListData bd = new BucketListData(this.GetDbContext(true), this.userHelper);

            bd.AddToken(unknownUserId, this.Token);

            // NOTE: RecordDoesNotExistException is expected
        }

        [TestMethod]
        [ExpectedException(typeof(RecordDoesNotExistException))]
        public void User_GetUser_UserDoesNotExist_Test()
        {
            RemoveTestUser();

            var unknownUserId = -12521;
            IBucketListData bd = new BucketListData(this.GetDbContext(true), this.userHelper);

            bd.GetUser(unknownUserId);

            // NOTE: RecordDoesNotExistException is expected
        }

        [TestMethod]
        [ExpectedException(typeof(RecordDoesNotExistException))]
        public void User_DeleteUser_UserDoesNotExist_Test()
        {
            RemoveTestUser();

            var unknownUserId = -12521;
            IBucketListData bd = new BucketListData(this.GetDbContext(true), this.userHelper);

            bd.DeleteUser(unknownUserId);

            // NOTE: RecordDoesNotExistException is expected
        }
    }
}
