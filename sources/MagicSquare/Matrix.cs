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
        public int N { get; }
        private readonly int[,] grid;

        private readonly int[] rowSums;
        private readonly int[] columnSums;

        public int D1Sum { get; private set; }
        public int D2Sum { get; private set; }

        public Matrix(int n)
        {
            if (n < 3) throw new ArgumentOutOfRangeException(nameof(n));

            N = n;

            grid = new int[n, n];

            rowSums = new int[n];
            columnSums = new int[n];
        }

        public void Set(int index, int value)
        {
            int rowIndex = (index - 1) / N;
            int columnIndex = (index - 1) % N;

            int oldValue = grid[rowIndex, columnIndex];

            rowSums[rowIndex] = rowSums[rowIndex] - oldValue + value;
            columnSums[columnIndex] = columnSums[columnIndex] - oldValue + value;

            if (rowIndex == columnIndex)
                D1Sum = D1Sum - oldValue + value;

            if (rowIndex + columnIndex == N - 1)
                D2Sum = D2Sum - oldValue + value;

            grid[rowIndex, columnIndex] = value;
        }

        public int Get(int index)
        {
            int rowIndex = (index - 1) / N;
            int columnIndex = (index - 1) % N;

            return grid[rowIndex, columnIndex];
        }

        public void Clear()
        {
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
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

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    sb.Append(grid[i, j]);

                    if (j < N - 1)
                        sb.Append(" ");
                }

                if (i < N - 1)
                    sb.AppendLine();
            }

            return sb.ToString();
        }

        public void Initialize(int[] initialValues)
        {
            for (int i = 0; i < initialValues.Length; i++)
                Set(i + 1, initialValues[i]);
        }

        public int[,] ToArray()
        {
            int[,] array = new int[N, N];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                    array[i, j] = grid[i, j];
            }

            return array;
        }
    }
}