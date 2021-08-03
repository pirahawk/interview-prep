using Xunit;

namespace InterviewPrepCode.Sandbox
{
    public class MyQueueTest
    {
        [Fact]
        void CanEnqueue()
        {
            var queue = new MyQueue<int>();

            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
        }

        [Fact]
        void CanDequeue()
        {
            var queue = new MyQueue<int>();

            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            Assert.Equal(1, queue.Dequeue());
            Assert.Equal(2, queue.Dequeue());
            Assert.Equal(3, queue.Dequeue());
            Assert.Equal(default(int), queue.Dequeue());

            queue.Enqueue(4);
            Assert.Equal(4, queue.Dequeue());
        }
    }

    public class MyQueue<TEntry>
    {
        private QueueEntry<TEntry> _head;
        private QueueEntry<TEntry> _tail;
        public int Length;

        public void Enqueue(TEntry i)
        {
            var entry = new QueueEntry<TEntry>
            {
                Entry = i
            };

            _head ??= entry;

            if (_tail == null)
            {
                _tail = entry;
            }
            else
            {
                _tail.Next = entry;
                _tail = entry;
            }
        }

        public TEntry Dequeue()
        {
            if (_head == null)
            {
                return default(TEntry);
            }

            var currentValue = _head.Entry;
            _head = _head.Next;
            _tail = _head != null ? _tail : null;
            return currentValue;
        }
    }

    public class QueueEntry<TEntry>
    {
        public TEntry Entry;
        public QueueEntry<TEntry> Next;
    }
}