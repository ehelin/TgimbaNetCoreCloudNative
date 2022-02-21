using BLLNetCore.Security;
using NUnit.Framework;
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
        }

        [TearDown]
        public void Cleanup()
        {
            TestUtilities.ClearEnvironmentalVariablesForUnitTests();
        }

        [SetUp]
        public void SetUp()
        {
            TestUtilities.SetEnvironmentalVariablesForUnitTests();
        }

        [Test]
        public void DecryptJwtToken_HappyPathTest()
        {
            var jwtToken = GetRealJwtToken();
            var decodedJwtToken = sut.DecryptJwtToken(jwtToken);

            Assert.IsNotNull(decodedJwtToken);
            Assert.IsTrue(decodedJwtToken.Issuer == EnvironmentalConfig.GetJwtIssuer());
        }

        #region JWT 

        [Test]
        public void GetJwtPrivateKey_HappyPathTest()
        {
            var jwtPrivateKey = sut.GetJwtPrivateKey();
            Assert.IsNotNull(jwtPrivateKey);
            Assert.IsTrue(jwtPrivateKey.Length > 0);
        }

        [Test]
        public void GetJwtIssuer_HappyPathTest()
        {
            var jwtIssuer = sut.GetJwtIssuer();
            Assert.IsNotNull(jwtIssuer);
            Assert.IsTrue(jwtIssuer.Length > 0);
        }
               
        [Test]
        public void GetJwtToken_HappyPathTest()
        {
            var jwtPrivateKey = "IAmAJwtPrivateKey";
            var jwtIssuer = "IAmAJwtIssuer";
            var jwtToken = sut.GetJwtToken(jwtPrivateKey, jwtIssuer);
            Assert.IsNotNull(jwtToken);
            Assert.IsTrue(jwtToken.Length > 0);
        }

        #endregion
    }
}
