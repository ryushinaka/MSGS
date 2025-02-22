using System;
using System.Collections.Generic;

namespace MiniScript.MSGS.Data.Numerics
{
    public static class LibrarySorter
    {
        // Factor defines the gap ratio. A factor of 2 is common, meaning every slot doubles the array size.
        private const int ExpansionFactor = 2;

        public static void LibrarySort(this List<double> list)
        {
            if (list.Count <= 1) return;

            // Expand the list to accommodate gaps
            List<double?> sortedWithGaps = new List<double?>((list.Count * ExpansionFactor) + 1);
            for (int i = 0; i < sortedWithGaps.Capacity; i++)
            {
                sortedWithGaps.Add(null); // Initialize with null gaps
            }

            // Insert the first element
            sortedWithGaps[sortedWithGaps.Capacity / ExpansionFactor] = list[0];

            int n = 1; // Number of sorted elements

            for (int i = 1; i < list.Count; i++)
            {
                double valueToInsert = list[i];
                int index = FindInsertIndex(sortedWithGaps, valueToInsert, n);
                InsertWithGaps(sortedWithGaps, index, valueToInsert, n);
                n++;

                // Expand gaps if needed
                if (n > sortedWithGaps.Capacity / ExpansionFactor)
                {
                    sortedWithGaps = ExpandGaps(sortedWithGaps);
                }
            }

            // Compact the sorted list by removing gaps
            list.Clear();
            foreach (var item in sortedWithGaps)
            {
                if (item.HasValue)
                {
                    list.Add(item.Value);
                }
            }
        }

        private static int FindInsertIndex(List<double?> sortedWithGaps, double value, int n)
        {
            int left = 0;
            int right = sortedWithGaps.Count - 1;

            // Binary search within non-null values
            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (!sortedWithGaps[mid].HasValue || sortedWithGaps[mid] < value)
                {
                    left = mid + 1;
                }
                else if (sortedWithGaps[mid] > value)
                {
                    right = mid - 1;
                }
                else
                {
                    return mid;
                }
            }

            return left; // Position to insert the new element
        }

        private static void InsertWithGaps(List<double?> sortedWithGaps, int index, double value, int n)
        {
            // Insert the element in the gap position if available
            if (!sortedWithGaps[index].HasValue)
            {
                sortedWithGaps[index] = value;
            }
            else
            {
                // Shift elements to make space
                int shiftIndex = index;
                while (sortedWithGaps[shiftIndex].HasValue)
                {
                    shiftIndex++;
                }

                // Shift elements
                for (int j = shiftIndex; j > index; j--)
                {
                    sortedWithGaps[j] = sortedWithGaps[j - 1];
                }

                sortedWithGaps[index] = value;
            }
        }

        private static List<double?> ExpandGaps(List<double?> list)
        {
            int newSize = list.Count * ExpansionFactor;
            List<double?> expanded = new List<double?>(newSize);

            // Insert gaps while copying elements
            for (int i = 0; i < list.Count; i++)
            {
                expanded.Add(list[i]);
                if (i % ExpansionFactor == 0) // Add extra gaps
                {
                    expanded.Add(null);
                }
            }

            return expanded;
        }
    }
}

