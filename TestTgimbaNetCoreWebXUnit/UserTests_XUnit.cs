using System;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using Shared.dto.api;
using TgimbaNetCoreWebShared.Controllers;

namespace TestTgimbaNetCoreWeb
{
    public class UserTests_XUnit : BaseTest
    {
        #region Process User

        [Fact]
        public void ProcessUser_HappyPathTest()
        {
            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);

            var request = GetLoginRequest();
            var tokenToReturn = "token";
            tgimbaService.Setup(x => x.ProcessUser
                                    (It.Is<string>(s => s == request.EncodedUserName),
                                      It.Is<string>(s => s == request.EncodedPassword)
                                        )).Returns(tokenToReturn);

            IActionResult result = tgimbaApi.ProcessUser(request);
            OkObjectResult requestResult = (OkObjectResult)result;

            Assert.NotNull(requestResult);
            Assert.Equal(200, requestResult.StatusCode);
            tgimbaService.Verify(x => x.ProcessUser
                                        (It.Is<string>(s => s == request.EncodedUserName),
                                            It.Is<string>(s => s == request.EncodedPassword)
                                                ), Times.Once);
            var token = (string)requestResult.Value;
            Assert.Equal(tokenToReturn, token);
        }

        [Fact]
        public void ProcessUser_ValidationErrorTest()
        {
            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);

            var request = GetLoginRequest();

            validationHelper.Setup(x => x.IsValidRequest
                                    (It.IsAny<LoginRequest>()))
                                        .Throws(new ArgumentNullException(""));

            IActionResult result = tgimbaApi.ProcessUser(request);
            BadResultVerify(result);
            tgimbaService.Verify(x => x.ProcessUser(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void ProcessUser_GeneralErrorTest()
        {
            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);

            var request = GetLoginRequest();

            tgimbaService.Setup(x => x.ProcessUser
                                (It.IsAny<string>(), It.IsAny<string>()))
                                     .Throws(new Exception("I am an exception"));

            IActionResult result = tgimbaApi.ProcessUser(request);
            BadResultVerify(result, 500);
        }

        #endregion

        #region Process User Registration

        [Fact]
        public void ProcessUserRegistration_HappyPathTest()
        {
            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);

            var email = "email";
            var userRegisteredToReturn = true;
            var registration = new RegistrationRequest() { Login = GetLoginRequest(), EncodedEmail = email };
            tgimbaService.Setup(x => x.ProcessUserRegistration
                                        (It.Is<string>(s => s == registration.Login.EncodedUserName),
                                            It.Is<string>(s => s == email),
                                                It.Is<string>(s => s == registration.Login.EncodedPassword)
                                                    )).Returns(userRegisteredToReturn);

            IActionResult result = tgimbaApi.ProcessUserRegistration(registration);
            OkObjectResult requestResult = (OkObjectResult)result;

            Assert.NotNull(requestResult);
            Assert.Equal(200, requestResult.StatusCode);
            tgimbaService.Verify(x => x.ProcessUserRegistration
                                        (It.Is<string>(s => s == registration.Login.EncodedUserName),
                                            It.Is<string>(s => s == email),
                                                It.Is<string>(s => s == registration.Login.EncodedPassword)
                                                    ), Times.Once);
            var userRegistered = (bool)requestResult.Value;
            Assert.Equal(userRegisteredToReturn, userRegistered);
        }

        [Fact]
        public void ProcessUserRegistration_ErrorTest()
        {
            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);

            var registration = new RegistrationRequest() { Login = GetLoginRequest(), EncodedEmail = "email" };

            validationHelper.Setup(x => x.IsValidRequest
                                    (It.IsAny<RegistrationRequest>()))
                                        .Throws(new ArgumentNullException(""));

            IActionResult result = tgimbaApi.ProcessUserRegistration(registration);
            BadResultVerify(result);
            tgimbaService.Verify(x => x.ProcessUserRegistration(It.IsAny<string>(), It.IsAny<string>()
                                                                    , It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void ProcessUserRegistration_GeneralErrorTest()
        {
            var tgimbaApi = new SharedTgimbaApiController(this.tgimbaService.Object, this.validationHelper.Object);

            var request = new RegistrationRequest() { Login = GetLoginRequest(), EncodedEmail = "email" };

            tgimbaService.Setup(x => x.ProcessUserRegistration
                                    (It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                                        .Throws(new Exception("I am an exception"));

            IActionResult result = tgimbaApi.ProcessUserRegistration(request);
            BadResultVerify(result, 500);
        }

        #endregion
    }
}
