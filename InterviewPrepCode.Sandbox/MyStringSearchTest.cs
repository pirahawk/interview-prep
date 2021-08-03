using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace InterviewPrepCode.Sandbox
{
    public class MyStringSearchTest
    {
        [Fact]
        void CanFind()
        {
            var str = "Extension methods have all the capabilities of regular static methods. boo";
            //var result = Regex.Matches(str, @"\bm\w.+\b");
            var expect = str.IndexOf("boo");
            var result = FindString(str, "boo").ToArray();
        }

        IEnumerable<int> FindString(string str, string arrToFind)
        {
            var strUpperBound = str.ToCharArray().GetUpperBound(0);
            var firstChar = arrToFind[0];

            for (int i = 0; i < str.ToCharArray().GetUpperBound(0); i++)
            {
                var currentChar = str[i];

                if (currentChar == firstChar)
                {
                    var j = i;
                    var isMatch = false;

                    for (int k = 0; k < arrToFind.Length; k++)
                    {
                        if (j + k > strUpperBound
                        || !str[j+k].Equals(arrToFind[k])
                        )
                        {
                            isMatch = false;
                            break;
                        }
                        isMatch = true;
                    }

                    if (isMatch)
                    {
                        yield return i;
                    }
                }
            }

        }
    }
}