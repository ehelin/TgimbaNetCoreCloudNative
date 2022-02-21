using NUnit.Framework;
using TgimbaNetCoreWebShared;
using TgimbaNetCoreWebShared.Controllers;

namespace TestTgimbaNetCoreWeb
{
    public class SharedRegistrationControllerTests : BaseTest
    {
        [Test]
        public void TestSharedRegistrationController_GoodRegistration()
        {
            bool goodRegistration = GetController().Registration("base64EncodedGoodUser", "base64EncodedGoodEmail", "base64EncodedGoodPass");

            Assert.AreEqual(true, goodRegistration);
        }

        [Test]
        public void TestSharedRegistrationController_BadRegistration()
        {
            bool goodRegistration = GetController().Registration("base64EncodedBadUser", "base64EncodedBadEmail", "base64EncodedBadPass");

            Assert.AreEqual(false, goodRegistration);
        }

        private SharedRegistrationController GetController()
        {
            SharedRegistrationController controller = new SharedRegistrationController(mockWebClient.Object);

            return controller;
        }
    }
}
