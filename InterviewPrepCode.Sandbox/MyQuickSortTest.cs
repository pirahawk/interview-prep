using Xunit;

namespace InterviewPrepCode.Sandbox
{
    public class MyQuickSortTest
    {
        [Fact]
        public void CanSort()
        {
            int[] input = new[] {4, 3, 5, 1, 2, 6};
            
            QuickSort(input, input.GetLowerBound(0), input.GetUpperBound(0));
            
            Assert.Equal(new[]{ 1,2,3,4,5,6}, input);
        }

        [Fact]
        public void CanPartiion()
        {
            int[] input = new[] { 4, 1, 2 };

            var result = Partition(input, 0, 2);

            Assert.Equal(1, result);
            Assert.Equal(new[] { 1,2,4}, input);
        }

        private void QuickSort(int[] input, int p, int r)
        {
            if (p >= r)
            {
                return;
            }

            int q = Partition(input, p, r);
            QuickSort(input, p, q-1);
            QuickSort(input, q+1, r);

        }

        int Partition(int[] arr, int p, int r)
        {
            var key = arr[r];
            int i = p - 1;

            for (int j = p; j < r; j++)
            {
                if (arr[j] < key)
                {
                    i++;
                    var tmp = arr[j];
                    arr[j] = arr[i];
                    arr[i] = tmp;
                }
            }

            var tmp1 = arr[i + 1];
            arr[i + 1] = key;
            arr[r] = tmp1;

            return i + 1;
        }
    }
}