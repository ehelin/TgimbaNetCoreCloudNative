using System;
using BLLNetCore.helpers;
using Xunit;
using Shared.interfaces;
using Shared.misc;

namespace TestAPINetCore_Unit.helpers
{
    public class StringHelperTests_XUnit : BaseTest
    {
        private IString sut = null;
        private string unencodedBase64String = "IAmAnUnBased64String";
        private string encodedBase64String = "SUFtQW5VbkJhc2VkNjRTdHJpbmc=";

        public StringHelperTests_XUnit() {
            sut = new StringHelper();
        }

        #region DecodeBase64String

        [Fact]
        public void DecodeBase64String_HappyPathTest()
        {
            var result = sut.DecodeBase64String(encodedBase64String);
            Assert.NotNull(result);
            Assert.Equal(unencodedBase64String, result);
        }

        [Fact]
        public void DecodeBase64String_NullValue()
        {
            var result = sut.DecodeBase64String(null);
            Assert.NotNull(result);
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void DecodeBase64String_EmptyValue()
        {
            var result = sut.DecodeBase64String("");
            Assert.NotNull(result);
            Assert.Equal(string.Empty, result);
        }

        #endregion

        #region EncodeBase64String

        [Fact]
        public void EncodeBase64String_HappyPathTest()
        {
            var result = sut.EncodeBase64String(unencodedBase64String);
            Assert.NotNull(result);
            Assert.Equal(encodedBase64String, result);
        }

        [Fact]
        public void EncodeBase64String_NullValue()
        {
            var result = sut.EncodeBase64String(null);
            Assert.NotNull(result);
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void EncodeBase64String_EmptyValue()
        {
            var result = sut.EncodeBase64String("");
            Assert.NotNull(result);
            Assert.Equal(string.Empty, result);
        }

        #endregion

        #region HasSortOrderAsc

        [Theory]
        [InlineData("asc", true)]
        [InlineData("Asc", true)]
        [InlineData("ASC", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("desc", false)]
        [InlineData("Desc", false)]
        [InlineData("DESC", false)]
        public void HasSortOrderAsc_Tests(string sortString, bool isAsc)
        {
            var result = sut.HasSortOrderAsc(sortString);
            Assert.Equal(isAsc, result);
        }

        #endregion

        #region GetLowerCaseSortString

        [Theory]
        [InlineData("asc", "asc")]
        [InlineData("Asc", "asc")]
        [InlineData("ASC", "asc")]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData("desc", "desc")]
        [InlineData("Desc", "desc")]
        [InlineData("DESC", "desc")]
        public void GetLowerCaseSortString_Tests(string sortString, string expectedResult)
        {
            var result = sut.GetLowerCaseSortString(sortString);
            Assert.Equal(expectedResult, result);
        }

        #endregion

        #region GetSortColumn

        [Theory]
        [InlineData("LISTITEMNAME", Enums.SortColumns.ListItemName)]
        [InlineData("ListItemName", Enums.SortColumns.ListItemName)]
        [InlineData("listitemname", Enums.SortColumns.ListItemName)]
        [InlineData("CREATED", Enums.SortColumns.Created)]
        [InlineData("Created", Enums.SortColumns.Created)]
        [InlineData("created", Enums.SortColumns.Created)]
        [InlineData("CATEGORY", Enums.SortColumns.Category)]
        [InlineData("Category", Enums.SortColumns.Category)]
        [InlineData("category", Enums.SortColumns.Category)]
        [InlineData("ACHIEVED", Enums.SortColumns.Achieved)]
        [InlineData("Achieved", Enums.SortColumns.Achieved)]
        [InlineData("achieved", Enums.SortColumns.Achieved)]
        [InlineData(null, null)]
        [InlineData("IAmAnUnknownSortString", null)]
        public void GetSortColumn_Tests(string sortString, Enums.SortColumns? expectedResult)
        {
            try 
            {
                var result = sut.GetSortColumn(sortString);
                Assert.Equal(expectedResult.Value, result);
            } 
            catch (Exception ex)
            {
                if (expectedResult != null || !ex.Message.Contains("Unknown sort string"))
                {
                    Assert.True(false); //expected result to be null and an error to be thrown
                }
            }
        }

        #endregion
    }
}
