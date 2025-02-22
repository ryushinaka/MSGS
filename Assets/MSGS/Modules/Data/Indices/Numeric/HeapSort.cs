using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniScript.MSGS.Data.Numerics
{
    //for reference:
    //https://en.wikipedia.org/wiki/Sorting_algorithm

    public static class HeapSortedList
    {
        public static void HeapSort(this List<double> lst)
        {
            int n = lst.Count;
            // Build heap (rearrange array)
            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(ref lst, n, i);

            // One by one extract an element from heap
            for (int i = n - 1; i > 0; i--)
            {
                // Move current root to end
                double temp = lst[0];
                lst[0] = lst[i];
                lst[i] = temp;

                // Call max heapify on the reduced heap
                Heapify(ref lst, i, 0);
            }
        }

        // To heapify a subtree rooted with node i which is an index in data[]
        private static void Heapify(ref List<double> lst, int n, int i)
        {
            int largest = i; // Initialize largest as root
            int left = 2 * i + 1; // left = 2*i + 1
            int right = 2 * i + 2; // right = 2*i + 2

            // If left child is larger than root
            if (left < n && lst[left] > lst[largest])
                largest = left;

            // If right child is larger than largest so far
            if (right < n && lst[right] > lst[largest])
                largest = right;

            // If largest is not root
            if (largest != i)
            {
                double swap = lst[i];
                lst[i] = lst[largest];
                lst[largest] = swap;

                // Recursively heapify the affected sub-tree
                Heapify(ref lst, n, largest);
            }
        }
    }
}
