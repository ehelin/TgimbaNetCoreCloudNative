using Xunit;
using TgimbaNetCoreWebShared;
using TgimbaNetCoreWebShared.Controllers;

namespace TestTgimbaNetCoreWeb
{
    public class SharedLoginControllerTests_XUnit : BaseTest
    {
        [Fact]
        public void TestSharedLoginController_GoodLogin()
        {
            var homeController = GetController();

            string token = homeController.Login("base64EncodedGoodUser", "base64EncodedGoodPass");

            Assert.Equal("token", token);
        }

        [Fact]
        public void TestSharedLoginController_BadLogin()
        {
            var homeController = GetController();

            string token = homeController.Login("base64EncodedBadUser", "base64EncodedBadPass");

            Assert.Null(token);
        }

        private SharedLoginController GetController()
        {
            SharedLoginController controller = new SharedLoginController(mockWebClient.Object);

            return controller;
        }
    }
}
