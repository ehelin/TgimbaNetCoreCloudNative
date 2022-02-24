using System.Net.Http;
using NUnit.Framework;
using Moq;
using TgimbaNetCoreWebShared;
using Newtonsoft.Json;
using System.Collections.Generic;
using Shared.dto;

namespace TestTgimbaNetCoreWeb
{
    [NonParallelizable]
    public class WebClientTests : BaseTest
    {
        [Test]
        public void Test_GoodRegistration()
        {
            Initialize();

            mockTgimbaHttpClient.Setup(x => x.Post(It.Is<string>(s => s.Contains("/api/tgimbaapi/processuserregistration")),
                                                        It.IsAny<StringContent>()))
                                                                .Returns("true");

            bool goodRegistration = GetWebClient().Registration("base64EncodedGoodUser", 
                                                                    "base64EncodedGoodEmail", 
                                                                        "base64EncodedGoodPass");

            Assert.AreEqual(true, goodRegistration);
            mockTgimbaHttpClient.Verify(x => x.Post(It.Is<string>(s => s.Contains("/api/tgimbaapi/processuserregistration")),
                                                     It.IsAny<StringContent>())
                                                             , Times.Once);
        }

        [Test]
        public void Test_BadRegistration()
        {
            Initialize();

            mockTgimbaHttpClient.Setup(x => x.Post(It.Is<string>(s => s.Contains("/api/tgimbaapi/processuserregistration")),
                                                        It.IsAny<StringContent>()))
                                                                .Returns("false");

            bool goodRegistration = GetWebClient().Registration("base64EncodedBadUser", 
                                                                      "base64EncodedBadEmail", 
                                                                           "base64EncodedBadPass");

            Assert.AreEqual(false, goodRegistration);
            mockTgimbaHttpClient.Verify(x => x.Post(It.Is<string>(s => s.Contains("/api/tgimbaapi/processuserregistration")),
                                                     It.IsAny<StringContent>())
                                                             , Times.Once);
        }

        [Test]
        public void Test_GoodLogin()
        {
            Initialize();

            mockTgimbaHttpClient.Setup(x => x.Post(It.Is<string>(s => s.Contains("/api/tgimbaapi/processuser")),
                                                      It.IsAny<StringContent>()))
                                                              .Returns("token");

            string token = GetWebClient().Login("base64EncodedGoodUser", "base64EncodedGoodPass");

            Assert.AreEqual("token", token);
            mockTgimbaHttpClient.Verify(x => x.Post(It.Is<string>(s => s.Contains("/api/tgimbaapi/processuser")),
                                                      It.IsAny<StringContent>())
                                                                , Times.Once);
        }

        [Test]
        public void Test_BadLogin()
        {
            Initialize();

            mockTgimbaHttpClient.Setup(x => x.Post(It.Is<string>(s => s.Contains("/api/tgimbaapi/processuser")),
                                                      It.IsAny<StringContent>()))
                                                              .Returns("");

            string token = GetWebClient().Login("base64EncodedBadUser", "base64EncodedBadPass");

            Assert.AreEqual("", token);
            mockTgimbaHttpClient.Verify(x => x.Post(It.Is<string>(s => s.Contains("/api/tgimbaapi/processuser")),
                                                      It.IsAny<StringContent>())
                                                                , Times.Once);
        }
        
        [Test]
        public void Test_GoodAddBucketListItem()
        {
            Initialize();

            var bucketListItemModel = GetBucketListItemModel("base64EncodedGoodUser", "newBucketListItem", null, true);
            mockTgimbaHttpClient.Setup(x => x.Post(It.Is<string>(s => s.Contains("/api/tgimbaapi/upsert")),
                                                      It.IsAny<StringContent>()))
                                                              .Returns("true");

            var bucketListAdded = GetWebClient().AddBucketListItem(bucketListItemModel, "base64EncodedGoodUser", "base64EncodedGoodToken");

            Assert.IsTrue(bucketListAdded);
            mockTgimbaHttpClient.Verify(x => x.Post(It.Is<string>(s => s.Contains("/api/tgimbaapi/upsert")),
                                                      It.IsAny<StringContent>())
                                                                , Times.Once);
        }

        [Test]
        public void Test_BadAddBucketListItem()
        {
            Initialize();

            var bucketListItemModel = GetBucketListItemModel("base64EncodedGoodUser", "newBucketListItem", null, true);
            mockTgimbaHttpClient.Setup(x => x.Post(It.Is<string>(s => s.Contains("/api/tgimbaapi/upsert")),
                                                      It.IsAny<StringContent>()))
                                                              .Returns("false");

            var bucketListAdded = GetWebClient().AddBucketListItem(bucketListItemModel, "base64EncodedGoodUser", "base64EncodedGoodToken");

            Assert.IsFalse(bucketListAdded);
            mockTgimbaHttpClient.Verify(x => x.Post(It.Is<string>(s => s.Contains("/api/tgimbaapi/upsert")),
                                                      It.IsAny<StringContent>())
                                                                , Times.Once);
        }

