namespace MedianOfTwoSortedArray
{
    internal class Program
    {
        //Probem Link: https://leetcode.com/problems/median-of-two-sorted-arrays
        static void Main(string[] args)
        {
            Console.WriteLine("Testing Median of Two Sorted Arrays Solutions:");

            var testCases = new List<(int[] nums1, int[] nums2, double expected)>
            {
                (new int[] { 1, 3 }, new int[] { 2 }, 2.0),
                (new int[] { 1, 2 }, new int[] { 3, 4 }, 2.5),
                (new int[] { 0, 0 }, new int[] { 0, 0 }, 0.0),
                (new int[] { }, new int[] { 1 }, 1.0),
                (new int[] { 2 }, new int[] { }, 2.0),
                (new int[] { 1, 3, 8 }, new int[] { 7, 9, 10, 11 }, 8.0),
                (new int[] { -5, -3, -1 }, new int[] { -4, -2, 0 }, -2.5)
            };

            var program = new Program();

            foreach (var (nums1, nums2, expected) in testCases)
            {
                Console.WriteLine($"Test Case: nums1 = [{string.Join(", ", nums1)}], nums2 = [{string.Join(", ", nums2)}]");
                Console.WriteLine($"Expected Median: {expected}");

                // Brute Force Solution
                double bruteForceResult = program.FindMedianSortedArrays(nums1, nums2);
                Console.WriteLine($"Brute Force Result: {bruteForceResult} - {(Math.Abs(bruteForceResult - expected) < 1e-6 ? "PASS" : "FAIL")}");

                // Optimized Solution
                double optimizedResult = program.FindMedianSortedArrays1(nums1, nums2);
                Console.WriteLine($"Optimized Result: {optimizedResult} - {(Math.Abs(optimizedResult - expected) < 1e-6 ? "PASS" : "FAIL")}");

                Console.WriteLine();
            }
        }

        //brute force
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            int n = nums1.Length;
            int m = nums2.Length;
            //int[] merged = new int[m + n]() ;
            List<int> merged = new List<int>();
            int i = 0;
            int j = 0;
            while (i < n && j < m)
            {
                if (nums1[i] < nums2[j])
                {
                    merged.Add(nums1[i]);
                    i++;
                }
                else
                {
                    merged.Add(nums2[j]);
                    j++;
                }
            }
            while (i < n)
            {
                merged.Add(nums1[i]);
                i++;
            }
            while (j < m)
            {
                merged.Add(nums2[j]);
                j++;
            }

            var mergedArray = merged.ToArray();
            if ((n + m) % 2 == 1)
            {
                return (double)mergedArray[(n + m) / 2];
            }
            else
            {
                return (double)(mergedArray[(n + m) / 2] + mergedArray[((n + m) / 2) - 1]) / 2;
            }
        }

        //Optimal approach
        public double FindMedianSortedArrays1(int[] nums1, int[] nums2)
        {

            if (nums1.Length > nums2.Length)
            {
                return FindMedianSortedArrays1(nums2, nums1);
            }

            int m = nums1.Length;
            int n = nums2.Length;
            int left = 0;
            int right = m;

            while (left <= right)
            {
                int partition1 = (left + right) / 2;
                int partition2 = (m + n + 1) / 2 - partition1;

                int maxLeft1 = (partition1 == 0) ? int.MinValue : nums1[partition1 - 1];
                int minRight1 = (partition1 == m) ? int.MaxValue : nums1[partition1];

                int maxLeft2 = (partition2 == 0) ? int.MinValue : nums2[partition2 - 1];
                int minRight2 = (partition2 == n) ? int.MaxValue : nums2[partition2];

                if (maxLeft1 <= minRight2 && maxLeft2 <= minRight1)
                {
                    if ((m + n) % 2 == 0)
                    {
                        return (Math.Max(maxLeft1, maxLeft2) + Math.Min(minRight1, minRight2)) / 2.0;
                    }
                    else
                    {
                        return Math.Max(maxLeft1, maxLeft2);
                    }
                }
                else if (maxLeft1 > minRight2)
                {
                    right = partition1 - 1;
                }
                else
                {
                    left = partition1 + 1;
                }

            }
            return 0;
        }
    }
}
