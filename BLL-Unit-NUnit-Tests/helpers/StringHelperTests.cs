using System;
using BLLNetCore.helpers;
using NUnit.Framework;
using Shared.interfaces;
using Shared.misc;

namespace TestAPINetCore_Unit.helpers
{
    [NonParallelizable]
    public class StringHelperTests : BaseTest
    {
        private IString sut = null;
        private string unencodedBase64String = "IAmAnUnBased64String";
        private string encodedBase64String = "SUFtQW5VbkJhc2VkNjRTdHJpbmc=";

        public StringHelperTests() {
            sut = new StringHelper();
        }

        #region DecodeBase64String

        [Test]
        public void DecodeBase64String_HappyPathTest()
        {
            var result = sut.DecodeBase64String(encodedBase64String);
            Assert.IsNotNull(result);
            Assert.AreEqual(unencodedBase64String, result);
        }

        [Test]
        public void DecodeBase64String_NullValue()
        {
            var result = sut.DecodeBase64String(null);
            Assert.IsNotNull(result);
            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void DecodeBase64String_EmptyValue()
        {
            var result = sut.DecodeBase64String("");
            Assert.IsNotNull(result);
            Assert.AreEqual(string.Empty, result);
        }

        #endregion

        #region EncodeBase64String

        [Test]
        public void EncodeBase64String_HappyPathTest()
        {
            var result = sut.EncodeBase64String(unencodedBase64String);
            Assert.IsNotNull(result);
            Assert.AreEqual(encodedBase64String, result);
        }

        [Test]
        public void EncodeBase64String_NullValue()
        {
            var result = sut.EncodeBase64String(null);
            Assert.IsNotNull(result);
            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void EncodeBase64String_EmptyValue()
        {
            var result = sut.EncodeBase64String("");
            Assert.IsNotNull(result);
            Assert.AreEqual(string.Empty, result);
        }

        #endregion

        #region HasSortOrderAsc

        [TestCase("asc", true)]
        [TestCase("Asc", true)]
        [TestCase("ASC", true)]
        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase("desc", false)]
        [TestCase("Desc", false)]
        [TestCase("DESC", false)]
        public void HasSortOrderAsc_Tests(string sortString, bool isAsc)
        {
            var result = sut.HasSortOrderAsc(sortString);
            Assert.AreEqual(isAsc, result);
        }

        #endregion

        #region GetLowerCaseSortString

        [TestCase("asc", "asc")]
        [TestCase("Asc", "asc")]
        [TestCase("ASC", "asc")]
        [TestCase(null, "")]
        [TestCase("", "")]
        [TestCase("desc", "desc")]
        [TestCase("Desc", "desc")]
        [TestCase("DESC", "desc")]
        public void GetLowerCaseSortString_Tests(string sortString, string expectedResult)
        {
            var result = sut.GetLowerCaseSortString(sortString);
            Assert.AreEqual(expectedResult, result);
        }

        #endregion

        #region GetSortColumn

        [TestCase("LISTITEMNAME", Enums.SortColumns.ListItemName)]
        [TestCase("ListItemName", Enums.SortColumns.ListItemName)]
        [TestCase("listitemname", Enums.SortColumns.ListItemName)]
        [TestCase("CREATED", Enums.SortColumns.Created)]
        [TestCase("Created", Enums.SortColumns.Created)]
        [TestCase("created", Enums.SortColumns.Created)]
        [TestCase("CATEGORY", Enums.SortColumns.Category)]
        [TestCase("Category", Enums.SortColumns.Category)]
        [TestCase("category", Enums.SortColumns.Category)]
        [TestCase("ACHIEVED", Enums.SortColumns.Achieved)]
        [TestCase("Achieved", Enums.SortColumns.Achieved)]
        [TestCase("achieved", Enums.SortColumns.Achieved)]
        [TestCase(null, null)]
        [TestCase("IAmAnUnknownSortString", null)]
        public void GetSortColumn_Tests(string sortString, Enums.SortColumns? expectedResult)
        {
            try 
            {
                var result = sut.GetSortColumn(sortString);
                Assert.AreEqual(expectedResult.Value, result);
            } 
            catch (Exception ex)
            {
                if (expectedResult != null || !ex.Message.Contains("Unknown sort string"))
                {
                    Assert.IsTrue(false); //expected result to be null and an error to be thrown
                }
            }
        }

        #endregion
    }
}
