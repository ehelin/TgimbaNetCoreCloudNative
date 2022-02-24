using Algorithms.Algorithms.Sorting.Implementations;
using NUnit.Framework;
using Shared.misc;

namespace Algorithms_Unit
{
    [NonParallelizable]
    public class LinqSortingTests : BaseSortingTest
    {
        #region name tests

        [Test]
        public void LinqSortListNameAscTest()
        {
            var valuesToBeSorted = GetNameValues();
            var valuesToBeCompared = GetNameValues();

            var sortedValues = RunSortTest(valuesToBeSorted, Enums.SortColumns.ListItemName, false, new LinqSort());
            ValidateSortListNameAscTest(valuesToBeCompared, sortedValues);
        }

        [Test]
        public void LinqSortListNameDescTest()
        {
            var valuesToBeSorted = GetNameValues();
            var valuesToBeCompared = GetNameValues();

            var sortedValues = RunSortTest(valuesToBeSorted, Enums.SortColumns.ListItemName, true, new LinqSort());
            ValidateSortListCreatedDescTest(valuesToBeCompared, sortedValues);
        }

        #endregion

        #region created tests

        [Test]
        public void LinqSortListCreatedAscTest()
        {
            var valuesToBeSorted = GetCreatedValues();
            var valuesToBeCompared = GetCreatedValues();

            var sortedValues = RunSortTest(valuesToBeSorted, Enums.SortColumns.Created, false, new LinqSort());
            ValidateSortListCreatedAscTest(valuesToBeCompared, sortedValues);
        }

        [Test]
        public void LinqSortListCreatedDescTest()
        {
            var valuesToBeSorted = GetCreatedValues();
            var valuesToBeCompared = GetCreatedValues();

            var sortedValues = RunSortTest(valuesToBeSorted, Enums.SortColumns.Created, true, new LinqSort());
            ValidateSortListCreatedDescTest(valuesToBeCompared, sortedValues);
        }

        #endregion

        #region category tests

        [Test]
        public void LinqSortListCategoryAscTest()
        {
            var valuesToBeSorted = GetCategoryValues();
            var valuesToBeCompared = GetCategoryValues();

            var sortedValues = RunSortTest(valuesToBeSorted, Enums.SortColumns.Category, false, new LinqSort());
            ValidateSortListCategoryAscTest(valuesToBeCompared, sortedValues);
        }

        [Test]
        public void LinqSortListCategoryDescTest()
        {
            var valuesToBeSorted = GetCategoryValues();
            var valuesToBeCompared = GetCategoryValues();

            var sortedValues = RunSortTest(valuesToBeSorted, Enums.SortColumns.Category, true, new LinqSort());
            ValidateSortListCategoryDescTest(valuesToBeCompared, sortedValues, true);
        }

        #endregion

        #region acheived tests

        [Test]
        public void LinqSortListAchievedAscTest()
        {
            var sortedValues = RunSortTest(GetAchievedValues(), Enums.SortColumns.Achieved, false, new LinqSort());
            ValidateSortAchievedAscTest(sortedValues);
        }

        [Test]
        public void LinqSortListAchievedDescTest()
        {
            var sortedValues = RunSortTest(GetAchievedValues(), Enums.SortColumns.Achieved, true, new LinqSort());
            ValidateSortAchievedDescTest(sortedValues);
        }

        #endregion
    }
}
