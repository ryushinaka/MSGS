using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MiniScript.MSGS.Data.Numerics
{
    //for reference:
    //https://en.wikipedia.org/wiki/Sorting_algorithm

    public static class IntroSortList
    {
        public static void IntroSort(this List<double> data)
        {
            int depthLimit = 2 * (int)Math.Log(data.Count, 2);
            IntroSortA(data, 0, data.Count - 1, depthLimit);
        }

        private static void IntroSortA(List<double> data, int left, int right, int depthLimit)
        {
            if (left < right)
            {
                if (depthLimit == 0)
                {
                    HeapSort(data, left, right);
                }
                else
                {
                    int partitionIndex = Partition(data, left, right);
                    IntroSortA(data, left, partitionIndex - 1, depthLimit - 1);
                    IntroSortA(data, partitionIndex + 1, right, depthLimit - 1);
                }
            }
        }

        private static int Partition(List<double> data, int left, int right)
        {
            double pivot = data[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (data[j] < pivot)
                {
                    i++;
                    Swap(data, i, j);
                }
            }

            Swap(data, i + 1, right);
            return i + 1;
        }

        private static void HeapSort(List<double> data, int left, int right)
        {
            int n = right - left + 1;
            for (int i = n / 2 - 1 + left; i >= left; i--)
            {
                Heapify(data, n, i, left);
            }

            for (int i = right; i >= left; i--)
            {
                Swap(data, left, i);
                Heapify(data, i - left, left, left);
            }
        }

        private static void Heapify(List<double> data, int n, int i, int left)
        {
            int largest = i;
            int l = 2 * i + 1 - left;
            int r = 2 * i + 2 - left;

            if (l < n + left && data[l] > data[largest])
            {
                largest = l;
            }

            if (r < n + left && data[r] > data[largest])
            {
                largest = r;
            }

            if (largest != i)
            {
                Swap(data, i, largest);
                Heapify(data, n, largest, left);
            }
        }

        private static void Swap(List<double> data, int i, int j)
        {
            double temp = data[i];
            data[i] = data[j];
            data[j] = temp;
        }
    }


}
