using System;
using System.Collections.Generic;

namespace MiniScript.MSGS.Data.Numerics
{
    public static class GnomeSortExtension
    {
        //for reference:
        //https://en.wikipedia.org/wiki/Sorting_algorithm

        public static void GnomeSort(this List<double> lst)
        {
            int index = 0;
            int n = lst.Count;

            while (index < n)
            {
                // If the current element is the first element or the previous element is less than or equal to the current element
                if (index == 0 || lst[index - 1] <= lst[index])
                {
                    index++; // Move to the next element
                }
                else
                {
                    // Swap the elements if they are out of order
                    Swap(ref lst, index, index - 1);
                    index--; // Move back to the previous element
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