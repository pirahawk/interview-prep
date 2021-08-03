using System;
using System.Linq;
using Xunit;

namespace InterviewPrepCode.Sandbox
{
    public class MyPriorityQueueTest
    {
        [Fact]
        private void Test()
        {
            var queue = new MyPriorityQueue<int>();
            Assert.Equal(0, queue.Length);

            Array.ForEach(Enumerable.Range(1, 20).ToArray(), i=> queue.Enqueue(i));

            Assert.Equal(20, queue.Length);

            Array.ForEach(Enumerable.Range(21, 20).ToArray(), i => queue.Enqueue(i));

            Assert.Equal(40, queue.Length);
        }

        [Fact]
        void TestPrioritySetting()
        {
            var queue = new MyPriorityQueue<int>();
            queue.Enqueue(4);
            queue.Enqueue(5);
            queue.Enqueue(1);

            Assert.Equal(3, queue.Length);

            Assert.Equal(1, queue.Dequeue());
            Assert.Equal(2, queue.Length);

            Assert.Equal(4, queue.Dequeue());
            Assert.Equal(1, queue.Length);

            Assert.Equal(5, queue.Dequeue());
            Assert.Equal(0, queue.Length);

        }
    }

    public class MyPriorityQueue<TItem> where TItem: IComparable<TItem>
    {
        private const int INITIAL_LENGTH = 10;
        private const double GROWTH_FACTOR = 0.2;

        private TItem[] queue;
        private int startIndex;
        private int endIndex;

        public MyPriorityQueue()
        {
            queue = new TItem[INITIAL_LENGTH];
            startIndex = -1;
            endIndex = -1;
        }

        public int Length => endIndex > 0? (endIndex - startIndex) + 1 :0;

        public void Enqueue(TItem i)
        {
            if (endIndex+1 == queue.GetUpperBound(0))
            {
                Resize();
            }

            queue[++endIndex] = i;
            startIndex = startIndex < 0 ? 0 : startIndex;
            SortByPriority();

            void SortByPriority()
            {
                var key = queue[endIndex];
                int j = endIndex - 1;

                while (j >= startIndex && queue[j].CompareTo(key) != -1)
                {
                    queue[j + 1] = queue[j];
                    j--;
                }

                queue[j + 1] = key;
            }
        }

        public TItem Dequeue()
        {
            if (Length <=0)
            {
                return default(TItem);
            }

            return queue[startIndex++];
        }

        private void Resize()
        {
            var newLength = (int)(queue.Length * GROWTH_FACTOR) + queue.Length;
            Array.Resize(ref queue, newLength);
        }

        
    }
}