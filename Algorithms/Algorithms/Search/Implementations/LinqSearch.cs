using System.Collections.Generic;
using System.Linq;
using Shared.dto;
using Shared.misc;

namespace Algorithms.Algorithms.Search.Implementations
{
    public class LinqSearch : ISearch
    {
        public Enums.SearchAlgorithms GetSearchingAlgorithm()
        {
            return Enums.SearchAlgorithms.Linq;
        }

        public IList<BucketListItem> Search(IList<BucketListItem> bucketListItems, string srchTerm)
        {
            var searchedBucketListItems = bucketListItems.Where(x => x.Name.Contains(srchTerm));

            return searchedBucketListItems.ToList();
        }
    }
}
