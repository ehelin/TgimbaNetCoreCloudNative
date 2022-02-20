using System;
using BLLNetCore.Security;
using Xunit;
using Moq;
using Shared;
using Shared.dto;
using Shared.interfaces;
using Shared.misc.testUtilities;

namespace TestAPINetCore_Unit.helpers
{
    public class PasswordHelperTests_XUnit : BaseTest
    {
        private IPassword sut = null;

        public PasswordHelperTests_XUnit() 
        {
            sut = new PasswordHelper();
            SetUp();
        }

        //[TestCleanup]
        public void Cleanup()
        {
            TestUtilities.ClearEnvironmentalVariablesForUnitTests();
        }

        //[TestInitialize]
        public void SetUp()
        {
            TestUtilities.SetEnvironmentalVariablesForUnitTests();
        }

        #region Passwords match

        [Fact]
        public void PasswordsMatch_True()
        {
            var existingUserPassword = "IAmAnExistingUserPassword";
            var existingUserSalt = "IAmAnExistingUserSalt";
            var existingPasswordDto = new Password(existingUserPassword, existingUserSalt);
            var hashedExistingUserSaltedPassword = sut.HashPassword(existingPasswordDto);

            var loginPassword = "IAmAnExistingUserPassword";
            var loginPasswordDto = new Password(loginPassword, existingUserSalt);
            var hashedLoginUserSaltedPassword = sut.HashPassword(loginPasswordDto);

            var user = new User();
            user.Password = hashedExistingUserSaltedPassword.SaltedHashedPassword;
            user.Salt = existingUserSalt;

            var passwordsMatch = sut.PasswordsMatch(hashedLoginUserSaltedPassword, user);

            Assert.True(passwordsMatch);
        }

        [Fact]
        public void PasswordsMatch_False()
        {
            var existingUserPassword = "IAmAnExistingUserPassword";
            var existingUserSalt = "IAmAnExistingUserSalt";
            var existingPasswordDto = new Password(existingUserPassword, existingUserSalt);
            var hashedExistingUserSaltedPassword = sut.HashPassword(existingPasswordDto);

            var loginPassword = "IAmAnExistingUserPasswordThatIsDifferent";
            var loginPasswordDto = new Password(loginPassword, existingUserSalt);
            var hashedLoginUserSaltedPassword = sut.HashPassword(loginPasswordDto);

            var user = new User();
            user.Password = hashedExistingUserSaltedPassword.SaltedHashedPassword;
            user.Salt = existingUserSalt;

            var passwordsMatch = sut.PasswordsMatch(hashedLoginUserSaltedPassword, user);

            Assert.False(passwordsMatch);
        }

        #endregion
        
        #region Salt

        [Theory]
        [InlineData(Constants.SALT_SIZE, false)]
        [InlineData(18, false)]
        [InlineData(64, false)]
        [InlineData(128, false)]
        [InlineData(2, true)]
        public void GetSalt_MultipleTests(int size, bool expectError)
        {
            if (expectError) 
            {
                try 
                {
                    var result = sut.GetSalt(size);

                    throw new Exception("Exception not thrown");
                } 
                catch (Exception ex)
                {
                    Assert.NotNull(ex);
                    Assert.True(ex.Message.Length > 0);
                }
            } 
            else
            {
                var result = sut.GetSalt(size);

                Assert.NotNull(result);
                Assert.True(result.Length > 0);
            }
        }

        #endregion

        #region HashPassword

        [Fact]
        public void HashPassword_HappyPathTest()
        {
            var password = "IAmAPassword";
            var salt = "IAmAComplicatedSaltThatIsAtLeastEightBytesLong";
            var passwordDto = new Password(password, salt);
            var updatedPasswordDto = sut.HashPassword(passwordDto);
            Assert.NotNull(updatedPasswordDto.SaltedHashedPassword);
            Assert.True(updatedPasswordDto.SaltedHashedPassword.Length>0);
        }

        #endregion

        #region Contains a number

        [Fact]
        public void ContainsOneNumber_ValueContainsANumber()
        {
            var password = "IAmAPassword9";
            var passwordContainsANumber = sut.ContainsOneNumber(password);
            Assert.True(passwordContainsANumber);
        }

        [Fact]
        public void ContainsOneNumber_ValueDoesNotContainsANumber()
        {
            var password = "IAmAPassword";
            var passwordContainsANumber = sut.ContainsOneNumber(password);
            Assert.False(passwordContainsANumber);
        }

        #endregion

        #region User Registration

        [Theory]
        [InlineData("userName", "email@email.com", "password123", true, true)]         //correct values
        [InlineData(null, "email@email.com", "password123", false, true)]              //user - null
        [InlineData("", "email@email.com", "password123", false, true)]                //user - empty string
        [InlineData("null", "email@email.com", "password123", false, true)]            //user - "null"
        [InlineData("user", "email@email.com", "pass", false, true)]                   //user- not long enough
        [InlineData("userName", null, "password123", false, true)]                     //email - null
        [InlineData("userName", "", "password123", false, true)]                       //email - empty string
        [InlineData("userName", "null", "password123", false, true)]                   //email - "null"
        [InlineData("userName", "emailnoatsigh", "password123", false, true)]
        [InlineData("userName", "email@email.com", null, false, true)]                 //password - null
        [InlineData("userName", "email@email.com", "", false, true)]                   //password- empty string
        [InlineData("userName", "email@email.com", "null", false, true)]               //password - "null"
        [InlineData("userName", "email@email.com", "password", false, false)]          //password - no number
        [InlineData("userName", "email@email.com", "pass", false, true)]               //password- not long enough
        public void IsValidUserToRegister_MultipleTests(string user, string email,
                                                            string password, bool isValid, bool mock)
        {
            this.mockPassword.Setup(x => x.ContainsOneNumber
                                    (It.Is<string>(s => s == password)))
                                        .Returns(mock);

            var result = sut.IsValidUserToRegister(user, email, password);

            Assert.Equal(isValid, result);
        }

        #endregion

        #region IsValidToken

        [Fact]
        public void IsValidToken_True()
        {
            var jwtToken = GetRealJwtToken();
            var user = GetUser();
            user.Token = jwtToken;

            var isValidToken = sut.IsValidToken(user, jwtToken);

            Assert.True(isValidToken);
        }

        [Fact]
        public void IsValidToken_Expired_False()
        {
            var jwtToken = GetRealJwtToken(-5);
            var user = GetUser();
            user.Token = jwtToken;

            var isValidToken = sut.IsValidToken(user, jwtToken);

            Assert.False(isValidToken);
        }

        [Fact]
        public void IsValidToken_NoToken_False()
        {
            string jwtToken = null;
            var user = GetUser();
            user.Token = jwtToken;

            var isValidToken = sut.IsValidToken(user, jwtToken);

            Assert.False(isValidToken);
        }

        [Fact]
        public void IsValidToken_NoUser_False()
        {
            string jwtToken = null;
            User user = null;

            var isValidToken = sut.IsValidToken(user, jwtToken);

            Assert.False(isValidToken);
        }

        [Fact]
        public void IsValidToken_UserTokenAndTokenDoNotMatch_False()
        {
            var jwtToken = GetRealJwtToken();
            var user = GetUser();

            var isValidToken = sut.IsValidToken(user, jwtToken);

            Assert.False(isValidToken);
        }

        #endregion
    }
}
