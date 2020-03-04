using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NUnitLite;

namespace InterviewPrepCode.Sorting
{
    class FbHeapSortSolution
    {

        [TestFixture]
        public class FbHeapSortTest
        {

            static IEnumerable LeftCases
            {
                get
                {
                    var arr = new[] { 0, 1, 2, 3, 4, 5 };

                    yield return new TestCaseData(0).Returns(1);
                    yield return new TestCaseData(1).Returns(3);
                    yield return new TestCaseData(3).Returns(7);
                }
            }

            static IEnumerable RightCases
            {
                get
                {
                    var arr = new[] { 0, 1, 2, 3, 4, 5 };

                    yield return new TestCaseData(0).Returns(2);
                    yield return new TestCaseData(1).Returns(4);
                    yield return new TestCaseData(3).Returns(8);
                }
            }

            [Test]
            [TestCaseSource("LeftCases")]
            public int CanCalculateLeft(int indexToFindLeftOf)
            {

                return FbHeapSort.Left(indexToFindLeftOf);

            }

            [Test]
            [TestCaseSource("RightCases")]
            public int CanCalculateRight(int indexToFindRightOf)
            {

                return FbHeapSort.Right(indexToFindRightOf);

            }

            [Test]
            public void CanCalculateParent()
            {
                Assert.That(FbHeapSort.Parent(1), Is.EqualTo(0));
                Assert.That(FbHeapSort.Parent(2), Is.EqualTo(0));
                Assert.That(FbHeapSort.Parent(3), Is.EqualTo(1));
            }

            [Test]
            public void CanMaxHeapify()
            {
                var t1 = new[] { 16, 4, 10, 14, 7, 9, 3, 2, 8, 1 };

                FbHeapSort.MaxHeapify(t1, 1, t1.Length);

                Assert.That(t1[1], Is.EqualTo(14));
                Assert.That(t1[3], Is.EqualTo(8));
                Assert.That(t1[8], Is.EqualTo(4));
            }

            [Test]
            public void CanBuildMaxHeap()
            {
                var t1 = new[] { 4, 1, 3, 2, 16, 9, 10, 14, 8, 7 };

                FbHeapSort.BuildMaxHeap(t1);

                Assert.That(t1[2], Is.EqualTo(10));
                Assert.That(t1[4], Is.EqualTo(7));
                Assert.That(t1[9], Is.EqualTo(1));
            }

            [Test]
            public void CanSort()
            {
                var t1 = new[] { 16, 14, 10, 8, 7, 9, 3, 2, 4, 1 };

                FbHeapSort.Sort(t1);

                Assert.That(t1[0], Is.EqualTo(1));
                Assert.That(t1[3], Is.EqualTo(4));
                Assert.That(t1[9], Is.EqualTo(16));
            }
        }


        public static class FbHeapSort
        {
            public static void Sort<TVal>(TVal[] arr) where TVal : IComparable<TVal>
            {
                BuildMaxHeap(arr);

                var heapSize = arr.Length;

                for (int i = heapSize - 1; i > 0; i--)
                {
                    var temp = arr[i];
                    arr[i] = arr[0];
                    arr[0] = temp;
                    MaxHeapify(arr, 0, --heapSize);
                }

            }

            public static void BuildMaxHeap<TVal>(TVal[] arr) where TVal : IComparable<TVal>
            {
                var midPoint = (int)Math.Floor(arr.Length / 2m);

                for (int start = midPoint; start >= 0; start--)
                {
                    MaxHeapify(arr, start, arr.Length);
                }
            }

            public static void MaxHeapify<TVal>(TVal[] arr, int i, int heapSize) where TVal : IComparable<TVal>
            {
                var leftIndex = Left(i);
                var rightIndex = Right(i);

                var max = leftIndex < heapSize && arr[i].CompareTo(arr[leftIndex]) < 0 ? leftIndex : i;
                max = rightIndex < heapSize && arr[max].CompareTo(arr[rightIndex]) < 0 ? rightIndex : max;

                if (max != i)
                {
                    var temp = arr[i];
                    arr[i] = arr[max];
                    arr[max] = temp;

                    MaxHeapify(arr, max, heapSize);
                }

            }

            public static int Left(int index)
            {
                var oneBasedIndex = ++index << 1;
                return --oneBasedIndex;
            }

            public static int Right(int index)
            {
                return Left(index) + 1;
            }

            public static int Parent(int index)
            {
                return (int)(Math.Floor(++index / 2m)) - 1;
            }

        }
    }

}