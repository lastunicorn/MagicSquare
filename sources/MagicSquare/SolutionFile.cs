using System;
using System.IO;

namespace DustInTheWind.MagicSquare
{
    internal class SolutionFile : IDisposable
    {
        private readonly StreamWriter streamWriter;

        public SolutionFile()
        {
            streamWriter = new StreamWriter("solutions.txt");
        }

        public void DisplaySolution(Matrix grid)
        {
            streamWriter.WriteLine(grid.ToString());
            //for (int i = 0; i < grid.N; i++)
            //{
            //    for (int j = 0; j < grid.N; j++)
            //    {
            //        streamWriter.Write(grid.Get(i, j));

            //        if (j < grid.N - 1)
            //            streamWriter.Write(" ");
            //    }

            //    streamWriter.WriteLine();
            //}

            streamWriter.WriteLine();
        }

        public void Dispose()
        {
            streamWriter.Dispose();
        }

        public void WriteStatistics(int solutionCount, TimeSpan totalTime)
        {
            streamWriter.WriteLine("Solution count: " + solutionCount);
            streamWriter.WriteLine("Time: " + totalTime);
        }
    }
}