using Algorithms.Algorithms.Sorting.Implementations;
using NUnit.Framework;
using Shared.misc;

namespace Algorithms_Unit
{
    [NonParallelizable]
    public class InsertionSortTests : BaseSortingTest
    {
        #region name tests

        [Test]
        public void InsertionSortListNameAscTest()
        {
            var sortedValues = RunSortTest(GetNameValues(), Enums.SortColumns.ListItemName, false, new InsertionSort());
            ValidateSortListNameAscTest(GetNameValues(), sortedValues);
        }

        [Test]
        public void InsertionSortListNameDescTest()
        {
            var sortedValues = RunSortTest(GetNameValues(), Enums.SortColumns.ListItemName, true, new InsertionSort());
            ValidateSortListCreatedDescTest(GetNameValues(), sortedValues);
        }

        #endregion

        #region created tests

        [Test]
        public void InsertionSortListCreatedAscTest()
        {
            var sortedValues = RunSortTest(GetCreatedValues(), Enums.SortColumns.Created, false, new InsertionSort());
            ValidateSortListCreatedAscTest(GetCreatedValues(), sortedValues);
        }

        [Test]
        public void InsertionSortListCreatedDescTest()
        {
            var sortedValues = RunSortTest(GetCreatedValues(), Enums.SortColumns.Created, true, new InsertionSort());
            ValidateSortListCreatedDescTest(GetCreatedValues(), sortedValues);
        }

        #endregion

        #region category tests

        [Test]
        public void InsertionSortListCategoryAscTest()
        {
            var sortedValues = RunSortTest(GetCategoryValues(), Enums.SortColumns.Category, false, new InsertionSort());
            ValidateSortListCategoryAscTest(GetCategoryValues(), sortedValues);
        }

        [Test]
        public void InsertionSortListCategoryDescTest()
        {
            var sortedValues = RunSortTest(GetCategoryValues(), Enums.SortColumns.Category, true, new InsertionSort());
            ValidateSortListCategoryDescTest(GetCategoryValues(), sortedValues, false);
        }

        #endregion

        #region acheived tests

        [Test]
        public void InsertionSortListAchievedAscTest()
        {
            var sortedValues = RunSortTest(GetAchievedValues(), Enums.SortColumns.Achieved, false, new InsertionSort());
            ValidateSortAchievedAscTest(sortedValues);
        }

        [Test]
        public void InsertionSortListAchievedDescTest()
        {
            var sortedValues = RunSortTest(GetAchievedValues(), Enums.SortColumns.Achieved, true, new InsertionSort());
            ValidateSortAchievedDescTest(sortedValues);
        }

        #endregion
    }
}
