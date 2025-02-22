using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniScript.MSGS.Data.Numerics
{
    //for reference:
    //https://en.wikipedia.org/wiki/Sorting_algorithm

    public static class OddEvenSortExtension
    {
        public static void OddEvenSort(this List<double> lst)
        {
            int n = lst.Count;
            bool sorted = false;

            while (!sorted)
            {
                sorted = true;

                //odd's
                for (int i = 1; i < n - 1; i += 2)
                {
                    if (lst[i] > lst[i + 1])
                    {
                        Swap(ref lst, i, i + 1);
                        sorted = false;
                    }
                }

                //even's
                for (int i = 0; i < n - 1; i += 2)
                {
                    if (lst[i] > lst[i + 1])
                    {                        
                        Swap(ref lst, i, i + 1);
                        sorted = false;
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
