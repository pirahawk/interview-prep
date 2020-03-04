using System;
using NUnit.Framework;
using NUnitLite;

namespace InterviewPrepCode.DynamicProgramming
{
    class MazeNDoorsSolution
    {

        [TestFixture]
        class MazeNDoors
        {
            [Test]
            public void PartitionIndexCorrectly()
            {
                int[,] maze = new int[,] {
                    {0,0,1},
                    {1,0,1},
                    {1,1,0}
                };

                Assert.That(maze.GetLength(0), Is.EqualTo(3));
                Assert.That(maze.GetLength(1), Is.EqualTo(3));

                var result = FindPath(maze, true, 0, 0);

                Assert.True(result);
            }

            [Test]
            public void CanFindMemoized()
            {
                int[,] maze = new int[,] {
                    {0,0,1},
                    {1,0,1},
                    {1,1,0}
                };

                bool?[,] lockMap = new bool?[maze.GetLength(0), maze.GetLength(1)];


                var result = FindPathM(maze, lockMap, true, 0, 0);

                Assert.True(result);
            }

            public bool FindPath(int[,] maze, bool hasKey, int i, int j)
            {
                var dimX = maze.GetLength(0);
                var dimY = maze.GetLength(1);

                if (i >= dimX || j >= dimY)
                {
                    return false;
                }

                if (i == dimX - 1 && j == dimY - 1) //End it
                {
                    return maze[i, j] == 0 || hasKey;
                }

                if (maze[i, j] == 0)
                {
                    return FindPath(maze, hasKey, i + 1, j) || FindPath(maze, hasKey, i, j + 1);
                }

                else
                {
                    if (!hasKey)
                    {
                        return false;
                    }

                    return FindPath(maze, false, i + 1, j) || FindPath(maze, false, i, j + 1);
                }
            }

            public bool FindPathM(int[,] maze, bool?[,] lockMap, bool hasKey, int i, int j)
            {
                var dimX = maze.GetLength(0);
                var dimY = maze.GetLength(1);

                if (i >= dimX || j >= dimY)
                {
                    return false;
                }

                if (i == dimX - 1 && j == dimY - 1) //End it
                {
                    return maze[i, j] == 0 || hasKey;
                }

                if (!lockMap[i, j].HasValue)
                {
                    lockMap[i, j] = maze[i, j] == 1; //True if Locked
                }

                var isOpen = !lockMap[i, j].Value;

                if (isOpen)
                {
                    bool canIMoveRight = j + 1 < dimY && lockMap[i, j + 1].HasValue ? lockMap[i, j + 1].Value && !hasKey : true;
                    bool rightTest = canIMoveRight && FindPathM(maze, lockMap, hasKey, i, j + 1);

                    bool canIMoveDown = i + 1 < dimX && lockMap[i + 1, j].HasValue ? lockMap[i + 1, j].Value && !hasKey : true;
                    bool downTest = canIMoveDown && FindPathM(maze, lockMap, hasKey, i + 1, j);

                    return rightTest || downTest;
                }
                else
                {
                    if (!hasKey)
                    {
                        return false;
                    }

                    bool canIMoveRight = j + 1 < dimY && lockMap[i, j + 1].HasValue ? !lockMap[i, j + 1].Value : true;
                    bool rightTest = canIMoveRight && FindPathM(maze, lockMap, false, i, j + 1);

                    bool canIMoveDown = i + 1 < dimX && lockMap[i + 1, j].HasValue ? !lockMap[i + 1, j].Value : true;
                    bool downTest = canIMoveDown && FindPathM(maze, lockMap, false, i + 1, j);

                    return rightTest || downTest;
                }
            }

        }


    }
}