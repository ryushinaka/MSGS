using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniScript.MSGS.Data.Numerics
{
    using System;
    using System.Collections.Generic;

    public static class BlockSortList
    {
        private static int runLength = 32;  // Optimal size of a block

        // Insertion sort function
        static void InsertionSort(ref List<double> arr, int left, int right)
        {
            for (int i = left + 1; i <= right; i++)
            {
                double temp = arr[i];
                int j = i - 1;
                while (j >= left && arr[j] > temp)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = temp;
            }
        }

        // Merge function
        static void Merge(ref List<double> arr, int l, int m, int r)
        {
            // Original array is broken in two parts: left and right array
            int len1 = m - l + 1, len2 = r - m;
            List<double> left = new List<double>();
            List<double> right = new List<double>();

            for (int x = 0; x < len1; x++)
                left.Add(arr[l + x]);
            for (int x = 0; x < len2; x++)
                right.Add(arr[m + 1 + x]);

            int i = 0;
            int j = 0;
            int k = l;

            // After comparing, we merge those two arrays in larger sub array
            while (i < len1 && j < len2)
            {
                if (left[i] <= right[j])
                {
                    arr[k] = left[i];
                    i++;
                }
                else
                {
                    arr[k] = right[j];
                    j++;
                }
                k++;
            }

            // Copy remaining elements of left, if any
            while (i < len1)
            {
                arr[k] = left[i];
                i++;
                k++;
            }

            // Copy remaining element of right, if any
            while (j < len2)
            {
                arr[k] = right[j];
                j++;
                k++;
            }
        }

        // Function to sort the array using block sort
        public static void BlockSort(this List<double> arr)
        {
            int n = arr.Count;

            // Sort individual subarrays of size runLength
            for (int i = 0; i < n; i += runLength)
            {
                InsertionSort(ref arr, i, Math.Min((i + 31), (n - 1)));
            }

            // Start merging from size runLength
            for (int size = runLength; size < n; size = 2 * size)
            {
                for (int left = 0; left < n; left += 2 * size)
                {
                    int mid = left + size - 1;
                    int right = Math.Min((left + 2 * size - 1), (n - 1));

                    if (mid < right)
                        Merge(ref arr, left, mid, right);
                }
            }
        }
    }
}