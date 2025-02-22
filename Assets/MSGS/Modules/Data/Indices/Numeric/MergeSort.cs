using System;
using System.Collections.Generic;

namespace MiniScript.MSGS.Data.Numerics
{
    //for reference:
    //https://en.wikipedia.org/wiki/Sorting_algorithm

    public static class MergeSortAlgorithm
    {
        public static void MergeSort(this List<double> list)
        {
            if (list.Count <= 1)
                return;

            int mid = list.Count / 2;
            List<double> left = list.GetRange(0, mid);
            List<double> right = list.GetRange(mid, list.Count - mid);

            MergeSort(left);
            MergeSort(right);
            Merge(ref list, left, right);
        }

        private static void Merge(ref List<double> list, List<double> left, List<double> right)
        {
            int i = 0, j = 0, k = 0;

            // Merge the left and right lists into the original list
            while (i < left.Count && j < right.Count)
            {
                if (left[i] <= right[j])
                {
                    list[k] = left[i];
                    i++;
                }
                else
                {
                    list[k] = right[j];
                    j++;
                }
                k++;
            }

            // Copy remaining elements of left (if any)
            while (i < left.Count)
            {
                list[k] = left[i];
                i++;
                k++;
            }

            // Copy remaining elements of right (if any)
            while (j < right.Count)
            {
                list[k] = right[j];
                j++;
                k++;
            }
        }
    }
}
