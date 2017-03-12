using System;

namespace DustInTheWind.MagicSquare
{
    internal class SolutionFoundEventArgs : EventArgs
    {
        public int[,] Solution { get; }

        public SolutionFoundEventArgs(int[,] solution)
        {
            if (solution == null) throw new ArgumentNullException(nameof(solution));

            Solution = solution;
        }
    }
}