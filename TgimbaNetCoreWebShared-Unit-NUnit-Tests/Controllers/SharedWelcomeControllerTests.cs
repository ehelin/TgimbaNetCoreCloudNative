using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using TgimbaNetCoreWebShared.Controllers;

namespace TestTgimbaNetCoreWeb
{
    public class SharedWelcomeControllerTests : BaseTest
    {
        [Test]
        public void TestSharedWelcomeControllerIndex()
        {
            SharedWelcomeController welcomeController = new SharedWelcomeController(this.mockWebClient.Object);

            IActionResult result = welcomeController.Index();
            ViewResult view = (ViewResult)result;

            Assert.IsNotNull(view);
        }
    }
}
