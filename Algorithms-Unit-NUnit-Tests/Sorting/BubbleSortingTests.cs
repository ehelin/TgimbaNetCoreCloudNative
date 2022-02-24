using Algorithms.Algorithms.Sorting.Implementations;
using NUnit.Framework;
using Shared.misc;

namespace Algorithms_Unit
{
    [NonParallelizable]
    public class BubbleSortingTests : BaseSortingTest
    {
        #region name tests

        [Test]
        public void BubbleSortListNameAscTest()
        {
            var sortedValues = RunSortTest(GetNameValues(), Enums.SortColumns.ListItemName, false, new BubbleSort());
            ValidateSortListNameAscTest(GetNameValues(), sortedValues);
        }

        [Test]
        public void BubbleSortListNameDescTest()
        {
            var sortedValues = RunSortTest(GetNameValues(), Enums.SortColumns.ListItemName, true, new BubbleSort());
            ValidateSortListCreatedDescTest(GetNameValues(), sortedValues);
        }

        #endregion

        #region created tests

        [Test]
        public void BubbleSortListCreatedAscTest()
        {
            var sortedValues = RunSortTest(GetCreatedValues(), Enums.SortColumns.Created, false, new BubbleSort());
            ValidateSortListCreatedAscTest(GetCreatedValues(), sortedValues);
        }

        [Test]
        public void BubbleSortListCreatedDescTest()
        {
            var sortedValues = RunSortTest(GetCreatedValues(), Enums.SortColumns.Created, true, new BubbleSort());
            ValidateSortListCreatedDescTest(GetCreatedValues(), sortedValues);
        }

        #endregion

        #region category tests

        [Test]
        public void BubbleSortListCategoryAscTest()
        {
            var sortedValues = RunSortTest(GetCategoryValues(), Enums.SortColumns.Category, false, new BubbleSort());
            ValidateSortListCategoryAscTest(GetCategoryValues(), sortedValues);
        }

        [Test]
        public void BubbleSortListCategoryDescTest()
        {
            var sortedValues = RunSortTest(GetCategoryValues(), Enums.SortColumns.Category, true, new BubbleSort());
            ValidateSortListCategoryDescTest(GetCategoryValues(), sortedValues, false);
        }

        #endregion

        #region acheived tests

        [Test]
        public void BubbleSortListAchievedAscTest()
        {
            var sortedValues = RunSortTest(GetAchievedValues(), Enums.SortColumns.Achieved, false, new BubbleSort());
            ValidateSortAchievedAscTest(sortedValues);
        }

        [Test]
        public void BubbleSortListAchievedDescTest()
        {
            var sortedValues = RunSortTest(GetAchievedValues(), Enums.SortColumns.Achieved, true, new BubbleSort());
            ValidateSortAchievedDescTest(sortedValues);
        }

        #endregion
    }
}
