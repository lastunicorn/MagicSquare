using System;
using System.Diagnostics;

namespace DustInTheWind.MagicSquare
{
    internal static class Program
    {
        private static void Main()
        {
            int solutionCount = 0;
            Stopwatch stopWatch = Stopwatch.StartNew();

            MagicSquare magicSquare = new MagicSquare(3);

            magicSquare.SolutionFound += (sender, e) =>
            {
                solutionCount++;
                DisplaySolution(e.Solution);
            };

            magicSquare.Calculate();

            Console.WriteLine("Solution count: " + solutionCount);
            Console.WriteLine("Time: " + stopWatch.Elapsed);

            Console.ReadKey(true);
        }

        private static void DisplaySolution(int[,] grid)
        {
            int rowCount = grid.GetLength(0);
            int columnCount = grid.GetLength(1);

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    Console.Write(grid[i, j]);

                    if (j < columnCount - 1)
                        Console.Write(" ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
