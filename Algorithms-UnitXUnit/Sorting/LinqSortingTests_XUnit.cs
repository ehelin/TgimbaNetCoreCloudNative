using Algorithms.Algorithms.Sorting.Implementations;
using Xunit;
using Shared.misc;

namespace Algorithms_Unit
{
    public class LinqSortingTests_XUnit : BaseSortingTest
    {
        #region name tests

        [Fact]
        public void LinqSortListNameAscTest()
        {
            var valuesToBeSorted = GetNameValues();
            var valuesToBeCompared = GetNameValues();

            var sortedValues = RunSortTest(valuesToBeSorted, Enums.SortColumns.ListItemName, false, new LinqSort());
            ValidateSortListNameAscTest(valuesToBeCompared, sortedValues);
        }

        [Fact]
        public void LinqSortListNameDescTest()
        {
            var valuesToBeSorted = GetNameValues();
            var valuesToBeCompared = GetNameValues();

            var sortedValues = RunSortTest(valuesToBeSorted, Enums.SortColumns.ListItemName, true, new LinqSort());
            ValidateSortListCreatedDescTest(valuesToBeCompared, sortedValues);
        }

        #endregion

        #region created tests

        [Fact]
        public void LinqSortListCreatedAscTest()
        {
            var valuesToBeSorted = GetCreatedValues();
            var valuesToBeCompared = GetCreatedValues();

            var sortedValues = RunSortTest(valuesToBeSorted, Enums.SortColumns.Created, false, new LinqSort());
            ValidateSortListCreatedAscTest(valuesToBeCompared, sortedValues);
        }

        [Fact]
        public void LinqSortListCreatedDescTest()
        {
            var valuesToBeSorted = GetCreatedValues();
            var valuesToBeCompared = GetCreatedValues();

            var sortedValues = RunSortTest(valuesToBeSorted, Enums.SortColumns.Created, true, new LinqSort());
            ValidateSortListCreatedDescTest(valuesToBeCompared, sortedValues);
        }

        #endregion

        #region category tests

        [Fact]
        public void LinqSortListCategoryAscTest()
        {
            var valuesToBeSorted = GetCategoryValues();
            var valuesToBeCompared = GetCategoryValues();

            var sortedValues = RunSortTest(valuesToBeSorted, Enums.SortColumns.Category, false, new LinqSort());
            ValidateSortListCategoryAscTest(valuesToBeCompared, sortedValues);
        }

        [Fact]
        public void LinqSortListCategoryDescTest()
        {
            var valuesToBeSorted = GetCategoryValues();
            var valuesToBeCompared = GetCategoryValues();

            var sortedValues = RunSortTest(valuesToBeSorted, Enums.SortColumns.Category, true, new LinqSort());
            ValidateSortListCategoryDescTest(valuesToBeCompared, sortedValues, true);
        }

        #endregion

        #region acheived tests

        [Fact]
        public void LinqSortListAchievedAscTest()
        {
            var sortedValues = RunSortTest(GetAchievedValues(), Enums.SortColumns.Achieved, false, new LinqSort());
            ValidateSortAchievedAscTest(sortedValues);
        }

        [Fact]
        public void LinqSortListAchievedDescTest()
        {
            var sortedValues = RunSortTest(GetAchievedValues(), Enums.SortColumns.Achieved, true, new LinqSort());
            ValidateSortAchievedDescTest(sortedValues);
        }

        #endregion
    }
}
