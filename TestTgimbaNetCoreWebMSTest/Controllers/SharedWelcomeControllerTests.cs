using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TgimbaNetCoreWebShared.Controllers;

namespace TestTgimbaNetCoreWeb
{
    [TestClass]
    public class SharedWelcomeControllerTests : BaseTest
    {
        [TestMethod]
        public void TestSharedWelcomeControllerIndex()
        {
            SharedWelcomeController welcomeController = new SharedWelcomeController(this.mockWebClient.Object);

            IActionResult result = welcomeController.Index();
            ViewResult view = (ViewResult)result;

            Assert.IsNotNull(view);
        }
    }
}
