using BLLNetCore.Security;
using Xunit;
using Shared.interfaces;
using Shared.misc;
using Shared.misc.testUtilities;

namespace TestAPINetCore_Unit.helpers
{
    public class GeneratorHelperTests : BaseTest
    {
        private IGenerator sut = null;
        
        public GeneratorHelperTests()
        {
            sut = new GeneratorHelper();
            TestUtilities.SetEnvironmentalVariablesForUnitTests();
            //TestUtilities.ClearEnvironmentalVariablesForUnitTests();
        }

        [Fact]
        public void DecryptJwtToken_HappyPathTest()
        {
            var jwtToken = GetRealJwtToken();
            var decodedJwtToken = sut.DecryptJwtToken(jwtToken);

            Assert.NotNull(decodedJwtToken);
            Assert.True(decodedJwtToken.Issuer == EnvironmentalConfig.GetJwtIssuer());
        }

        #region JWT 

        [Fact]
        public void GetJwtPrivateKey_HappyPathTest()
        {
            var jwtPrivateKey = sut.GetJwtPrivateKey();
            Assert.NotNull(jwtPrivateKey);
            Assert.True(jwtPrivateKey.Length > 0);
        }

        [Fact]
        public void GetJwtIssuer_HappyPathTest()
        {
            var jwtIssuer = sut.GetJwtIssuer();
            Assert.NotNull(jwtIssuer);
            Assert.True(jwtIssuer.Length > 0);
        }
               
        [Fact]
        public void GetJwtToken_HappyPathTest()
        {
            var jwtPrivateKey = "IAmAJwtPrivateKey";
            var jwtIssuer = "IAmAJwtIssuer";
            var jwtToken = sut.GetJwtToken(jwtPrivateKey, jwtIssuer);
            Assert.NotNull(jwtToken);
            Assert.True(jwtToken.Length > 0);
        }

        #endregion
    }
}
