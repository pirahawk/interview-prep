using System;
using NUnit.Framework;
using NUnitLite;

namespace InterviewPrepCode.DynamicProgramming
{

    /// <summary>
    /// https://gist.githubusercontent.com/p-van/f7a5ec230c8f894bf5f84f4735b212ff/raw/92db6e3ec3c81582b34584c964cc3b94d019f800/gistfile1.txt
    /// </summary>
    ///
    
    public class StockProblem
    {
        public void DoTest()
        {
            int[] price = { 100, 180, 260, 310, 40, 535, 695 };
            //int[] price = {100, 180};

            int prof = Profit(price);
            Console.WriteLine($"MaxProfit {prof}");
        }

        public int Profit(int[] price)
        {
            return MaxProfit(price, 0, price.Length - 1);
        }

        private int MaxProfit(int[] price, int startDay, int endDay)
        {
            if (startDay >= endDay)
            {
                return 0;
            }

            int profit = 0;

            for (int i = startDay; i < endDay; i++)
            {

                for (int j = i + 1; j <= endDay; j++)
                {

                    if (price[i] < price[j])
                    {
                        var sellProfit = price[j] - price[i];
                        var profitFromTradeInPast = MaxProfit(price, startDay, i - 1);
                        var profitFromTradeInFuture = MaxProfit(price, j + 1, endDay);
                        var currentProfitForcast = profitFromTradeInPast + sellProfit + profitFromTradeInFuture;
                        profit = Math.Max(profit, currentProfitForcast);
                    }
                }
            }

            return profit;
        }
    }
}