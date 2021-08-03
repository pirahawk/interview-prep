using System;
using System.Linq;
using Xunit;

namespace InterviewPrepCode.Sandbox
{
    public class MyDictionaryTest
    {
        [Fact]
        void CanCreate()
        {
            var dictionary = new MyDictionary<int, string>();

            // dictionary.Add(5, "e");

            var inserts = Enumerable.Range(1, 100).Select(i => new
            {
                Key = i,
                Value = $"Val-{i}"
            }).ToArray();

            Array.ForEach(inserts, obj => dictionary.Add(obj.Key, obj.Value));

            Assert.True(dictionary.HasKey(8));
            Assert.False(dictionary.HasKey(800));
        }

        [Fact]
        void CanRemove()
        {
            var dictionary = new MyDictionary<int, string>();

            var inserts = Enumerable.Range(1, 10).Select(i => new
            {
                Key = i,
                Value = $"Val-{i}"
            }).ToArray();

            Array.ForEach(inserts, obj => dictionary.Add(obj.Key, obj.Value));

            // var hasKey = dictionary.HasKey(8);
            // dictionary.Remove(8);
            // dictionary.Remove(1);
            // dictionary.Remove(2);
            //
            //
            // dictionary.Add(8, $"8");
            // dictionary.Add(2, $"8");
            // dictionary.Add(1, $"8");

            Array.ForEach(inserts, obj => dictionary.Remove(obj.Key));
            Array.ForEach(inserts, obj => dictionary.Add(obj.Key, obj.Value));
            Array.ForEach(inserts, obj => dictionary.Add(obj.Key, obj.Value));
        }
    }

    public class MyDictionary<TKey, TValue> where TKey: IComparable<TKey>
    {
        private const int HASH_BUCKETS_SIZE = 1024;
        private const int ENTRY_SIZE = 10;
        private const double GROWTH_FACTOR = 0.2;

        private int[] buckets;
        private Entry<TKey, TValue>[] entries;
        private int nextEntryInsertIndex;
        private int freeList;
        private int freeCount;

        public MyDictionary()
        {
            buckets = new int [HASH_BUCKETS_SIZE];
            Array.Fill(buckets, -1);

            entries = new Entry<TKey, TValue>[ENTRY_SIZE];
            nextEntryInsertIndex = 0;
            freeList = -1;
            freeCount = 0;
        }

        public void Add(TKey key, TValue value)
        {
            var bucketIndex = CalculateBucketIndex(key);
            var entryIndex = buckets[bucketIndex];

            //Try update value first

            for (int i = entryIndex; i >= 0; i = entries[i].NextEntryIndex)
            {
                if (entries[i].HashCode == key.GetHashCode() 
                    && entries[i].Key.CompareTo(key) == 0)
                {
                    entries[i].Value = value;
                    return;
                }
            }

            int insertIndex = -1;

            // check if Free holes in Entires available

            if (freeCount > 0)
            {
                insertIndex = freeList;
                freeList = entries[freeList].NextEntryIndex;
                freeCount--;
            }
            else
            {
                // No previous match, need to add new item to Entries
                if (nextEntryInsertIndex >= entries.Length)
                {
                    ResizeEntries();
                }
                insertIndex = nextEntryInsertIndex;
                nextEntryInsertIndex++;
            }

            var entryToAdd = new Entry<TKey, TValue>
            {
                Key = key,
                Value = value,
                HashCode = key.GetHashCode(),
                NextEntryIndex = buckets[bucketIndex]
            };

            entries[insertIndex] = entryToAdd;
            buckets[bucketIndex] = insertIndex;
            
        }

        public bool HasKey(TKey key)
        {
            var bucketIndex = CalculateBucketIndex(key);
            var entryIndex = buckets[bucketIndex];

            for (int i = entryIndex; i >= 0; i = entries[i].NextEntryIndex)
            {
                if (entries[i].HashCode == key.GetHashCode()
                    && entries[i].Key.CompareTo(key) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        private void ResizeEntries()
        {
            var resizeLength = (GROWTH_FACTOR * entries.Length) + entries.Length;
            Array.Resize(ref entries, (int)resizeLength);
        }

        private int CalculateBucketIndex(TKey key)
        {
            var nonNegativeHashCode= key.GetHashCode() & int.MaxValue;
            var bucketsArrayIndex = nonNegativeHashCode % buckets.Length;
            return bucketsArrayIndex;
        }

        public void Remove(TKey key)
        {
            var bucketIndex = CalculateBucketIndex(key);
            var entryIndex = buckets[bucketIndex];
            var foundAtIndex = entryIndex;

            Entry<TKey, TValue> entryMatch = null;
            Entry<TKey, TValue> previousEntryMatch = entries[entryIndex];

            for (; foundAtIndex >= 0; foundAtIndex = entries[foundAtIndex].NextEntryIndex)
            {
                if (entries[foundAtIndex].HashCode == key.GetHashCode() && entries[foundAtIndex].Key.CompareTo(key) == 0)
                {
                    entryMatch = entries[foundAtIndex];
                    break;
                }

                previousEntryMatch = entries[foundAtIndex];
            }

            // Did not find anything so return
            if (entryMatch == null)
            {
                throw new Exception($"Key:{key} does not exist");
            }

            //Remove from Link first

            if (foundAtIndex == entryIndex)// Index is same as the very first pointed to in the bucket, hence update bucket index direct
            {
                buckets[bucketIndex] = entryMatch.NextEntryIndex;
            }

            if (entryMatch != previousEntryMatch) // otherwise this condition should hold where its some child down the list in the chain, hence update parent Next Index
            {
                previousEntryMatch.NextEntryIndex = entryMatch.NextEntryIndex;
            }

            entryMatch.NextEntryIndex = -1; // de-link any child entries entry


            
            if (freeList == -1) // if the freeList is unassigned, then entry to remove becomes first index pointed to
            {
                freeList = foundAtIndex;
            }
            else // otherwise iterate through to find where the last in the chain is pointed to
            {
                var updateIndex = freeList;

                while (entries[updateIndex].NextEntryIndex > -1)
                {
                    updateIndex = entries[updateIndex].NextEntryIndex;
                }

                entries[updateIndex].NextEntryIndex = foundAtIndex;
            }

            freeCount++; //Increment the free count
        }
    }

    public class Entry<TKey, TValue> where TKey : IComparable<TKey>
    {
        public TKey Key;
        public TValue Value;
        public int HashCode;
        public int NextEntryIndex = -1;
    }
}