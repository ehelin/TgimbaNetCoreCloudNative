using Shared.interfaces;
using System;
using Shared.dto.api;
using TgimbaNetCoreWebShared;
using Xunit;

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

        [Theory]
        [InlineData(1, false, false)]
        [InlineData(0, false, true)]
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
                Assert.False(validationErrorExpected);
            }
            catch (Exception ex)
            {
                Assert.True(validationErrorExpected);
                Assert.True(ex is ArgumentNullException);
            }
        }

        #endregion

        #region IsValidRequest - GetBucketListItemRequest

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
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
                Assert.False(validationErrorExpected);
            }
            catch (Exception ex)
            {
                Assert.True(validationErrorExpected);
                Assert.True(ex is ArgumentNullException);
            }
        }

        #endregion

        #region IsValidRequest - LoginRequest

        [Theory]
        [InlineData("userName", "password", false, false)]     // happy path
        [InlineData(null, "password", false, true)]            // null userName
        [InlineData("", "password", false, true)]              // empty userName
        [InlineData("userName", null, false, true)]            // null password
        [InlineData("userName", "", false, true)]              // empty password
        [InlineData(null, null, true, true)]                   // null request
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
                Assert.False(validationErrorExpected);
            }
            catch (Exception ex)
            {
                Assert.True(validationErrorExpected);
                Assert.True(ex is ArgumentNullException);
            }
        }

        #endregion

        #region IsValidRequest - RegistrationRequest

        [Theory]
        [InlineData("email", false, false)]        // happy path
        [InlineData(null, false, true)]            // null email
        [InlineData("", false, true)]              // empty email
        [InlineData(null, true, true)]             // null request
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
                Assert.False(validationErrorExpected);
            }
            catch (Exception ex)
            {
                Assert.True(validationErrorExpected);
                Assert.True(ex is ArgumentNullException);
            }
        }

        #endregion

        #region IsValidRequest - TokenRequest

        [Theory]
        [InlineData("userName", "token", false, false)]        // happy path
        [InlineData(null, "token", false, true)]               // null userName
        [InlineData("", "token", false, true)]                 // empty userName
        [InlineData("userName", null, false, true)]            // null token
        [InlineData("userName", "", false, true)]              // empty token
        [InlineData(null, null, true, true)]                   // null request
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
                Assert.False(validationErrorExpected);
            }
            catch (Exception ex)
            {
                Assert.True(validationErrorExpected);
                Assert.True(ex is ArgumentNullException);
            }
        }

        #endregion

        #region IsValidRequest - UpsertBucketListItemRequest

        [Theory]
        [InlineData(false, false)]        // happy path
        [InlineData(true, true)]          // null request
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
                Assert.False(validationErrorExpected);
            }
            catch (Exception ex)
            {
                Assert.True(validationErrorExpected);
                Assert.True(ex is ArgumentNullException);
            }
        }

        #endregion
    }
}
