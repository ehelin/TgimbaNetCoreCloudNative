using Microsoft.AspNetCore.Mvc;
using Xunit;
using TgimbaNetCoreWebShared.Controllers;

namespace TestTgimbaNetCoreWeb
{
    public class SharedWelcomeControllerTest_XUnit : BaseTest
    {
        [Fact]
        public void TestSharedWelcomeControllerIndex()
        {
            SharedWelcomeController welcomeController = new SharedWelcomeController(this.mockWebClient.Object);

            IActionResult result = welcomeController.Index();
            ViewResult view = (ViewResult)result;

            Assert.NotNull(view);
        }
    }
}
