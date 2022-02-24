using NUnit.Framework;
using TgimbaNetCoreWebShared;
using TgimbaNetCoreWebShared.Controllers;

namespace TestTgimbaNetCoreWeb
{
    [NonParallelizable]
    public class SharedRegistrationControllerTests : BaseTest
    {
        [Test]
        public void TestSharedRegistrationController_GoodRegistration()
        {
            Initialize();

            bool goodRegistration = GetController().Registration("base64EncodedGoodUser", "base64EncodedGoodEmail", "base64EncodedGoodPass");

            Assert.AreEqual(true, goodRegistration);
        }

        [Test]
        public void TestSharedRegistrationController_BadRegistration()
        {
            Initialize();

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
