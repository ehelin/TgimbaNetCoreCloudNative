using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using Shared.dto;
using Shared;
using Shared.misc.testUtilities;

namespace TestAPINetCore_Unit
{
    public class MiscTests_XUnit : BaseTest
    {
        //[TestCleanup]
        public void Cleanup()
        {
            TestUtilities.ClearEnvironmentalVariablesForUnitTests();
        }

        //[TestInitialize]
        public void SetUp()
        {
            TestUtilities.SetEnvironmentalVariablesForUnitTests();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void LogAuthenticated_HappyPathTest(bool isValidToken)
        {
            var msg = "I am a message";
            var testToken = SetUpTokenForTesting(isValidToken);

            this.service.LogAuthenticated(msg, testToken.EncodedUserName, testToken.EncodedToken);

            if (isValidToken)
            {
                this.mockBucketListData.Verify(x => x.LogMsg(It.Is<string>(s => s.Contains(msg))), Times.Once);
            }
            else
            {
                this.mockBucketListData.Verify(x => x.LogMsg(It.Is<string>(s => s.Contains(msg))), Times.Never);
            }

            TestTokenVerifies(testToken);
        }

        [Fact]
        public void Log_HappyPathTest()
        {
            var msg = "I am a message";

            this.service.Log(msg);

            this.mockBucketListData.Verify(x => x.LogMsg(It.Is<string>(s => s.Contains(msg))), Times.Once);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetSystemStatistics_HappyPathTest(bool isValidToken)
        {
            var testToken = SetUpTokenForTesting(isValidToken);
            var systemStatisticToReturn = new SystemStatistic()
            {
                WebSiteIsUp = true,
                DatabaseIsUp = true,
                AzureFunctionIsUp = true,
                Created = DateTime.UtcNow.ToString()
            };
            IList<SystemStatistic> systemStatisticsToReturn = new List<SystemStatistic>();
            systemStatisticsToReturn.Add(systemStatisticToReturn);
            this.mockBucketListData.Setup(x => x.GetSystemStatistics()).Returns(systemStatisticsToReturn);

            var systemStatistics = this.service.GetSystemStatistics(testToken.EncodedUserName, testToken.EncodedToken);

            if (isValidToken)
            {
                Assert.NotNull(systemStatistics);
                Assert.Equal(systemStatisticToReturn.WebSiteIsUp, systemStatistics[0].WebSiteIsUp);
                Assert.Equal(systemStatisticToReturn.DatabaseIsUp, systemStatistics[0].DatabaseIsUp);
                Assert.Equal(systemStatisticToReturn.AzureFunctionIsUp, systemStatistics[0].AzureFunctionIsUp);
                Assert.Equal(systemStatisticToReturn.Created, systemStatistics[0].Created);
                this.mockBucketListData.Verify(x => x.GetSystemStatistics(), Times.Once);
            }
            else
            {
                Assert.Null(systemStatistics);
            }

            TestTokenVerifies(testToken);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetSystemBuildStatistics_HappyPathTest(bool isValidToken)
        {
            var testToken = SetUpTokenForTesting(isValidToken);
            var commonDate = DateTime.UtcNow.ToString();
            var systemBuildStatisticToReturn = new SystemBuildStatistic()
            {
                Start = commonDate,
                End = commonDate,
                BuildNumber = "build",
                Status = "status"
            };
            var systemBuildStatisticsToReturn = new List<SystemBuildStatistic>();
            systemBuildStatisticsToReturn.Add(systemBuildStatisticToReturn);
            this.mockBucketListData.Setup(x => x.GetSystemBuildStatistics()).Returns(systemBuildStatisticsToReturn);

            var systemBuildStatistics = this.service.GetSystemBuildStatistics(testToken.EncodedUserName, testToken.EncodedToken);

            if (isValidToken)
            {
                Assert.NotNull(systemBuildStatistics);
                Assert.Equal(systemBuildStatisticToReturn.Start, systemBuildStatistics[0].Start);
                Assert.Equal(systemBuildStatisticToReturn.End, systemBuildStatistics[0].End);
                Assert.Equal(systemBuildStatisticToReturn.BuildNumber, systemBuildStatistics[0].BuildNumber);
                Assert.Equal(systemBuildStatisticToReturn.Status, systemBuildStatistics[0].Status);
                this.mockBucketListData.Verify(x => x.GetSystemBuildStatistics(), Times.Once);
            }
            else
            {
                Assert.Null(systemBuildStatistics);
            }

            TestTokenVerifies(testToken);
        }

        [Fact]
        public void GetTestResult_HappyPathTest()
        {
            var testResult = this.service.GetTestResult();

            Assert.NotNull(testResult);
            Assert.Equal(Constants.API_TEST_RESULT, testResult);
        }

        #region Login Demo User Tests

        [Fact]
        public void LoginDemoUser_HappyPathTest()
        {
            var jwtToken = "jwtToken";
            var jwtPrivateKey = "jwtPrivateKey";
            var jwtIssuer = "jwtIssuer";
            var demoUserToReturn = GetUser(1, Constants.DEMO_USER, Constants.DEMO_USER_PASSWORD);
            var expectedHashPasswordParameter = new Password(Constants.DEMO_USER_PASSWORD);
            var expectedHashPasswordToReturn = new Password(Constants.DEMO_USER_PASSWORD);

            LoginDemoUserTest_MockSetup(jwtToken, jwtPrivateKey, jwtIssuer, demoUserToReturn, expectedHashPasswordParameter, expectedHashPasswordToReturn);

            var token = this.service.LoginDemoUser();

            LoginDemoUserTest_Asserts(token, jwtToken, jwtPrivateKey, jwtIssuer, demoUserToReturn, expectedHashPasswordParameter, expectedHashPasswordToReturn);
        }

        [Fact]
        public void LoginDemoUser_PasswordsDoNotMatch()
        {
            var demoUserToReturn = GetUser(1, Constants.DEMO_USER, Constants.DEMO_USER_PASSWORD);
            this.mockBucketListData
                .Setup(x => x.GetUser(It.Is<string>(s => s == Constants.DEMO_USER)))
                    .Returns(demoUserToReturn);
            var token = this.service.LoginDemoUser();

            Assert.Null(token);
        }

        [Fact]
        public void LoginDemoUser_UserDoesNotExist()
        {
            var token = this.service.LoginDemoUser();

            Assert.Null(token);
        }

        #region private methods

        private void LoginDemoUserTest_MockSetup
        (
            string jwtToken,
            string jwtPrivateKey,
            string jwtIssuer,
            User demoUserToReturn,
            Password expectedHashPasswordParameter,
            Password expectedHashPasswordToReturn
        )
        {
            expectedHashPasswordToReturn.SaltedHashedPassword = "saltedDemoUserPassword";

            this.mockBucketListData
                .Setup(x => x.GetUser(It.Is<string>(s => s == Constants.DEMO_USER)))
                    .Returns(demoUserToReturn);
            this.mockGenerator.Setup(x => x.GetJwtPrivateKey()).Returns(jwtPrivateKey);
            this.mockGenerator.Setup(x => x.GetJwtIssuer()).Returns(jwtIssuer);
            this.mockPassword.Setup(x =>
                x.HashPassword
                    (It.Is<Password>(s => s.GetPassword() == expectedHashPasswordParameter.GetPassword())))
                        .Returns(expectedHashPasswordToReturn);
            this.mockPassword.Setup(x =>
                    x.PasswordsMatch
                        (It.Is<Password>(z => z == expectedHashPasswordToReturn)
                        , It.Is<User>(s => s == demoUserToReturn))).Returns(true);
            this.mockGenerator.Setup(x =>
                    x.GetJwtToken
                        (It.Is<string>(z => z == jwtPrivateKey)
                        , It.Is<string>(s => s == jwtIssuer)
                        , It.Is<int>(s => s == Constants.TOKEN_LIFE))).Returns(jwtToken);
        }

        private void LoginDemoUserTest_Asserts
        (
            string token,
            string jwtToken,
            string jwtPrivateKey,
            string jwtIssuer,
            User demoUserToReturn,
            Password expectedHashPasswordParameter,
            Password expectedHashPasswordToReturn
        )
        {
            Assert.NotNull(token);
            Assert.True(token.Length > 0);
            Assert.Equal(jwtToken, token);

            this.mockPassword
                .Verify(x => x.HashPassword(
                    It.Is<Password>(s => s.GetPassword() == expectedHashPasswordParameter.GetPassword()))
                            , Times.Once);
            this.mockPassword.Verify(x =>
                    x.PasswordsMatch
                        (It.Is<Password>(z => z == expectedHashPasswordToReturn)
                        , It.Is<User>(s => s == demoUserToReturn))
                        , Times.Once);
            this.mockGenerator.Verify(x => x.GetJwtPrivateKey(), Times.Once);
            this.mockGenerator.Verify(x => x.GetJwtIssuer(), Times.Once);
            this.mockGenerator.Verify(x =>
                    x.GetJwtToken
                        (It.Is<string>(z => z == jwtPrivateKey)
                        , It.Is<string>(s => s == jwtIssuer)
                        , It.Is<int>(s => s == Constants.TOKEN_LIFE))
                        , Times.Once);
        }

        #endregion

        #endregion
    }
}
