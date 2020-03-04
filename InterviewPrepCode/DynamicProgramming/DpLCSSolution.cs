using System;
using NUnit.Framework;
using NUnitLite;

namespace InterviewPrepCode.DynamicProgramming
{
    class DpLCSSolution
    {

        [TestFixture]
        class DpLCS
        {

            [Test]
            public void PartitionIndexCorrectly()
            {

                var S = "ABAZDC";
                var T = "BACBAD";

                Longest(S, T, 5, 5);

            }


            int Longest(string s, string t, int indexS, int indexT)
            {

                if (indexS == -1 || indexT == -1)
                {
                    Console.WriteLine($"[{indexS},{indexT}] Zero");
                    return 0;
                }

                if (indexS < s.Length && indexT < t.Length && s[indexS] == t[indexT])
                {
                    var prevLongest = Longest(s, t, indexS - 1, indexT - 1);
                    var longest = 1 + prevLongest;

                    Console.WriteLine($"[ {indexS}, {indexT}] Match -> 1 + {prevLongest}");
                    return longest;
                }

                var max1 = Longest(s, t, indexS - 1, indexT);
                var max2 = Longest(s, t, indexS, indexT - 1);

                var max = Math.Max(max1, max2);

                Console.WriteLine($"[ {indexS}, {indexT}] NoMatch -> Max: [S: {indexS - 1} T: {indexT}] Vs [S: {indexS} T: {indexT - 1}] = {max1} vs {max2} = {max}");
                return max;
            }
        }


    }
}