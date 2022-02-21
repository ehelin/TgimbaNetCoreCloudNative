using System.Collections.Generic;
using Algorithms.Algorithms.Search;
using Algorithms.Algorithms.Search.Implementations;
using Xunit;
using Shared.dto;

namespace Algorithms_Unit
{
    public class BinarySearchTests
    {
        #region prototype tests

        [Fact]
        public void BinarySearchPrototypeTest()
        {
            var cheatSheet = new Algorithms.Algorithms.AlgorithmCheatSheet(true);
            cheatSheet.RunBinarySearchSentencesSingleSearchTermPrototype();
        }

        #endregion

        #region name tests
      
        [Fact]
        public void BinarySearchNameTest()
        {
            ISearch sut = new BinarySearch();
            var bucketListItems = new List<BucketListItem>();
            var searchTerm = "of";
            var expectedMatchCount = 3;

            bucketListItems.Add(new BucketListItem() { Name = "paris - see nortre dame cathedral" });
            bucketListItems.Add(new BucketListItem() { Name = "get picture on ledge in Trolltunga Norway" });
            bucketListItems.Add(new BucketListItem() { Name = "Plitvice Lakes National Park  Croatia" });
            bucketListItems.Add(new BucketListItem() { Name = "see alex Lang Fingles Cave on the island of Staffa...of the west coast of scotland." });
            bucketListItems.Add(new BucketListItem() { Name = "The Great Pyramid of Cholula" });
            bucketListItems.Add(new BucketListItem() { Name = "visit all big famous cities" });
            bucketListItems.Add(new BucketListItem() { Name = "ireland->aran - chain of islands south part ireland(ring of cari)" });
            bucketListItems.Add(new BucketListItem() { Name = "eat at Jiros sushi bar in tokoyo(needs year reservation)" });

            var searchResults = sut.Search(bucketListItems, searchTerm);

            Assert.NotNull(searchResults);
            Assert.Equal(expectedMatchCount, searchResults.Count);

            var ctr = 0;
            foreach(var bucketListItem in bucketListItems)
            {
                // TODO - stream line these checks
                if (bucketListItem.Name == "see alex Lang Fingles Cave on the island of Staffa...of the west coast of scotland."
                        || bucketListItem.Name == "The Great Pyramid of Cholula"
                            || bucketListItem.Name == "ireland->aran - chain of islands south part ireland(ring of cari)")
                {
                    ctr++;
                }
            }

            Assert.Equal(expectedMatchCount, ctr);
        }

        [Fact]
        public void BinarySearchNameTestTwo()
        {
            ISearch sut = new BinarySearch();
            var bucketListItems = new List<BucketListItem>();
            var searchTerm = "2";
            var expectedMatchCount = 1;

            bucketListItems.Add(new BucketListItem() { Name = "Bucket item test 3" });
            bucketListItems.Add(new BucketListItem() { Name = "Bucket item test 1" });
            bucketListItems.Add(new BucketListItem() { Name = "Bucket item test 7" });
            bucketListItems.Add(new BucketListItem() { Name = "Bucket item test 5" });
            bucketListItems.Add(new BucketListItem() { Name = "Bucket item test 4" });
            bucketListItems.Add(new BucketListItem() { Name = "Bucket item test 2" });
            bucketListItems.Add(new BucketListItem() { Name = "Bucket item test 6" });

            var searchResults = sut.Search(bucketListItems, searchTerm);

            Assert.NotNull(searchResults);
            Assert.Equal(expectedMatchCount, searchResults.Count);
        }

        #endregion
    }
}
