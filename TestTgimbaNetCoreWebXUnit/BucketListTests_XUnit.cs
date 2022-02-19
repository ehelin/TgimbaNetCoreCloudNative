using System;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using Shared.dto;
using Shared.dto.api;
using TgimbaNetCoreWebShared.Controllers;

namespace TestTgimbaNetCoreWeb
{
    public class BucketListTests_XUnit : BaseTest
    {
        #region DeleteBucketListItem

        [Fact]
        public void DeleteBucketListItem_HappyPathTest()
        {
            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);

            var request = GetDeleteListItemRequest();

            IActionResult result = tgimbaApi.DeleteBucketListItem(request.EncodedUserName, request.EncodedToken, request.BucketListItemId);
            GoodResultVerify(result);
            tgimbaService.Verify(x => x.DeleteBucketListItem(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void DeleteBucketListItem_ValidationErrorTest()
        {
            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);

            var request = GetDeleteListItemRequest();

            validationHelper.Setup(x => x.IsValidRequest
                                        (It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                                            .Throws(new ArgumentNullException(""));

            IActionResult result = tgimbaApi.DeleteBucketListItem(request.EncodedUserName, request.EncodedToken, request.BucketListItemId);
            BadResultVerify(result);
            tgimbaService.Verify(x => x.DeleteBucketListItem(
                                        It.IsAny<int>(), It.IsAny<string>(),
                                            It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void DeleteBucketListItem_GeneralErrorTest()
        {
            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);

            var request = GetDeleteListItemRequest();

            tgimbaService.Setup(x => x.DeleteBucketListItem(
                                        It.IsAny<int>(), It.IsAny<string>(),
                                            It.IsAny<string>()))
                                                .Throws(new Exception("I am an exception"));

            IActionResult result = tgimbaApi.DeleteBucketListItem(request.EncodedUserName, request.EncodedToken, request.BucketListItemId);
            BadResultVerify(result, 500);
        }

        #endregion

        #region GetBucketListItem

        [Fact]
        public void GetBucketListItem_HappyPathTest()
        {
            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);

            var request = GetBucketListItemRequest();

            IActionResult result = tgimbaApi.GetBucketListItem(request);
            GoodResultVerify(result);
            tgimbaService.Verify(x => x.GetBucketListItems(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>())
                , Times.Once);
        }

        [Fact]
        public void GetBucketListItem_ValidationErrorTest()
        {
            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);

            var request = GetBucketListItemRequest();

            validationHelper.Setup(x => x.IsValidRequest
                                        (It.IsAny<GetBucketListItemRequest>()))
                                            .Throws(new ArgumentNullException(""));

            IActionResult result = tgimbaApi.GetBucketListItem(request);
            BadResultVerify(result);

            tgimbaService.Verify(x => x.GetBucketListItems(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>())
                , Times.Never);
        }

        [Fact]
        public void GetBucketListItem_GeneralErrorTest()
        {
            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);

            var request = GetBucketListItemRequest();

            tgimbaService.Setup(x => x.GetBucketListItems(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Throws(new Exception("I am an exception"));

            IActionResult result = tgimbaApi.GetBucketListItem(request);
            BadResultVerify(result, 500);
        }

        #endregion

        #region UpsertBucketListItem

        [Fact]
        public void UpsertBucketListItem_HappyPathTest()
        {
            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);

            var request = GetUpsertRequest();

            IActionResult result = tgimbaApi.UpsertBucketListItem(request);
            GoodResultVerify(result);
            tgimbaService.Verify(x => x.UpsertBucketListItem(It.IsAny<BucketListItem>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void LogMessage_HappyPathTest()
        {
            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);

            var request = GetLogMessageRequest();

            IActionResult result = tgimbaApi.Log(request);

            OkResult requestResult = (OkResult)result;

            Assert.NotNull(requestResult);
            Assert.Equal(200, requestResult.StatusCode);
            tgimbaService.Verify(x => x.Log(It.IsAny<string>()), Times.Never);
            tgimbaService.Verify(x => x.LogAuthenticated(It.Is<string>(s => s == request.Message),
                                                            It.IsAny<string>(),
                                                                It.IsAny<string>()),
                                                                    Times.Once);
        }

        [Fact]
        public void LogMessage_ValidationError()
        {
            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);

            var request = GetLogMessageRequest();
            validationHelper.Setup(x => x.IsValidRequest
                                        (It.IsAny<LogMessageRequest>()))
                                            .Throws(new ArgumentNullException(""));

            IActionResult result = tgimbaApi.Log(request);

            Assert.NotNull(result);
            Assert.Equal(500, Convert.ToInt32(System.Net.HttpStatusCode.InternalServerError));
            tgimbaService.Verify(x => x.Log(It.IsAny<string>()), Times.Never);
            tgimbaService.Verify(x => x.LogAuthenticated(It.Is<string>(s => s == request.Message),
                                                        It.IsAny<string>(),
                                                            It.IsAny<string>()),
                                                                Times.Never);
        }

        [Fact]
        public void UpsertBucketListItem_ValidationErrorTest()
        {
            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);

            var request = GetUpsertRequest();

            validationHelper.Setup(x => x.IsValidRequest
                                        (It.IsAny<UpsertBucketListItemRequest>()))
                                            .Throws(new ArgumentNullException(""));

            IActionResult result = tgimbaApi.UpsertBucketListItem(request);
            BadResultVerify(result);
            tgimbaService.Verify(x => x.UpsertBucketListItem(It.IsAny<BucketListItem>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }


        [Fact]
        public void UpsertBucketListItem_GeneralErrorTest()
        {
            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);

            var request = GetUpsertRequest();

            tgimbaService.Setup(x => x.UpsertBucketListItem(It.IsAny<BucketListItem>(),
                                                             It.IsAny<string>(), It.IsAny<string>()))
                                                                .Throws(new Exception("I am an exception"));

            IActionResult result = tgimbaApi.UpsertBucketListItem(request);
            BadResultVerify(result, 500);
        }

        #endregion
    }
}
