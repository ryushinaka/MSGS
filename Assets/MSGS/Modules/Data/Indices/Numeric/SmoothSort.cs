using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniScript.MSGS.Data.Numerics
{
    //for reference:
    //https://en.wikipedia.org/wiki/Sorting_algorithm

    public static class SmoothSortList
    {
        public static void SmoothSort(this List<double> list)
        {
            int length = list.Count;
            int[] lp = GenerateLeonardoNumbers(length);

            // Construct the heap
            for (int i = 1; i < length; i++)
            {
                if ((i & 3) == 3)
                {
                    Sift(ref list, i - 1, 1, lp);
                    Sift(ref list, i, 0, lp);
                }
                else
                {
                    Sift(ref list, i, lp[RightMostZeroBit(i)], lp);
                }
            }

            // Dismantle the heap
            for (int i = length - 1; i >= 0; i--)
            {
                if (lp[RightMostZeroBit(i)] == 1)
                {
                    continue;
                }
                Trinkle(ref list, i, lp[RightMostZeroBit(i)], lp, false);
            }
        }

        private static void Sift(ref List<double> list, int p, int pShift, int[] lp)
        {
            double val = list[p];
            while (pShift > 1)
            {
                int rt = p - 1;
                int lf = p - 1 - lp[pShift - 2];
                if (val >= list[lf] && val >= list[rt])
                    break;
                if (list[lf] >= list[rt])
                {
                    list[p] = list[lf];
                    p = lf;
                    pShift -= 1;
                }
                else
                {
                    list[p] = list[rt];
                    p = rt;
                    pShift -= 2;
                }
            }
            list[p] = val;
        }

        private static void Trinkle(ref List<double> list, int p, int pShift, int[] lp, bool isTrusty)
        {
            double val = list[p];
            while (pShift > 1)
            {
                int stepson = p - lp[pShift];

                if (list[stepson] <= val)
                    break;

                if (!isTrusty && pShift > 1 &&
                    list[p - 1] >= list[p - 1 - lp[pShift - 2]] &&
                    list[p - 1] >= list[p - 1 - lp[pShift - 1]])
                {
                    break;
                }

                list[p] = list[stepson];
                p = stepson;
                int trail = RightMostZeroBit(p + 1);
                pShift = lp[trail];
                isTrusty = false;
            }
            if (!isTrusty)
            {
                list[p] = val;
                Sift(ref list, p, pShift, lp);
            }
        }

        private static int RightMostZeroBit(int x)
        {
            int position = 0;
            while ((x & 1) == 1)
            {
                x >>= 1;
                position++;
            }
            return position;
        }

        private static int[] GenerateLeonardoNumbers(int limit)
        {
            List<int> numbers = new List<int> { 1, 1 };
            while (true)
            {
                int next = numbers[numbers.Count - 1] + numbers[numbers.Count - 2] + 1;
                if (next > limit) break;
                numbers.Add(next);
            }
            return numbers.ToArray();
        }
    }
}