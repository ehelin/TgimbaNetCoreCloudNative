using Xunit;
using TgimbaNetCoreWebShared.Controllers;
using TgimbaNetCoreWebShared;

namespace TestTgimbaNetCoreWeb
{
    public class SharedBucketListControllerTests : BaseTest
    {
        [Fact]
        public void TestSharedBucketListController_Initialize()
        {
            var userAgent = "IAmAUserAgent";
            var initializeResult = GetController().Initialize(userAgent);

            Assert.NotNull(initializeResult);
            Assert.False(initializeResult.IsMobile);
            Assert.Equal(Utilities.GetAvailableSortingAlgorithms().Length
                            , initializeResult.AvailableSortingAlgorithms.Length);
            Assert.Equal(Utilities.GetAvailableSearchingAlgorithms().Length
                            , initializeResult.AvailableSearchingAlgorithms.Length);
        }

        [Fact]
        public void TestSharedBucketListController_EditBucketListItem_GoodParameters()
        {
            var bucketListItem = GetBucketListItemModel("base64EncodedGoodUser", "editedBucketListItem", "123", true);
            var itemUpdated = GetController().EditBucketListItem(bucketListItem, "base64EncodedGoodUser", "base64EncodedGoodToken");

            Assert.True(itemUpdated);
        }

        [Fact]
        public void TestSharedBucketListController_EditBucketListItem_BadParameters()
        {
            var bucketListItem = GetBucketListItemModel("base64EncodedGoodUser", "editedBucketListItem", null, true);
            var itemUpdated = GetController().EditBucketListItem(bucketListItem, "base64EncodedBadUser", "base64EncodedBadToken");

            Assert.False(itemUpdated);
        }

        [Fact]
        public void TestSharedBucketListController_DeleteBucketListItem_GoodParameters()
        {
            var itemDeleted = GetController().DeleteBucketListItem("123", "base64EncodedGoodUser", "base64EncodedGoodToken");

            Assert.True(itemDeleted);
        }

        [Fact]
        public void TestSharedBucketListController_DeleteBucketListItem_BadParameters()
        {
            var itemDeleted = GetController().DeleteBucketListItem(null, "base64EncodedBadUser", "base64EncodedBadToken");

            Assert.False(itemDeleted);
        }

        [Fact]
        public void TestSharedBucketListController_AddBucketListItem_GoodParameters()
        {
            var bucketListItem = GetBucketListItemModel("base64EncodedGoodUser", "newBucketListItem", null, true);
            var itemAdded = GetController().AddBucketListItem(bucketListItem, "base64EncodedGoodUser", "base64EncodedGoodToken");

            Assert.True(itemAdded);
        }

        [Fact]
        public void TestSharedBucketListController_AddBucketListItem_BadParameters()
        {
            var itemAdded = GetController().AddBucketListItem(null, "base64EncodedBadUser", "base64EncodedBadToken");

            Assert.False(itemAdded);
        }

        [Fact]
        public void TestSharedBucketListController_GetBucketListItems_GoodParameters()
        {
            var user = Shared.misc.Utilities.EncodeClientBase64String("base64EncodedGoodUser");
            var bucketListItems = GetController().GetBucketListItems(user,
                                                                      "base64EncodedGoodSortString",
                                                                      "base64EncodedGoodToken",
                                                                      "base64EncodedGoodSrchTerm",
                                                                      "base64EncodedGoodSortType",
                                                                      "base64EncodedGoodSearchType");

            Assert.NotNull(bucketListItems);
            Assert.Single(bucketListItems);
        }

        [Fact]
        public void TestSharedBucketListController_GetBucketListItems_BadParameters()
        {
            var bucketListItems = GetController().GetBucketListItems("base64EncodedBadUser",
                                                                      "base64EncodedBadSortString",
                                                                      "base64EncodedBadToken",
                                                                      "base64EncodedGoodSrchTerm");

            Assert.Null(bucketListItems);
        }

        private SharedBucketListController GetController()
        {
            SharedBucketListController controller = new SharedBucketListController(this.mockWebClient.Object);

            return controller;
        }
    }
}
