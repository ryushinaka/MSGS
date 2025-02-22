using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MiniScript.MSGS.Data.Numerics
{
    //For reference:
    //https://en.wikipedia.org/wiki/Sorting_algorithm


    public static class TournamentSort
    {
        public static void Sort(this List<double> data)
        {
            int size = data.Count;
            // The tournament tree will be stored in an array
            int treeSize = 2 * size;
            double[] tree = new double[treeSize];

            // Initialize the tournament tree with infinity
            for (int i = 0; i < treeSize; i++)
            {
                tree[i] = double.PositiveInfinity;
            }

            // Populate leaves with the data
            for (int i = 0; i < size; i++)
            {
                tree[size + i] = data[i];
            }

            // Build the tournament tree
            for (int i = size - 1; i > 0; i--)
            {
                tree[i] = Math.Min(tree[2 * i], tree[2 * i + 1]);
            }

            // Perform the sort
            for (int i = 0; i < size; i++)
            {
                double min = tree[1];
                data[i] = min;  // Store the smallest element in the list

                // Replace the element with infinity and rebuild the tree
                for (int pos = Array.IndexOf(tree, min, size); pos > 0; pos /= 2)
                {
                    tree[pos] = double.PositiveInfinity;
                    if (pos / 2 > 0) tree[pos / 2] = Math.Min(tree[pos], tree[pos ^ 1]);
                }
            }
        }
    }

}