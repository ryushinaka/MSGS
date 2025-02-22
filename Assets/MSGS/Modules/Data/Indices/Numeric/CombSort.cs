using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniScript.MSGS.Data.Numerics
{
    public static class CombSortExtension
    {
        //for reference:
        //https://en.wikipedia.org/wiki/Sorting_algorithm

        public static void CombSort(this List<double> lst)
        {
            int n = lst.Count;
            int gap = n;
            bool swapped = true;
            const double shrinkFactor = 1.3; // Shrink factor for the gap

            while (gap > 1 || swapped)
            {
                // Update the gap value for the next comb
                gap = (int)(gap / shrinkFactor);
                if (gap < 1)
                    gap = 1;

                swapped = false;

                // Compare elements at the current gap
                for (int i = 0; i < n - gap; i++)
                {
                    if (lst[i] > lst[i + gap])
                    {
                        // Swap if the elements are in the wrong order
                        Swap(ref lst, i, i + gap);
                        swapped = true;
                    }
                }
            }
        }

        private static void Swap(ref List<double> numbers, int i, int j)
        {
            double temp = numbers[i];
            numbers[i] = numbers[j];
            numbers[j] = temp;
        }
    }
}
