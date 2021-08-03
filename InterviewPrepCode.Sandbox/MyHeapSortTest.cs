using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace InterviewPrepCode.Sandbox
{
    public class MyHeapSortTest
    {
        public static IEnumerable<object[]> HeapBuildTestCases
        {
            get
            {
                yield return new object[] { new[] { 1, 3, 2 }, 3, 0, new[] { 3, 1, 2 } };
            }
        }

        [Theory]
        [MemberData(nameof(HeapBuildTestCases))]
        public void CanMaxHeapify(int[] inputElements, int heapSize, int startIndex, int[] expected)
        {
            var heap = new Heap()
            {
                Elements = inputElements,
                HeapSize = heapSize
            };

            MyHeapSorter.MaxHeapify(heap, startIndex);

            Assert.Equal(expected, heap.Elements);
        }

        [Fact]
        public void CanBuildHeap()
        {
            var t1 = new[] { 4, 1, 3, 2, 16, 9, 10, 14, 8, 7 };

            var heap = new Heap()
            {
                Elements = t1,
                HeapSize = t1.Length
            };

            MyHeapSorter.BuildMaxHeap(heap);
        }

        [Fact]
        public void CanSort()
        {
            var t1 = new[] { 4, 1, 3, 2, 16};

            var heap = new Heap()
            {
                Elements = t1,
                HeapSize = t1.Length
            };

            var result = MyHeapSorter.HeapSort(heap).ToArray();
            Assert.Equal(new[] { 16, 4, 3, 2, 1 }, result);
        }

        public class MyHeapSorter
        {

            public static IEnumerable<int> HeapSort(Heap heap)
            {
                BuildMaxHeap(heap);

                for (int i = heap.HeapSize-1; i >=0; i--)
                {
                    var currentMax = heap[0];
                    heap[0] = heap[i];
                    heap[i] = currentMax;
                    heap.HeapSize--;
                    yield return currentMax;
                    MaxHeapify(heap, 0);
                }
            }

            public static void BuildMaxHeap(Heap heap)
            {
                int midwayParentIndex = (int) Math.Floor(heap.HeapElementsLength / 2d);

                while (midwayParentIndex >= 0)
                {
                    MaxHeapify(heap, midwayParentIndex);
                    --midwayParentIndex;
                }
            }

            public static void MaxHeapify(Heap heap, int elemIndex)
            {

                var leftIndex = Left(elemIndex);
                var rightIndex = Right(elemIndex);
                var swapIndex = elemIndex;

                if (leftIndex < heap.HeapSize && heap[leftIndex] > heap[swapIndex])
                {
                    swapIndex = leftIndex;
                }

                if (rightIndex < heap.HeapSize && heap[rightIndex] > heap[swapIndex])
                {
                    swapIndex = rightIndex;
                }

                if (swapIndex != elemIndex)
                {
                    var currentKey = heap[elemIndex];
                    heap[elemIndex] = heap[swapIndex];
                    heap[swapIndex] = currentKey;
                    MaxHeapify(heap, swapIndex);
                }

                int Left(int index)
                {
                    return ((index + 1) << 1)-1;
                    //return index > 0? index << 1 : 1;
                }

                int Right(int index)
                {
                    return Left(index) + 1;
                }

                int Parent(int index)
                {
                    return (int)Math.Floor((index - 1) / 2d);
                }
            }
        }

        public class Heap
        {
            public int[] Elements { get; set; }
            public int HeapSize { get; set; }
            public int HeapElementsLength => Elements?.Length ?? 0;

            public int this[int key]
            {
                get => Elements[key];
                set => Elements[key] = value;
            }
        }
    }
}