        [Test]
        public void Test_GoodEditBucketListItem()
        {
            Initialize();

            var bucketListItemModel = GetBucketListItemModel("base64EncodedGoodUser", "newBucketListItem", null, true);
            mockTgimbaHttpClient.Setup(x => x.Post(It.Is<string>(s => s.Contains("/api/tgimbaapi/upsert")),
                                                      It.IsAny<StringContent>()))
                                                              .Returns("true");

            var bucketListAdded = GetWebClient().AddBucketListItem(bucketListItemModel, "base64EncodedGoodUser", "base64EncodedGoodToken");

            Assert.IsTrue(bucketListAdded);
            mockTgimbaHttpClient.Verify(x => x.Post(It.Is<string>(s => s.Contains("/api/tgimbaapi/upsert")),
                                                      It.IsAny<StringContent>())
                                                                , Times.Once);
        }

        [Test]
        public void Test_BadEditBucketListItem()
        {
            Initialize();

            var bucketListItemModel = GetBucketListItemModel("base64EncodedGoodUser", "newBucketListItem", null, true);
            mockTgimbaHttpClient.Setup(x => x.Post(It.Is<string>(s => s.Contains("/api/tgimbaapi/upsert")),
                                                      It.IsAny<StringContent>()))
                                                              .Returns("false");

            var bucketListAdded = GetWebClient().AddBucketListItem(bucketListItemModel, "base64EncodedGoodUser", "base64EncodedGoodToken");

            Assert.IsFalse(bucketListAdded);
            mockTgimbaHttpClient.Verify(x => x.Post(It.Is<string>(s => s.Contains("/api/tgimbaapi/upsert")),
                                                      It.IsAny<StringContent>())
                                                                , Times.Once);
        }

        [Test]
        public void Test_GoodGetBucketListItems()
        {
            Initialize();

            var bucketListItemModel = GetBucketListItemModel("base64EncodedGoodUser", 
                                                                "newBucketListItem", 
                                                                    null, 
                                                                        true);
            var bucketListItem = new BucketListItem()
            {
                Name = "newBucketListItem",
                Created = System.DateTime.Now,
                Category = "Hot",
                Achieved = true,
                Latitude = (decimal)1.1,
                Longitude = (decimal)2.1
            };
            var bucketListItems = new List<BucketListItem>();
            bucketListItems.Add(bucketListItem);
            var bucketListItemsToReturn = JsonConvert.SerializeObject(bucketListItems);

            mockTgimbaHttpClient.Setup(x => x.Get(It.Is<string>(s => s.Contains("/api/tgimbaapi/getbucketlistitems")), 
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(bucketListItemsToReturn);

            var user = Shared.misc.Utilities.EncodeClientBase64String("base64EncodedGoodUser");
            var results = GetWebClient().GetBucketListItems(user,
                                                                    "base64EncodedGoodSortString",
                                                                    "base64EncodedGoodToken",
                                                                    "base64EncodedGoodSrchTerm");

            Assert.IsNotNull(results);
            Assert.AreEqual("newBucketListItem", results[0].Name);
            mockTgimbaHttpClient.Verify(x => x.Get(It.Is<string>(s => s.Contains("/api/tgimbaapi/getbucketlistitems")),
                It.IsAny<string>(),
                It.IsAny<string>())
                , Times.Once);
        }

        [Test]
        public void Test_BadGetBucketListItems()
        {
            Initialize();

            mockTgimbaHttpClient.Setup(x => x.Get(It.Is<string>(s => s.Contains("/api/tgimbaapi/getbucketlistitems")),
             It.IsAny<string>(),
             It.IsAny<string>()))
             .Returns("");

            var bucketItemList = GetWebClient().GetBucketListItems("", "", "");

            Assert.IsNull(bucketItemList);
            mockTgimbaHttpClient.Verify(x => x.Get(It.Is<string>(s => s.Contains("/api/tgimbaapi/getbucketlistitems")),
              It.IsAny<string>(),
              It.IsAny<string>())
              , Times.Once);
        }

        private IWebClient GetWebClient()
        {
            IWebClient webClient = new WebClient("https://api.tgimba.com", mockTgimbaHttpClient.Object);

            return webClient;
        }
    }
}
