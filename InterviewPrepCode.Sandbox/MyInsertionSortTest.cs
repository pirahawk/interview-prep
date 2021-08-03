using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Xunit;

namespace InterviewPrepCode.Sandbox
{
    public class MyInsertionSortTest
    {
        public static IEnumerable<object[]> InsertionSortTestCases
        {
            get
            {
                yield return new[] { new int[0], new int[0] };
                yield return new[] { new[] { 1 }, new[] { 1 } };
                yield return new[] { new[] { 5, 3, 2, 1, 4 }, new[] { 1, 2, 3, 4, 5 } };

                var rand = Enumerable.Range(0, 20)
                    .Select(i => RandomNumberGenerator.GetInt32(100))
                    .ToArray();

                yield return new[] { rand, rand.OrderBy(i => i).ToArray() };

                yield return new[] { new[] { "b", "a" }, new[] { "a", "b" } };
                yield return new[] { new[] { 3d, 1d }, new[] { 1d, 3d } };
            }
        }


        [Theory]
        [MemberData(nameof(InsertionSortTestCases))]
        public void CanInsertionSort<TIn>(TIn[] input, TIn[] expected) where TIn : IComparable<TIn>
        {
            MyInsertionSort.Sort(input);
            Assert.Equal(expected, input);
        }

        public class MyInsertionSort
        {
            public static void Sort<TIn>(TIn[] items) where TIn : IComparable<TIn>
            {
                if (items.Length <= 1)
                {
                    return;
                }

                for (int j = 1; j < items.Length; j++)
                {
                    var key = items[j];

                    int i = j - 1;


                    //while (i >= 0 && items[i] > key)
                    while (i >= 0 && items[i].CompareTo(key) == 1)

                    {
                        items[i + 1] = items[i];
                        i--;
                    }

                    items[i + 1] = key;
                }



                // for (int j = items.GetLowerBound(0); j <= items.GetUpperBound(0); j++)
                // {
                //     int i = j-1;
                //     var key = items[j];
                //
                //     while (i >= 0 && items[i] > key)
                //     {
                //         items[i + 1] = items[i];
                //         i--;
                //     }
                //
                //     items[i + 1] = key;
                // }
            }
        }
    }
}