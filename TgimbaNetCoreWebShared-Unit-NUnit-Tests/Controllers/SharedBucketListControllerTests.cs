using NUnit.Framework;
using TgimbaNetCoreWebShared.Controllers;
using TgimbaNetCoreWebShared;

namespace TestTgimbaNetCoreWeb
{
    public class SharedBucketListControllerTests : BaseTest
    {
        [Test]
        public void TestSharedBucketListController_Initialize()
        {
            var userAgent = "IAmAUserAgent";
            var initializeResult = GetController().Initialize(userAgent);

            Assert.IsNotNull(initializeResult);
            Assert.IsFalse(initializeResult.IsMobile);
            Assert.AreEqual(Utilities.GetAvailableSortingAlgorithms().Length
                            , initializeResult.AvailableSortingAlgorithms.Length);
            Assert.AreEqual(Utilities.GetAvailableSearchingAlgorithms().Length
                            , initializeResult.AvailableSearchingAlgorithms.Length);
        }

        [Test]
        public void TestSharedBucketListController_EditBucketListItem_GoodParameters()
        {
            var bucketListItem = GetBucketListItemModel("base64EncodedGoodUser", "editedBucketListItem", "123", true);
            var itemUpdated = GetController().EditBucketListItem(bucketListItem, "base64EncodedGoodUser", "base64EncodedGoodToken");

            Assert.IsTrue(itemUpdated);
        }

        [Test]
        public void TestSharedBucketListController_EditBucketListItem_BadParameters()
        {
            var bucketListItem = GetBucketListItemModel("base64EncodedGoodUser", "editedBucketListItem", null, true);
            var itemUpdated = GetController().EditBucketListItem(bucketListItem, "base64EncodedBadUser", "base64EncodedBadToken");

            Assert.IsFalse(itemUpdated);
        }

        [Test]
        public void TestSharedBucketListController_DeleteBucketListItem_GoodParameters()
        {
            var itemDeleted = GetController().DeleteBucketListItem("123", "base64EncodedGoodUser", "base64EncodedGoodToken");

            Assert.IsTrue(itemDeleted);
        }

        [Test]
        public void TestSharedBucketListController_DeleteBucketListItem_BadParameters()
        {
            var itemDeleted = GetController().DeleteBucketListItem(null, "base64EncodedBadUser", "base64EncodedBadToken");

            Assert.IsFalse(itemDeleted);
        }

        [Test]
        public void TestSharedBucketListController_AddBucketListItem_GoodParameters()
        {
            var bucketListItem = GetBucketListItemModel("base64EncodedGoodUser", "newBucketListItem", null, true);
            var itemAdded = GetController().AddBucketListItem(bucketListItem, "base64EncodedGoodUser", "base64EncodedGoodToken");

            Assert.IsTrue(itemAdded);
        }

        [Test]
        public void TestSharedBucketListController_AddBucketListItem_BadParameters()
        {
            var itemAdded = GetController().AddBucketListItem(null, "base64EncodedBadUser", "base64EncodedBadToken");

            Assert.IsFalse(itemAdded);
        }

        [Test]
        public void TestSharedBucketListController_GetBucketListItems_GoodParameters()
        {
            var user = Shared.misc.Utilities.EncodeClientBase64String("base64EncodedGoodUser");
            var bucketListItems = GetController().GetBucketListItems(user,
                                                                      "base64EncodedGoodSortString",
                                                                      "base64EncodedGoodToken",
                                                                      "base64EncodedGoodSrchTerm",
                                                                      "base64EncodedGoodSortType",
                                                                      "base64EncodedGoodSearchType");

            Assert.IsNotNull(bucketListItems);
            Assert.AreEqual(1, bucketListItems.Count);
        }

        [Test]
        public void TestSharedBucketListController_GetBucketListItems_BadParameters()
        {
            var bucketListItems = GetController().GetBucketListItems("base64EncodedBadUser",
                                                                      "base64EncodedBadSortString",
                                                                      "base64EncodedBadToken",
                                                                      "base64EncodedGoodSrchTerm");

            Assert.IsNull(bucketListItems);
        }

        private SharedBucketListController GetController()
        {
            SharedBucketListController controller = new SharedBucketListController(this.mockWebClient.Object);

            return controller;
        }
    }
}
