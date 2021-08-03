using System;
using Xunit;
using Xunit.Abstractions;

namespace InterviewPrepCode.Sandbox
{
    public class MyDPMatrixNChainTest
    {
        private ITestOutputHelper output;

        public MyDPMatrixNChainTest(ITestOutputHelper output)
        {
            this.output = output;
        }


        [Fact]
        public void CanFindDoor()
        {
            var maze = new int[,]
            {
                {0,0,1},
                {1,0,1},
                {1,1,0}
            };


            var result = CanExitMaze(maze, true, 0, 0);

            Assert.True(result);
        }

        bool CanExitMaze(int[,] maze, bool hasKey, int y, int x)
        {
            //output.WriteLine($"Visiting ->  x:{x} y:{y} hasKey:{hasKey}");

            if (IsOutOfBounds())
            {
                //output.WriteLine($"Can't continue-> OutOfBounds:{IsOutOfBounds()}");
                return false;
            }

            if ((IsLocked() && !hasKey))
            {
                //output.WriteLine($"Can't continue-> LockedWithNoKey: {IsLocked() && !hasKey}");
                return false;
            }

            if (IsEndPoint())
            {
                //output.WriteLine($"***** Endpoint Reached *********");
                return true;
            }

            hasKey = IsLocked() && hasKey? !hasKey : hasKey;

            //output.WriteLine($"Exploring NextState");

            return
                (IsLocked() && (CanExitMaze(maze, hasKey,  y, x + 1) || CanExitMaze(maze, hasKey,  y + 1, x)))
                || (CanExitMaze(maze, hasKey, y, x + 1) || CanExitMaze(maze, hasKey, y + 1, x));


            bool IsLocked()
            {
                try
                {
                    return maze[y, x] == 1;
                }
                catch (Exception e)
                {
                    throw;
                }
            }

            bool IsOutOfBounds()
            {
                var endXIndex = maze.GetUpperBound(1);
                var endYIndex = maze.GetUpperBound(0);

                return x > endXIndex || y > endYIndex;
            }

            bool IsEndPoint()
            {
                var endXIndex = maze.GetUpperBound(1);
                var endYIndex = maze.GetUpperBound(0);

                return x == endXIndex && y == endYIndex;
            }
        }
    }
}