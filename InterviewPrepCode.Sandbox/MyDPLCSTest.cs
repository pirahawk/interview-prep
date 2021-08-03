using System;
using Xunit;

namespace InterviewPrepCode.Sandbox
{
    public class MyDPLCSTest
    {
        [Fact]
        void CanLCS()
        {
            var S = "ABAZDC";
            var T = "BACBAD";

            var result = LCS(S, T, 5, 5);
        }

        int LCS(string S, string T, int i, int j)
        {
            if (i < 0 || j < 0)
            {
                return 0;
            }

            if (S[i] == T[j])
            {
                return 1 + LCS(S,T, i-1, j-1);
            }
            else
            {
                return Math.Max(LCS(S, T, i - 1, j), LCS(S, T, i, j - 1));
            }
        }
    }
}