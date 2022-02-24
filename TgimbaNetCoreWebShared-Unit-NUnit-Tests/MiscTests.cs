using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;
using Shared;
using Shared.dto;
using Shared.dto.api;
using Shared.interfaces;
using TgimbaNetCoreWebShared.Controllers;

namespace TestTgimbaNetCoreWeb
{
    [NonParallelizable]
    public class MiscTests : BaseTest
    {
        #region GetSystemBuildStatistics

        [Test]
        public void GetSystemBuildStatistics_HappyPathTest()
        {
            Initialize();

            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);
            var createdDate = DateTime.UtcNow.ToString();
            var systemBuildStatisticsToReturn = new List<SystemBuildStatistic>();
            systemBuildStatisticsToReturn.Add(new SystemBuildStatistic()
            {
                Start = createdDate,
                End = createdDate,
                BuildNumber = "I am a build number",
                Status = "I am a status",
            });

            tgimbaService.Setup(x => x.GetSystemBuildStatistics(It.IsAny<string>(),
                                                                  It.IsAny<string>()))
                                                                     .Returns(systemBuildStatisticsToReturn);

            IActionResult result = tgimbaApi.GetSystemBuildStatistics("encodedUser", "encodedToken");
            OkObjectResult requestResult = (OkObjectResult)result;

            Assert.IsNotNull(requestResult);
            Assert.AreEqual(200, requestResult.StatusCode);
            tgimbaService.Verify(x => x.GetSystemBuildStatistics("encodedUser", "encodedToken"), Times.Once);
            var systemStatistics = (List<SystemBuildStatistic>)requestResult.Value;
            Assert.AreEqual(1, systemStatistics.Count);
            Assert.AreEqual(systemBuildStatisticsToReturn, systemStatistics);
        }

        [Test]
        public void GetSystemBuildStatistics_NoResultNullCollection()
        {
            Initialize();

            var tgimbaService = new Mock<ITgimbaService>();
            var validationHelper = new Mock<IValidationHelper>();
            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);
            List<SystemBuildStatistic> systemBuildStatisticsToReturn = null;

            tgimbaService.Setup(x => x.GetSystemBuildStatistics(It.IsAny<string>(),
                                                                  It.IsAny<string>()))
                                                                    .Returns(systemBuildStatisticsToReturn);

            IActionResult result = tgimbaApi.GetSystemBuildStatistics("encodedUser", "encodedToken");
            StatusCodeResult requestResult = (StatusCodeResult)result;

