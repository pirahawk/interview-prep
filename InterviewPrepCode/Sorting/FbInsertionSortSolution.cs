using System;
using NUnit.Framework;

namespace InterviewPrepCode.Sorting
{
    class FbInsertionSortSolution
    {
        [TestFixture]
        class FbInsertionSortTest
        {

            [Test]
            public void CanSort()
            {
                var arr = new[] { 5, 4, 3, 2, 1, 0 };

                FbInsertionSort.Sort(arr);

                Assert.That(arr[0], Is.EqualTo(0));
                Assert.That(arr[2], Is.EqualTo(2));
                Assert.That(arr[5], Is.EqualTo(5));

            }
        }

        class FbInsertionSort
        {

            public static void Sort<TVal>(TVal[] arr) where TVal : IComparable<TVal>
            {
                if (arr.Length < 2)
                {
                    return;
                }

                for (int i = 1; i < arr.Length; i++)
                {
                    var key = arr[i];
                    int j = i - 1;

                    for (; j >= 0; j--)
                    {

                        if (arr[j].CompareTo(key) < 0)
                        {
                            break;
                        }

                        arr[j + 1] = arr[j];
                    }

                    arr[j + 1] = key;
                }
            }

        }
    }
}