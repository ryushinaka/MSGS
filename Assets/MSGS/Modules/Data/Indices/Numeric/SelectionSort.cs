using System;
using System.Collections.Generic;

namespace MiniScript.MSGS.Data.Numerics
{
    public static class SelectionSortExtension
    {
        //for reference:
        //https://en.wikipedia.org/wiki/Sorting_algorithm

        public static void SelectionSort(this List<double> lst)
        {
            int n = lst.Count;

            // Move through the list
            for (int i = 0; i < n - 1; i++)
            {
                // Find the index of the minimum element in the unsorted part
                int minIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (lst[j] < lst[minIndex])
                    {
                        minIndex = j; // Update the index of the minimum element
                    }
                }

                // Swap the found minimum element with the first element of the unsorted part
                if (minIndex != i)
                {
                    Swap(ref lst, i, minIndex);
                }
            }
        }

        private static void Swap(ref List<double> lst, int i, int j)
        {
            double temp = lst[i];
            lst[i] = lst[j];
            lst[j] = temp;
        }
    }

}