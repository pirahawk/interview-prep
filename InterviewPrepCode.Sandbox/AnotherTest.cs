using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Xunit;

namespace InterviewPrepCode.Sandbox
{
    public class AnotherTest
    {
        [Fact]
        void CanInsertionSort()
        {
            var input = new[] { 4, 3, 1, 2 };
            InsertionSort(input);

            Assert.Equal(new[] { 1, 2, 3, 4 }, input);
        }

        void InsertionSort(int[] input)
        {

            for (int i = 1; i < input.Length; i++)
            {
                var currentKey = input[i];
                var j = i - 1;

                while (j >= 0 && input[j] > currentKey)
                {
                    input[j + 1] = input[j];
                    j--;
                }

                input[j + 1] = currentKey;
            }
        }

        [Fact]
        void CanMaxHeapify()
        {
            var input = new[] { 1, 2, 3, 4 };
            var heap = new Heap
            {
                A = input,
                HeapSize = input.Length
            };

            MaxHeapify(heap, 0);

            Assert.Equal(new[] { 3, 2, 1, 4 }, input);
        }

        [Fact]
        void CanBuildMaxHeap()
        {
            var input = new[] { 5, 2, 1, 3, 4 };
            var heap = new Heap
            {
                A = input,
                HeapSize = input.Length
            };

            BuildMaxHeap(heap);
        }

        [Fact]
        void CanSort()
        {
            var input = new[] { 5, 2, 1, 3, 4 };
            var result = HeapSort(input);
            Assert.Equal(new[] { 5, 4, 3, 2, 1 }, result);
        }

        IEnumerable<int> HeapSort(int[] input)
        {
            var heap = new Heap
            {
                A = input,
                HeapSize = input.Length
            };

            BuildMaxHeap(heap);

            for (int i = heap.HeapSize-1; i >= 0; i--)
            {
                var currentMax = input[0];

                input[0] = input[i];
                input[i] = currentMax;
                heap.HeapSize--;

                MaxHeapify(heap, 0);

                yield return currentMax;

            }
        }

        void BuildMaxHeap(Heap h)
        {
            var midwayIndex = (int)Math.Floor(h.HeapSize / 2d);

            for (var i = midwayIndex; i >= 0; i--)
            {
                MaxHeapify(h, i);
            }
        }

        void MaxHeapify(Heap h, int index)
        {
            var leftIndex = Left(index);
            var rightIndex = Right(index);
            var maxIndex = index;

            if (leftIndex < h.HeapSize && h[leftIndex] > h[index])
            {
                maxIndex = leftIndex;
            }

            if (rightIndex < h.HeapSize && h[rightIndex] > h[maxIndex])
            {
                maxIndex = rightIndex;
            }

            if (maxIndex != index)
            {
                var key = h[index];
                h[index] = h[maxIndex];
                h[maxIndex] = key;
                MaxHeapify(h, maxIndex);
            }

            int Left(int i)
            {
                return ((i + 1) << 1) - 1;
            }

            int Right(int i)
            {
                return Left(i) + 1;
            }
        }

        public class Heap
        {
            public int[] A { get; set; }
            public int Length => A?.Length ?? 0;
            public int HeapSize { get; set; }


            public int this[int key]
            {
                get => A[key];
                set => A[key] = value;
            }

        }
    }
}