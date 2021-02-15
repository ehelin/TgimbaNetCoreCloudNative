﻿using System.Collections.Generic;
using Shared.dto;
using Shared.misc;

namespace Algorithms.Algorithms.Sorting.Implementations
{
    public class BubbleSort : BaseSort, ISort
    {
        public Enums.SortAlgorithms GetSortingAlgorithm()
        {
            return Enums.SortAlgorithms.Bubble;
        }

        public IList<BucketListItem> Sort(IList<BucketListItem> values, Enums.SortColumns sortColumn, bool desc)
        {
            for(int outer=0; outer<values.Count; outer++)
            {
                if (outer > values.Count + 1) { break; }

                for (int inner=0; inner<values.Count-1; inner++)
                {
                    long innerCompare = 0;
                    long innerPlusOneCompare = 0;

                    if (sortColumn == Enums.SortColumns.ListItemName) { SortName(inner, inner+1, values, out innerCompare, out innerPlusOneCompare); }
                    if (sortColumn == Enums.SortColumns.Created) { SortCreated(inner, inner + 1, values, out innerCompare, out innerPlusOneCompare); }
                    if (sortColumn == Enums.SortColumns.Category) { SortCategory(inner, inner + 1, values, out innerCompare, out innerPlusOneCompare); }
                    if (sortColumn == Enums.SortColumns.Achieved) { SortAchieved(inner, inner + 1, values, out innerCompare, out innerPlusOneCompare); }

                    if (ifSwapValues(desc, innerCompare, innerPlusOneCompare))
                    {
                        var tmp = values[inner];
                        values[inner] = values[inner + 1];
                        values[inner + 1] = tmp;
                    }
                }
            }

            return values;
        }

        private bool ifSwapValues(bool desc, long innerCompare, long innerPlusOneCompare)
        {
            bool swap = !desc && innerCompare > innerPlusOneCompare;

            if (!swap)
            {
                swap = desc && innerCompare < innerPlusOneCompare;
            }

            return swap;
        }
    }
}
