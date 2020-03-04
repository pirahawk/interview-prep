using System;
using NUnit.Framework;
using NUnitLite;

namespace InterviewPrepCode.DataStructures
{
    class FbHashSolution
    {

        [TestFixture]
        public class Test
        {

            [Test]
            public void TestBarker()
            {
                var hashT = new FbHashTable();

                for (int i = 0; i <= 100000; i++)
                {
                    hashT.Add($"a{i}", i);
                }

                var test = hashT.Get("a832");
                Assert.That(test, Is.EqualTo(832));
                



                
            }
        }

        public class FbHashTable
        {

            int initialBucketSize = 100;
            double desiredLoadFactor = 0.75;
            int growthFactor = 2;

            Entry[] buckets;
            int loadedBucketCount;

            public FbHashTable()
            {
                buckets = new Entry[initialBucketSize];
                loadedBucketCount = 0;
            }

            public int Get(string key)
            {
                var calculatedHash = GetHashCode(key);
                var bucketIndex = GetKeyIndex(calculatedHash);

                var entry = buckets[bucketIndex];

                while (entry != null)
                {

                    if (entry.Hash == calculatedHash)
                    {
                        return entry.Value;
                    }

                    entry = entry.Next;
                }

                throw new Exception($"Key not found: {key}");
            }

            public void Add(string key, int itemValue)
            {
                AttemptResize();
                var calculatedHash = GetHashCode(key);
                var newEntry = new Entry { Key = key, Value = itemValue, Hash = calculatedHash };
                Store(newEntry);
            }

            private void AttemptResize()
            {
                if (buckets.Length == 0)
                {
                    return;
                }

                var loadFactor = (double)loadedBucketCount / buckets.Length;
                if (loadFactor > desiredLoadFactor)
                {
                    ResizeBuckets();
                    ///Console.WriteLine($"LoadFactor Excceed {loadFactor}");
                }
            }

            private void ResizeBuckets()
            {
                var newBucketLength = buckets.Length * growthFactor;
                var temp = buckets;

                buckets = new Entry[newBucketLength];
                loadedBucketCount = 0;

                Console.WriteLine($"LoadFactor Excceed expanded to: {newBucketLength}");

                for (int i = 0; i < temp.Length; i++)
                {
                    var entry = temp[i];

                    while (entry != null)
                    {
                        var nextHolder = entry.Next;
                        entry.Next = null;
                        Store(entry);
                        entry = nextHolder;
                    }
                }
            }

            private void Store(Entry entry)
            {
                //var bucketIndex = entry.Hash % buckets.Length;
                var bucketIndex = GetKeyIndex(entry.Hash);

                if (buckets[bucketIndex] != null)
                {
                    buckets[bucketIndex].Add(entry);
                    return;
                }

                buckets[bucketIndex] = entry;
                loadedBucketCount++;
            }

            private int GetKeyIndex(int hash)
            {
                return hash % buckets.Length;
            }

            private int GetHashCode(string key)
            {
                var hash = key.GetHashCode();
                hash = hash & int.MaxValue;
                return hash;
            }
        }

        public class Entry
        {
            public string Key;
            public int Value;
            public int Hash;

            public Entry Next;

            public void Add(Entry newEntry)
            {
                Next ??= newEntry;

                if (Next != newEntry)
                {
                    Next.Add(newEntry);
                }
            }
        }
    }
}