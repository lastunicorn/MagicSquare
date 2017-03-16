using System;
using System.Text;

namespace DustInTheWind.MagicSquare
{
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

        public void Set(int rowIndex, int columnIndex, int value)
        {
            grid[rowIndex - 1, columnIndex - 1] = value;
        }

        public void Set(int index, int value)
        {
            int rowIndex = (index - 1) / N;
            int columnIndex = (index - 1) % N;

            int oldValue = grid[rowIndex, columnIndex];

            rowSums[rowIndex] -= oldValue;
            rowSums[rowIndex] += value;

            columnSums[columnIndex] -= oldValue;
            columnSums[columnIndex] += value;

            if (rowIndex == columnIndex)
            {
                D1Sum -= oldValue;
                D1Sum += value;
            }

            if (rowIndex + columnIndex == N - 1)
            {
                D2Sum -= oldValue;
                D2Sum += value;
            }

            grid[rowIndex, columnIndex] = value;
        }

        public int Get(int rowIndex, int columnIndex)
        {
            return grid[rowIndex - 1, columnIndex - 1];
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
    }
}