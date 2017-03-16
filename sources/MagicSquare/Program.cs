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

            using (SolutionFile solutionFile = new SolutionFile())
            {
                MagicSquare magicSquare = new MagicSquare(4);

                magicSquare.SolutionFound += (sender, e) =>
                {
                    solutionCount++;

                    Console.WriteLine(e.Solution.ToString());
                    Console.WriteLine();

                    solutionFile.DisplaySolution(e.Solution);
                };

                magicSquare.Calculate();

                solutionFile.WriteStatistics(solutionCount, stopWatch.Elapsed);
            }

            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
