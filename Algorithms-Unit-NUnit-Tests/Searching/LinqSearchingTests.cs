using System.Collections.Generic;
using Algorithms.Algorithms.Search;
using Algorithms.Algorithms.Search.Implementations;
using NUnit.Framework;
using Shared.dto;

namespace Algorithms_Unit
{
    [NonParallelizable]
    public class LinqSearchingTests
    {
        #region name tests

        [Test]
        public void BubbleSortListNameAscTest()
        {
            ISearch sut = new LinqSearch();
            var values = new List<BucketListItem>();

            values.Add(new BucketListItem() { Name = "ZBucketListItem" });
            values.Add(new BucketListItem() { Name = "yBucketListItem" });
            values.Add(new BucketListItem() { Name = "ABucketListItem" });
            values.Add(new BucketListItem() { Name = "tBucketListItem" });

            var searchResults = sut.Search(values, "yBucketListItem");

            Assert.IsNotNull(searchResults);
            Assert.AreEqual(1, searchResults.Count);
            Assert.AreEqual("yBucketListItem", searchResults[0].Name);
        }

        #endregion
    }
}
