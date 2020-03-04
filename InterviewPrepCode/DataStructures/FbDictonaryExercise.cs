using System;
using NUnit.Framework;
using NUnitLite;

namespace InterviewPrepCode.DataStructures
{
    public class FbDictonaryExercise
    {

        [TestFixture]
        public class FbDictionaryTest
        {
            [Test]
            public void Test()
            {
                var dict = new FbDictionary<string, int>();


                for (int i = 0; i < 10000; i++)
                {

                    //dict.Add(i.GetHashCode().ToString(), i);

                    dict.Add($"someKey{i.GetHashCode()}", i);
                }

                Console.WriteLine($"Stats: {dict.buckets.Length} - {dict.numEntries} - {dict.numCollisions}");

                
            }
        }


        class FbDictionary<TKey, TValue>
        {

            const int initialBucketSize = 5;
            const float desiredLoadFactor = 2;
            const int scaleFactor = 5;

            public Entry<TKey, TValue>[] buckets;
            public int numEntries;
            public int numCollisions;


            public FbDictionary()
            {
                buckets = new Entry<TKey, TValue>[initialBucketSize];
            }

            public void Add(TKey key, TValue val)
            {

                TryGrowBuckets();

                var hash = GetHash(key);
                var newEntry = new Entry<TKey, TValue>
                {
                    Key = key,
                    Value = val
                };

                var targetBucketIndex = hash % buckets.Length;
                numEntries++;

                if (buckets[targetBucketIndex] != null)
                {
                    //Console.WriteLine($"Collision detected {targetBucketIndex}-{key}-{val}-{hash}");

                    var currentEntry = buckets[targetBucketIndex];

                    // prevent stackoverflow??
                    while (currentEntry.Next != null)
                    {
                        currentEntry = currentEntry.Next;
                    }

                    currentEntry.Next = newEntry;
                    numCollisions++;
                    return;
                }

                buckets[targetBucketIndex] = newEntry;
            }

            private void TryGrowBuckets()
            {
                if (numEntries <= 0)
                {
                    return;
                }

                while (GetLoadFactor() < desiredLoadFactor)
                {
                    var newBucketSize = Math.Ceiling((desiredLoadFactor * scaleFactor) * numEntries);
                    Console.WriteLine($"Resize buckets {GetLoadFactor()}-{numEntries}-{newBucketSize}");
                    var newBuckets = new Entry<TKey, TValue>[Convert.ToInt32(newBucketSize)];
                    buckets.CopyTo(newBuckets, 0);
                    buckets = newBuckets;
                }
            }


            private float GetLoadFactor()
            {
                return buckets.Length / numEntries;
            }

            private int GetHash(TKey key)
            {
                var hash = key.GetHashCode();
                hash = hash & int.MaxValue;

                return hash;
            }

        }

        class Entry<TKey, TValue>
        {
            public TKey Key;
            public TValue Value;
            public Entry<TKey, TValue> Next;


        }
    }
}