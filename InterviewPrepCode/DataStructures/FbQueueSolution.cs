using System;
using NUnit.Framework;
using NUnitLite;

namespace InterviewPrepCode.DataStructures
{
    class FbQueueSolution
    {
        [TestFixture]
        class QueueT
        {

            [Test]
            public void WorksCorrectly()
            {
                var queue = new Queue<int>();

                queue.Enqueue(1);
                queue.Enqueue(2);
                queue.Enqueue(3);


                Assert.That(queue.Dequeue(), Is.EqualTo(1));
                Assert.That(queue.Dequeue(), Is.EqualTo(2));
                Assert.That(queue.Dequeue(), Is.EqualTo(3));
                Assert.That(queue.Dequeue(), Is.EqualTo(default(int)));


                //             Assert.That(stack.Pop(), Is.EqualTo(2));
                //             Assert.That(stack.Pop(), Is.EqualTo(1));
                //             Assert.That(stack.Pop(), Is.EqualTo(default(int)));

            }
        }

        public class Queue<TVal>
        {
            Entry<TVal> Start;
            Entry<TVal> End;

            public void Enqueue(TVal item)
            {
                var next = new Entry<TVal>(item);

                if (End != null)
                {
                    End.Next = next;
                }

                End = next;

                Start = Start ?? next;

            }

            public TVal Dequeue()
            {
                var temp = Start;

                if (temp != null)
                {
                    Start = temp.Next;
                    return temp.Value;
                }

                return default(TVal);
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