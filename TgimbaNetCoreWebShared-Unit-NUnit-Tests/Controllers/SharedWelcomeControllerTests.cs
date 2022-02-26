using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using TgimbaNetCoreWebShared.Controllers;

namespace TestTgimbaNetCoreWeb
{
    [NonParallelizable]
    public class SharedWelcomeControllerTests : BaseTest
    {
        public SharedWelcomeControllerTests()
        {
            Initialize();
        }

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
