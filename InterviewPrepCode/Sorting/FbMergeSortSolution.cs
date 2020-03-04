using System;
using NUnit.Framework;
using NUnitLite;

namespace InterviewPrepCode.Sorting
{
    class FbMergeSortSolution
    {

        [TestFixture]
        class FbMergeSortTest
        {

            [Test]
            public void MergeTest()
            {
                var a1 = new int[] { 8, 3, 1, 8, 98, 8, 32, 64, 56 };

                var startIndex = 0;
                var endIndex = a1.Length - 1;
                var midIndex = (int)Math.Floor((startIndex + endIndex) / 2m);

                MergeAlg.MergeSort(a1, startIndex, endIndex);

                foreach (var i in a1) { Console.Write($"{i}/"); }
            }
        }

        static class MergeAlg
        {
            public static void MergeSort<TVal>(TVal[] arr, int startIndex, int endIndex) where TVal : IComparable<TVal>
            {
                if (startIndex >= endIndex)
                {
                    return;
                }

                var midIndex = (int)Math.Floor((startIndex + endIndex) / 2m);
                MergeSort(arr, startIndex, midIndex);
                MergeSort(arr, midIndex + 1, endIndex);
                Merge(arr, startIndex, midIndex, endIndex);
            }

            public static void Merge<TVal>(TVal[] arr, int startIndex, int midIndex, int endIndex) where TVal : IComparable<TVal>
            {
                int lengthLeft = midIndex - startIndex + 1;
                int lengthRight = endIndex - midIndex;

                var left = new TVal[lengthLeft];
                var right = new TVal[lengthRight];

                int i = 0;
                int j = startIndex;

                for (; j <= midIndex; j++)
                {
                    left[i++] = arr[j];
                }

                i = 0;
                j = midIndex + 1;

                for (; j <= endIndex; j++)
                {
                    right[i++] = arr[j];
                }

                i = 0;
                j = 0;
                int k = startIndex;

                while (i < left.Length && j < right.Length)
                {
                    if (left[i].CompareTo(right[j]) <= 0)
                    {
                        arr[k++] = left[i++];
                    }
                    else
                    {
                        arr[k++] = right[j++];
                    }
                }

                while (i < left.Length)
                {
                    arr[k++] = left[i++];
                }

                while (j < right.Length)
                {
                    arr[k++] = right[j++];
                }

            }

        }
    }

}