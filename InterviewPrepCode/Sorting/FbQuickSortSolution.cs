using System;
using NUnit.Framework;
using NUnitLite;

namespace InterviewPrepCode.Sorting
{
    class FbQuickSortSolution
    {


        [TestFixture]
        public class FbQuickSortTest
        {

            [Test]
            public void TestPartition()
            {
                //var t1 = new []{2,8,7,1,3,5,5,6,4};
                var t1 = new[] { 4, 2, 3 };

                Assert.Throws<Exception>(() => FbQuickSort.Partition(t1, 4, 3));

                var result = FbQuickSort.Partition(t1, 0, 2);

                Assert.That(result, Is.EqualTo(1));

                Assert.That(t1[0], Is.EqualTo(2));
                Assert.That(t1[1], Is.EqualTo(3));
                Assert.That(t1[2], Is.EqualTo(4));

                
            }


            [Test]
            public void TestSort()
            {
                var t1 = new[] { 2, 8, 7, 1, 3, 5, 5, 6, 4 };
                //var t1 = new []{4,2,3};


                FbQuickSort.Sort(t1, 0, t1.Length - 1);

                Assert.That(t1[0], Is.EqualTo(1));
                Assert.That(t1[3], Is.EqualTo(4));
                Assert.That(t1[8], Is.EqualTo(8));
            }
        }

        public class FbQuickSort
        {

            public static void Sort<TVal>(TVal[] arr, int start, int end) where TVal : IComparable<TVal>
            {
                if (start < end)
                {
                    var partitionIndex = Partition(arr, start, end);
                    Sort(arr, start, partitionIndex - 1);
                    Sort(arr, partitionIndex + 1, end);
                }
            }

            public static int Partition<TVal>(TVal[] arr, int start, int end) where TVal : IComparable<TVal>
            {

                if (start > end)
                {
                    throw new Exception($"start cannot be > end");
                }

                var key = arr[end];
                var partitionIndex = start - 1;

                for (int j = start; j < end; j++)
                {
                    if (arr[j].CompareTo(key) <= 0)
                    {

                        var temp = arr[++partitionIndex];
                        arr[partitionIndex] = arr[j];
                        arr[j] = temp;
                    }
                }

                var tempSwap = arr[++partitionIndex];
                arr[partitionIndex] = key;
                arr[end] = tempSwap;
                return partitionIndex;
            }


        }
    }
}