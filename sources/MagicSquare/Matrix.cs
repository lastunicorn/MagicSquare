using System;
using System.Text;

namespace DustInTheWind.MagicSquare
{
    /// <summary>
    /// Represents a matrix of n x n that also calculates the sum of the elements on each row, column and the two diagonals.
    /// Automatically calculates the row sums when the elements are changed.
    /// Needs a call to <see cref="CalculateSums"/> to calculate the other sums.
    /// </summary>
    internal class Matrix
    {
        private readonly int n;
        private readonly int[,] grid;

        private readonly int[] rowSums;
        private readonly int[] columnSums;

        public int D1Sum { get; private set; }
        public int D2Sum { get; private set; }

        public Matrix(int n)
        {
            if (n < 3) throw new ArgumentOutOfRangeException(nameof(n));

            this.n = n;

            grid = new int[n, n];

            rowSums = new int[n];
            columnSums = new int[n];
        }

        public void Set(int index, int value)
        {
            int rowIndex = (index - 1) / n;
            int columnIndex = (index - 1) % n;

            int oldValue = grid[rowIndex, columnIndex];
            
            rowSums[rowIndex] = rowSums[rowIndex] - oldValue + value;
            columnSums[columnIndex] = columnSums[columnIndex] - oldValue + value;

            if (rowIndex == columnIndex)
                D1Sum = D1Sum - oldValue + value;

            if (rowIndex + columnIndex == n - 1)
                D2Sum = D2Sum - oldValue + value;

            grid[rowIndex, columnIndex] = value;
        }

        public int Get(int index)
        {
            int rowIndex = (index - 1) / n;
            int columnIndex = (index - 1) % n;

            return grid[rowIndex, columnIndex];
        }

        public void Clear()
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    grid[i, j] = 0;

            for (int i = 0; i < rowSums.Length; i++)
                rowSums[i] = 0;

            for (int i = 0; i < columnSums.Length; i++)
                columnSums[i] = 0;

            D1Sum = 0;
            D2Sum = 0;
        }
        
        //public void CalculateSums()
        //{
        //    for (int i = 0; i < rowSums.Length; i++)
        //        rowSums[i] = 0;

        //    for (int i = 0; i < columnSums.Length; i++)
        //        columnSums[i] = 0;

        //    D1Sum = 0;
        //    D2Sum = 0;

        //    for (int i = 0; i < n; i++)
        //    {
        //        for (int j = 0; j < n; j++)
        //        {
        //            int value = grid[i, j];

        //            rowSums[i] += value;
        //            columnSums[j] += value;

        //            if (i == j)
        //                D1Sum += value;

        //            if (i + j == n - 1)
        //                D2Sum += value;
        //        }
        //    }
        //}

        public int GetRowSum(int rowIndex)
        {
            return rowSums[rowIndex - 1];
        }

        public int GetColumnSum(int columnIndex)
        {
            return columnSums[columnIndex - 1];
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    sb.Append(grid[i, j]);

                    if (j < n - 1)
                        sb.Append(" ");
                }

                if (i < n - 1)
                    sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}