            Assert.IsNotNull(requestResult);
            Assert.AreEqual(404, requestResult.StatusCode);
        }

        [Test]
        public void GetSystemBuildStatistics_NoResultEmptyCollection()
        {
            Initialize();

            var tgimbaService = new Mock<ITgimbaService>();
            var validationHelper = new Mock<IValidationHelper>();
            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);
            var systemBuildStatisticsToReturn = new List<SystemBuildStatistic>();

            tgimbaService.Setup(x => x.GetSystemBuildStatistics(It.IsAny<string>(),
                                                                  It.IsAny<string>()))
                                                                    .Returns(systemBuildStatisticsToReturn);

            IActionResult result = tgimbaApi.GetSystemBuildStatistics("encodedUser", "encodedToken");
            StatusCodeResult requestResult = (StatusCodeResult)result;

            Assert.IsNotNull(requestResult);
            Assert.AreEqual(404, requestResult.StatusCode);
        }

        [Test]
        public void GetSystemBuildStatistics_GeneralErrorTest()
        {
            Initialize();

            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);
            var exception = "I am an exception";
            tgimbaService.Setup(x => x.GetSystemBuildStatistics(It.IsAny<string>(),
                                                                  It.IsAny<string>()))
                                                                     .Throws(new Exception(exception));
            IActionResult result = tgimbaApi.GetSystemBuildStatistics("encodedUser", "encodedToken");
            StatusCodeResult requestResult = (StatusCodeResult)result;

            tgimbaService.Verify(x => x.Log(It.Is<string>(s => s == exception)), Times.Once);
            Assert.IsNotNull(requestResult);
            Assert.AreEqual(500, requestResult.StatusCode);
        }

        #endregion

        #region GetSystemStatistics

        [Test]
        public void GetSystemStatistics_HappyPathTest()
        {
            Initialize();

            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);
            var createdDate = DateTime.UtcNow.ToString();
            var systemStatisticsToReturn = new List<SystemStatistic>();
            systemStatisticsToReturn.Add(new SystemStatistic()
            {
                WebSiteIsUp = true,
                DatabaseIsUp = true,
                AzureFunctionIsUp = true,
                Created = createdDate,
            });
            tgimbaService.Setup(x => x.GetSystemStatistics(It.IsAny<string>(),
                                                                  It.IsAny<string>()))
                                                                        .Returns(systemStatisticsToReturn);

            IActionResult result = tgimbaApi.GetSystemStatistics("encodedUser", "encodedToken");
            OkObjectResult requestResult = (OkObjectResult)result;

            Assert.IsNotNull(requestResult);
            Assert.AreEqual(200, requestResult.StatusCode);
            tgimbaService.Verify(x => x.GetSystemStatistics(It.IsAny<string>(),
                                                                  It.IsAny<string>())
                                                                       , Times.Once);
            var systemStatistics = (List<SystemStatistic>)requestResult.Value;
            Assert.AreEqual(1, systemStatistics.Count);
            Assert.AreEqual(systemStatisticsToReturn, systemStatistics);
        }

        [Test]
        public void GetSystemStatistics_NoResultNullCollection()
        {
            Initialize();

            var tgimbaService = new Mock<ITgimbaService>();
            var validationHelper = new Mock<IValidationHelper>();
            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);
            List<SystemStatistic> systemStatisticsToReturn = null;

            tgimbaService.Setup(x => x.GetSystemStatistics(It.IsAny<string>(),
                                                                  It.IsAny<string>()))
                                                                    .Returns(systemStatisticsToReturn);

            IActionResult result = tgimbaApi.GetSystemStatistics("encodedUser", "encodedToken");
            StatusCodeResult requestResult = (StatusCodeResult)result;

            Assert.IsNotNull(requestResult);
            Assert.AreEqual(404, requestResult.StatusCode);
        }

        [Test]
        public void GetSystemStatistics_NoResultEmptyCollection()
        {
            Initialize();

            var tgimbaService = new Mock<ITgimbaService>();
            var validationHelper = new Mock<IValidationHelper>();
            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);
            var systemStatisticsToReturn = new List<SystemStatistic>();

            tgimbaService.Setup(x => x.GetSystemStatistics(It.IsAny<string>(),
                                                                  It.IsAny<string>()))
                                                                        .Returns(systemStatisticsToReturn);

            IActionResult result = tgimbaApi.GetSystemStatistics("encodedUser", "encodedToken");
            StatusCodeResult requestResult = (StatusCodeResult)result;

            Assert.IsNotNull(requestResult);
            Assert.AreEqual(404, requestResult.StatusCode);
        }

        [Test]
        public void GetSystemStatistics_GeneralErrorTest()
        {
            Initialize();

            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);
            var exception = "I am an exception";
            tgimbaService.Setup(x => x.GetSystemStatistics(It.IsAny<string>(),
                                                                  It.IsAny<string>()))
                                                                        .Throws(new Exception(exception));
            IActionResult result = tgimbaApi.GetSystemStatistics("encodedUser", "encodedToken");
            StatusCodeResult requestResult = (StatusCodeResult)result;

            tgimbaService.Verify(x => x.Log(It.Is<string>(s => s == exception)), Times.Once);
            Assert.IsNotNull(requestResult);
            Assert.AreEqual(500, requestResult.StatusCode);
        }

        #endregion

        #region Log

        [Test]
        public void Log_HappyPathTest()
        {
            Initialize();

            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);
            var request = new LogMessageRequest()
            {
                Token = new TokenRequest()
                {
                    EncodedUserName = "encodeUser",
                    EncodedToken = "encodedToken"
                },
                Message = "IAmALogMessage"
            };
            IActionResult result = tgimbaApi.Log(request);
            OkResult requestResult = (OkResult)result;

            Assert.IsNotNull(requestResult);
            Assert.AreEqual(200, requestResult.StatusCode);
            tgimbaService.Verify(x => x.LogAuthenticated(It.Is<string>(s => s == request.Message),
                                                            It.IsAny<string>(),
                                                              It.IsAny<string>())
                                                               , Times.Once);
        }

        [Test]
        public void Log_GeneralErrorTest()
        {
            Initialize();

            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);
            tgimbaService.Setup(x => x.LogAuthenticated(It.IsAny<string>(),
                                                          It.IsAny<string>(),
                                                            It.IsAny<string>()))
                                                                  .Throws(new Exception("I am an exception"));
            var request = new LogMessageRequest()
            {
                Token = new TokenRequest()
                {
                    EncodedUserName = "encodeUser",
                    EncodedToken = "encodedToken"
                },
                Message = "IAmALogMessage"
            };
            IActionResult result = tgimbaApi.Log(request);
            StatusCodeResult requestResult = (StatusCodeResult)result;

            Assert.IsNotNull(requestResult);
            Assert.AreEqual(500, requestResult.StatusCode);
        }

        #endregion

        #region Test Result

        [Test]
        public void GetTestResult_HappyPathTest()
        {
            Initialize();

            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);
            tgimbaService.Setup(x => x.GetTestResult())
                            .Returns(Constants.API_TEST_RESULT);

            IActionResult result = tgimbaApi.GetTestResult();
            OkObjectResult requestResult = (OkObjectResult)result;

            Assert.IsNotNull(requestResult);
            Assert.AreEqual(200, requestResult.StatusCode);
            tgimbaService.Verify(x => x.GetTestResult(), Times.Once);
            var testResult = (string)requestResult.Value;
            Assert.AreEqual(Constants.API_TEST_RESULT, testResult);
        }

        [Test]
        public void GetTestResult_GeneralErrorTest()
        {
            Initialize();

            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);
            var exception = "I am an exception";
            tgimbaService.Setup(x => x.GetTestResult())
                            .Throws(new Exception(exception));

            IActionResult result = tgimbaApi.GetTestResult();
            StatusCodeResult requestResult = (StatusCodeResult)result;

            tgimbaService.Verify(x => x.Log(It.Is<string>(s => s == exception)), Times.Once);
            Assert.IsNotNull(requestResult);
            Assert.AreEqual(500, requestResult.StatusCode);
        }

        #endregion
    }
}
