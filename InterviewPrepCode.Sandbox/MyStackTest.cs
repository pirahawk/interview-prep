using System;
using System.Linq;
using Xunit;

namespace InterviewPrepCode.Sandbox
{
    public class MyStackTest
    {

        [Fact]
        void CanPushToStack()
        {
            var stack = new MyStack<int>();

            Assert.Equal(0, stack.StackLength);

            var count = Enumerable.Range(0, 100).Select(i =>
            {
                stack.Push(i);
                return i;
            }).Count();

            Assert.Equal(count, stack.StackLength);
        }

        [Fact]
        void CanPopFromStack()
        {
            var stack = new MyStack<int>();

            Assert.Equal(0, stack.StackLength);


            Assert.Equal(default(int), stack.Pop());

            stack.Push(1);
            stack.Push(2);

            Assert.Equal(2, stack.StackLength);

            Assert.Equal(2, stack.Pop());
            Assert.Equal(1, stack.StackLength);

            Assert.Equal(1, stack.Pop());
            Assert.Equal(0, stack.StackLength);
        }
    }

    public class MyStack<TEntry> where TEntry : IComparable<TEntry>
    {
        private const int INITIAL_LOAD = 10;
        private const double RESIZE_FACTOR = 0.2;

        private TEntry[] Stack;
        private int HeadIndex;
        public int StackLength => HeadIndex + 1;

        public MyStack()
        {
            Stack = new TEntry[INITIAL_LOAD];
            HeadIndex = -1;
        }

        public void Push(TEntry entry)
        {
            if (StackLength + 1 >= Stack.Length)
            {
                Resize();
            }

            Stack[++HeadIndex] = entry;
        }

        public TEntry Pop()
        {
            if (StackLength == 0)
            {
                return default(TEntry);
            }

            return Stack[HeadIndex--];
        }

        private void Resize()
        {
            var newElemLength = (int)Math.Ceiling(RESIZE_FACTOR * Stack.Length);
            Array.Resize(ref Stack, Stack.Length + newElemLength);
        }
    }
}