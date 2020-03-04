using System;
using NUnit.Framework;
using NUnitLite;

namespace InterviewPrepCode.DataStructures
{
    class FbStackSolution
    {
        [TestFixture]
        class StackT
        {

            [Test]
            public void PartitionIndexCorrectly()
            {
                var stack = new Stack<int>();
                stack.Push(1);
                stack.Push(2);

                Assert.That(stack.Pop(), Is.EqualTo(2));
                Assert.That(stack.Pop(), Is.EqualTo(1));
                Assert.That(stack.Pop(), Is.EqualTo(default(int)));

            }
        }

        public class Stack<TVal>
        {
            Entry<TVal> Head;

            public void Push(TVal val)
            {
                var newEntry = new Entry<TVal>(val);

                Head ??= newEntry;

                if (Head != newEntry)
                {
                    newEntry.Next = Head;
                    Head = newEntry;
                }
            }


            public TVal Pop()
            {
                if (Head == null)
                {
                    return default(TVal);
                }

                var top = Head.Value;
                Head = Head.Next;
                return top;
            }
        }

        public class Entry<TVal>
        {
            public TVal Value;
            public Entry<TVal> Next;

            public Entry(TVal val)
            {
                Value = val;
            }
        }


    }
}