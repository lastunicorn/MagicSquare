﻿using System;
using System.Linq;

namespace MagicSquare
{
    internal sealed class MagicSquare
    {
        private readonly int n;
        private readonly int[,] grid;
        private readonly bool[] numbers;

        public event EventHandler<SolutionFoundEventArgs> SolutionFound;

        public MagicSquare(int n)
        {
            if (n <= 0) throw new ArgumentOutOfRangeException(nameof(n));

            this.n = n;

            grid = new int[n, n];
            numbers = new bool[n * n];
        }

        public void Calculate()
        {
            Initialize();

            while (true)
            {
                bool isValid = Validate();

                if (isValid)
                {
                    SolutionFoundEventArgs eva = new SolutionFoundEventArgs(grid);
                    OnSolutionFound(eva);
                }

                bool nextStepSuccess = NextStep();

                if (!nextStepSuccess)
                    break;
            }
        }

        private void Initialize()
        {
            for (int i = 0; i < numbers.Length; i++)
                numbers[i] = false;

            FillGrid();
            Increment(0);
        }

        private void FillGrid()
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    grid[i, j] = 0;
        }

        private bool Validate()
        {
            int rowCount = grid.GetLength(0);
            int columnCount = grid.GetLength(1);

            int[] rowSums = new int[rowCount];
            int[] columnSums = new int[columnCount];
            int d1Sum = 0;
            int d2Sum = 0;

            for (int i = 0; i < rowCount; i++)
                for (int j = 0; j < columnCount; j++)
                {
                    int value = grid[i, j];

                    rowSums[i] += value;
                    columnSums[j] += value;

                    if (i == j)
                        d1Sum += value;

                    if (i + j == columnCount - 1)
                        d2Sum += value;
                }

            return d2Sum == d1Sum && rowSums.All(x => x == d1Sum) && columnSums.All(x => x == d1Sum);
        }

        private bool NextStep()
        {
            for (int i = n * n - 1; i >= 0; i--)
            {
                bool success = Increment(i);

                if (success)
                    return true;
            }

            return false;
        }

        private bool Increment(int i)
        {
            int currentNumber = grid.GetSnakeValue(i);
            int currentNumberIndex = currentNumber - 1;

            if (currentNumberIndex >= 0)
                numbers[currentNumberIndex] = false;

            while (true)
            {
                currentNumberIndex++;

                if (currentNumberIndex >= numbers.Length)
                {
                    grid.SetSnakeValue(i, 0);
                    return false;
                }

                if (!numbers[currentNumberIndex])
                {
                    numbers[currentNumberIndex] = true;
                    currentNumber = currentNumberIndex + 1;

                    grid.SetSnakeValue(i, currentNumber);

                    if (i + 1 == n * n)
                        return true;

                    return Increment(i + 1);
                }
            }
        }

        private void OnSolutionFound(SolutionFoundEventArgs e)
        {
            EventHandler<SolutionFoundEventArgs> eventHandler = SolutionFound;

            eventHandler?.Invoke(this, e);
        }
    }
}