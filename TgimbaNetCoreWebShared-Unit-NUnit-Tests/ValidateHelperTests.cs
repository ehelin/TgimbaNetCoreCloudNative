using NUnit.Framework;
using Shared.interfaces;
using System;
using Shared.dto.api;
using TgimbaNetCoreWebShared;

namespace TestTgimbaNetCoreWeb
{
    public class ValidateHelperTests : BaseTest
    {
        private IValidationHelper sut = null;

        public ValidateHelperTests()
        {
            sut = new ValidationHelper();
        }

        #region IsValidRequest - DeleteBucketListItemRequest

        [TestCase(1, false, false)]
        [TestCase(0, false, true)]
        public void IsValidRequest_DeleteBucketListItemRequest_Tests
        (
            int id,
            bool nullRequest,
            bool validationErrorExpected
        )
        {
            DeleteBucketListItemRequest request = null;
            var token = SetTokenRequest();

            if (!nullRequest)
            {
                request = new DeleteBucketListItemRequest()
                {
                    BucketListItemId = id,
                    EncodedToken = token.EncodedToken,
                    EncodedUserName = token.EncodedUserName
                };
            }

            try
            {
                sut.IsValidRequest(request.EncodedUserName, request.EncodedToken, request.BucketListItemId);
                Assert.IsFalse(validationErrorExpected);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(validationErrorExpected);
                Assert.IsTrue(ex is ArgumentNullException);
            }
        }

        #endregion

        #region IsValidRequest - GetBucketListItemRequest

        [TestCase(false, false)]
        [TestCase(true, true)]
        public void IsValidRequest_GetBucketListItemRequest_Tests
        (
            bool nullRequest,
            bool validationErrorExpected
        )
        {
            GetBucketListItemRequest request = null;
            var token = SetTokenRequest();

            if (!nullRequest)
            {
                request = new GetBucketListItemRequest()
                {
                    EncodedUserName = token.EncodedUserName,
                    EncodedToken = token.EncodedToken
                };
            }

            try
            {
                sut.IsValidRequest(request);
                Assert.IsFalse(validationErrorExpected);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(validationErrorExpected);
                Assert.IsTrue(ex is ArgumentNullException);
            }
        }

        #endregion

        #region IsValidRequest - LoginRequest

        [TestCase("userName", "password", false, false)]     // happy path
        [TestCase(null, "password", false, true)]            // null userName
        [TestCase("", "password", false, true)]              // empty userName
        [TestCase("userName", null, false, true)]            // null password
        [TestCase("userName", "", false, true)]              // empty password
        [TestCase(null, null, true, true)]                   // null request
        public void IsValidRequest_LoginRequest_Tests
        (
            string userName,
            string password,
            bool nullRequest,
            bool validationErrorExpected
        )
        {
            LoginRequest request = null;

            if (!nullRequest)
            {
                request = SetLoginRequest(userName, password);
            }

            try
            {
                sut.IsValidRequest(request);
                Assert.IsFalse(validationErrorExpected);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(validationErrorExpected);
                Assert.IsTrue(ex is ArgumentNullException);
            }
        }

        #endregion

        #region IsValidRequest - RegistrationRequest

        [TestCase("email", false, false)]        // happy path
        [TestCase(null, false, true)]            // null email
        [TestCase("", false, true)]              // empty email
        [TestCase(null, true, true)]             // null request
        public void IsValidRequest_RegistrationRequest_Tests
        (
            string email,
            bool nullRequest,
            bool validationErrorExpected
        )
        {
            RegistrationRequest request = null;

            if (!nullRequest)
            {
                request = new RegistrationRequest()
                {
                    Login = SetLoginRequest(),
                    EncodedEmail = email
                };
            }

            try
            {
                sut.IsValidRequest(request);
                Assert.IsFalse(validationErrorExpected);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(validationErrorExpected);
                Assert.IsTrue(ex is ArgumentNullException);
            }
        }

        #endregion

        #region IsValidRequest - TokenRequest

        [TestCase("userName", "token", false, false)]        // happy path
        [TestCase(null, "token", false, true)]               // null userName
        [TestCase("", "token", false, true)]                 // empty userName
        [TestCase("userName", null, false, true)]            // null token
        [TestCase("userName", "", false, true)]              // empty token
        [TestCase(null, null, true, true)]                   // null request
        public void IsValidRequest_TokenRequest_Tests
        (
            string userName,
            string token,
            bool nullRequest,
            bool validationErrorExpected
        )
        {
            TokenRequest request = null;

            if (!nullRequest)
            {
                request = SetTokenRequest(userName, token);
            }

            try
            {
                sut.IsValidRequest(request);
                Assert.IsFalse(validationErrorExpected);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(validationErrorExpected);
                Assert.IsTrue(ex is ArgumentNullException);
            }
        }

        #endregion

        #region IsValidRequest - UpsertBucketListItemRequest

        [TestCase(false, false)]        // happy path
        [TestCase(true, true)]          // null request
        public void IsValidRequest_UpsertBucketListItemRequest_Tests
        (
            bool nullRequest,
            bool validationErrorExpected
        )
        {
            UpsertBucketListItemRequest request = null;

            if (!nullRequest)
            {
                request = new UpsertBucketListItemRequest()
                {
                    BucketListItem = new Shared.dto.BucketListItem(),
                    Token = SetTokenRequest()
                };
            }

            try
            {
                sut.IsValidRequest(request);
                Assert.IsFalse(validationErrorExpected);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(validationErrorExpected);
                Assert.IsTrue(ex is ArgumentNullException);
            }
        }

        #endregion
    }
}
