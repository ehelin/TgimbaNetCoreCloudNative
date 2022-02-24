using NUnit.Framework;
using TgimbaNetCoreWebShared;
using TgimbaNetCoreWebShared.Controllers;

namespace TestTgimbaNetCoreWeb
{
    [NonParallelizable]
    public class SharedLoginControllerTests : BaseTest
    {
        [Test]
        public void TestSharedLoginController_GoodLogin()
        {
            Initialize();

            var homeController = GetController();

            string token = homeController.Login("base64EncodedGoodUser", "base64EncodedGoodPass");

            Assert.AreEqual("token", token);
        }

        [Test]
        public void TestSharedLoginController_BadLogin()
        {
            Initialize();

            var homeController = GetController();

            string token = homeController.Login("base64EncodedBadUser", "base64EncodedBadPass");

            Assert.AreEqual(null, token);
        }

        private SharedLoginController GetController()
        {
            SharedLoginController controller = new SharedLoginController(mockWebClient.Object);

            return controller;
        }
    }
}
