using System;
using Xunit;
using Shared.misc;
using SharedWeb = TgimbaNetCoreWebShared;

namespace TestTgimbaNetCoreWeb
{
    public class UtilitiesTests : BaseTest
    {
        [Fact]
        public void Test_IsMobileTrue()
        {
            Assert.True(SharedWeb.Utilities.IsMobile("Mozilla/5.0 (Linux; U; Android 4.4.2; en-us; SCH-I535 Build/KOT49H) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30"));
        }

        [Fact]
        public void Test_IsMobileFalse()
        {
            Assert.False(SharedWeb.Utilities.IsMobile("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.246"));
        }

        [Fact]
        public void Test_GetAvailableSortingAlgorithms()
        {
            var availableSortingAlgorithms = SharedWeb.Utilities.GetAvailableSortingAlgorithms();
            var expectedEnumValues = Enum.GetNames(typeof(Enums.SortAlgorithms));

            Assert.Equal(expectedEnumValues.Length, availableSortingAlgorithms.Length);
            foreach(var availableSortingAlgorithm in availableSortingAlgorithms)
            {
                Assert.True(Array.IndexOf(expectedEnumValues, availableSortingAlgorithm) != -1);
            }
         }

        [Fact]
        public void Test_GetAvailableSearchingAlgorithms()
        {
            var availableSearchingAlgorithms = SharedWeb.Utilities.GetAvailableSearchingAlgorithms();
            var expectedEnumValues = Enum.GetNames(typeof(Enums.SearchAlgorithms));

            Assert.Equal(expectedEnumValues.Length, availableSearchingAlgorithms.Length);
            foreach (var availableSearchingAlgorithm in availableSearchingAlgorithms)
            {
                Assert.True(Array.IndexOf(expectedEnumValues, availableSearchingAlgorithm) != -1);
            }
        }

        [Fact]
        public void Test_ConvertModelToStringArray()
        {
            var bucketListItemString = GetBucketListItemSingleString("base64EncodedGoodUser", "testBucketLIstItem", null, true);
            var bucketListItemModel = GetBucketListItemModel("base64EncodedGoodUser", "testBucketLIstItem", null, true);

            var convertedBucketListItem = SharedWeb.Utilities.ConvertModelToString(bucketListItemModel);

            Assert.Equal(bucketListItemString, convertedBucketListItem);
        }

        [Fact]
        public void Test_ConvertCategoryHotToEnum()
        {
            var enumValue = SharedWeb.Utilities.ConvertCategoryToEnum("Hot");

            Assert.Equal(Enums.BucketListItemTypes.Hot, enumValue);
        }

        [Fact]
        public void Test_ConvertCategoryWarmToEnum()
        {
            var enumValue = SharedWeb.Utilities.ConvertCategoryToEnum("Warm");

            Assert.Equal(Enums.BucketListItemTypes.Warm, enumValue);
        }

        [Fact]
        public void Test_ConvertCategoryColdToEnum()
        {
            var enumValue = SharedWeb.Utilities.ConvertCategoryToEnum("Cool");

            Assert.Equal(Enums.BucketListItemTypes.Cool, enumValue);
        }

        [Fact]
        public void Test_ConvertCategoryBadInputToEnumThrowsException()
        {
            try
            {
                var enumValue = SharedWeb.Utilities.ConvertCategoryToEnum("UnrecognizedCategory");
                Assert.Equal(1, 2);    // Fail if this line executes
            }
            catch (Exception e)
            {
                Assert.NotNull(e);
                Assert.Equal("Unknown category: UnrecognizedCategory", e.Message);
            }
        }
    }
}
