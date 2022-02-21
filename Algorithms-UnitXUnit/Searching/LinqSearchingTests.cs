using System.Collections.Generic;
using Algorithms.Algorithms.Search;
using Algorithms.Algorithms.Search.Implementations;
using Xunit;
using Shared.dto;

namespace Algorithms_Unit
{
    public class LinqSearchingTests
    {
        #region name tests

        [Fact]
        public void BubbleSortListNameAscTest()
        {
            ISearch sut = new LinqSearch();
            var values = new List<BucketListItem>();

            values.Add(new BucketListItem() { Name = "ZBucketListItem" });
            values.Add(new BucketListItem() { Name = "yBucketListItem" });
            values.Add(new BucketListItem() { Name = "ABucketListItem" });
            values.Add(new BucketListItem() { Name = "tBucketListItem" });

            var searchResults = sut.Search(values, "yBucketListItem");

            Assert.NotNull(searchResults);
            Assert.Single(searchResults);
            Assert.Equal("yBucketListItem", searchResults[0].Name);
        }

        #endregion
    }
}
