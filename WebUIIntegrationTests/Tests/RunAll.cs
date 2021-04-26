 using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.misc.testUtilities;

namespace TgimbaSeleniumTests.Tests
{
    [TestClass]
    public class RunAll
    {
        [TestCleanup]
        public void Cleanup()
        {
            TestUtilities.ClearEnvironmentalVariablesForIntegrationTests();
        }

        [TestInitialize]
        public void SetUp()
        {
            TestUtilities.SetEnvironmentalVariablesForIntegrationTests();
        }

        [TestMethod]
        public void RunAllLocalTests()
        {				  
			foreach (string url in Utilities.GetUrls())
			{
				CleanUpLocal(url);
				RunAllTestsLocalDesktop(url);
			}
		}
        
        public void CleanUpLocal(string host, bool onlyDeleteBucketListItems = false)
        {
            var utilities = new Shared.misc.testUtilities.TestUtilities();
            utilities.CleanUpLocal(host, onlyDeleteBucketListItems);
        }
        
        private void RunAllTestsLocalDesktop(string url)
        {
			Chrome.Chrome chromeDesk = new Chrome.Chrome(url);
			chromeDesk.TestHappyPathChrome(url);

			//Firefox.Firefox firefoxDesk = new Firefox.Firefox(url);
            //firefoxDesk.TestHappyPathFireFox();
        }    
    }
}
