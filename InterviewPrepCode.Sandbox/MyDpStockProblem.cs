using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace InterviewPrepCode.Sandbox
{
    public class MyDpStockProblem
    {
        private ITestOutputHelper _outputHelper;

        public MyDpStockProblem(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void DoTest()
        {
            int[] stockTicker = { 100, 180, 260, 310, 40, 535, 695 };

            //int[] stockTicker = {1,2,3,4};

            var maxProfit = CalculateMaxProfit(stockTicker, 
                stockTicker.GetLowerBound(0), 
                stockTicker.GetUpperBound(0));

        }

        private int CalculateMaxProfit(int[] stockTicker, int startDay, int endDay)
        {
            if (startDay >= endDay)
            {
                return 0;
            }

            var profit = 0;

            for (int i = startDay; i < endDay -1; i++)
            {
                for (int j = i+1; j <= endDay; j++)
                {

                    if (stockTicker[i] < stockTicker[j])
                    {
                        var currentSellProfit = stockTicker[j] - stockTicker[i];
                        var maxFromTradeInPast = CalculateMaxProfit(stockTicker, startDay, i - 1);
                        var maxFromTradeInFuture = CalculateMaxProfit(stockTicker, j+1, endDay);

                        var currentProfit = maxFromTradeInPast + currentSellProfit + maxFromTradeInFuture;

                        profit = Math.Max(profit, currentProfit);
                    }
                }
            }

            return profit;
        }
    }
}