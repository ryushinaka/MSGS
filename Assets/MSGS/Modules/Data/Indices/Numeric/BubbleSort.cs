using System;
using System.Collections.Generic;


namespace MiniScript.MSGS.Data.Numerics
{
    //for reference:
    //https://en.wikipedia.org/wiki/Sorting_algorithm

    public static class BubbleSortExtension
    {
        public static void BubbleSort(this List<double> lst)
        {
            int n = lst.Count;
            bool swapped;

            for (int i = 0; i < n - 1; i++)
            {
                swapped = false;
                
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (lst[j] > lst[j + 1])
                    {                
                        double temp = lst[j];
                        lst[j] = lst[j + 1];
                        lst[j + 1] = temp;
                        swapped = true;
                    }
                }

                if (!swapped) { break; }
            }
        }
    }

}