using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DustInTheWind.MagicSquare
{
    internal class SolutionFile : IDisposable
    {
        private readonly StreamWriter streamWriter;

        private readonly List<int[,]> solutions = new List<int[,]>();

        public SolutionFile()
        {
            string fileName = $"solutions - {DateTime.Now:yyyy MM dd HHmmss}.txt";
            streamWriter = new StreamWriter(fileName);
        }

        public void AddSolution(Matrix grid)
        {
            lock (solutions)
                solutions.Add(grid.ToArray());
        }

        public void Flush()
        {
            foreach (int[,] solution in solutions)
            {
                streamWriter.WriteLine(ToString(solution));
                streamWriter.WriteLine();
            }

            streamWriter.Flush();
        }

        public void Dispose()
        {
            streamWriter.Dispose();
        }

        public string ToString(int[,] grid)
        {
            StringBuilder sb = new StringBuilder();

            int rowCount = grid.GetLength(0);
            int columnCount = grid.GetLength(1);

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    sb.Append(grid[i, j]);

                    if (j < columnCount - 1)
                        sb.Append(" ");
                }

                if (i < columnCount - 1)
                    sb.AppendLine();
            }

            return sb.ToString();
        }

        public void WriteStatistics(int solutionCount, TimeSpan totalTime)
        {
            streamWriter.WriteLine("Solution count: " + solutionCount);
            streamWriter.WriteLine("Time: " + totalTime);
        }
    }
}