// MagicSquare
// Copyright (C) 2017 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

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
