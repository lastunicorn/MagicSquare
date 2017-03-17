using System;
using System.IO;

namespace DustInTheWind.MagicSquare
{
    internal class SolutionFile : IDisposable
    {
        private readonly StreamWriter streamWriter;

        public SolutionFile()
        {
            string fileName = $"solutions - {DateTime.Now:yyyy MM dd HHmmss}.txt";
            streamWriter = new StreamWriter(fileName);
        }

        public void DisplaySolution(Matrix grid)
        {
            streamWriter.WriteLine(grid.ToString());
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