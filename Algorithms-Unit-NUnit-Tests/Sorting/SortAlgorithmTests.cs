using Algorithms.Algorithms.Sorting.Implementations;
using NUnit.Framework;
using Shared.misc;
using Algorithms.Algorithms.Sorting;

namespace Algorithms_Unit
{
    public class SortAlgorithmTests : BaseSortingTest
    {
        [Test]
        public void SortAlgorithmGetAlgorithmLinqTest()
        {
            var sortingAlgorithms = GetSortingAlgorithms();
            var sut = new AvailableSortingAlgorithms(sortingAlgorithms);

            ISort sortingAlgorithm = sut.GetAlgorithm(Enums.SortAlgorithms.Linq);

            Assert.IsNotNull(sortingAlgorithm);
            Assert.AreEqual(Enums.SortAlgorithms.Linq, sortingAlgorithm.GetSortingAlgorithm());
        }

        [Test]
        public void SortAlgorithmGetAlgorithmBubbleTest()
        {
            var sortingAlgorithms = GetSortingAlgorithms();
            var sut = new AvailableSortingAlgorithms(sortingAlgorithms);

            ISort sortingAlgorithm = sut.GetAlgorithm(Enums.SortAlgorithms.Bubble);

            Assert.IsNotNull(sortingAlgorithm);
            Assert.AreEqual(Enums.SortAlgorithms.Bubble, sortingAlgorithm.GetSortingAlgorithm());
        }
    }
}
