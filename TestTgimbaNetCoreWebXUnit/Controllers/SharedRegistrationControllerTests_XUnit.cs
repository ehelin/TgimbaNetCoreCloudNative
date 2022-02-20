using Xunit;
using TgimbaNetCoreWebShared;
using TgimbaNetCoreWebShared.Controllers;

namespace TestTgimbaNetCoreWeb
{
    public class SharedRegistrationControllerTests_XUnit : BaseTest
    {
        [Fact]
        public void TestSharedRegistrationController_GoodRegistration()
        {
            bool goodRegistration = GetController().Registration("base64EncodedGoodUser", "base64EncodedGoodEmail", "base64EncodedGoodPass");

            Assert.True(goodRegistration);
        }

        [Fact]
        public void TestSharedRegistrationController_BadRegistration()
        {
            bool goodRegistration = GetController().Registration("base64EncodedBadUser", "base64EncodedBadEmail", "base64EncodedBadPass");

            Assert.False(goodRegistration);
        }

        private SharedRegistrationController GetController()
        {
            SharedRegistrationController controller = new SharedRegistrationController(mockWebClient.Object);

            return controller;
        }
    }
}