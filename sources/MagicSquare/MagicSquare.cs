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

namespace DustInTheWind.MagicSquare
{
    internal sealed class MagicSquare
    {
        private readonly int[] initialValues;
        private readonly int offset;
        private readonly Matrix matrix;
        private readonly Tokens numbers;
        private readonly int targetSum;

        public event EventHandler<SolutionFoundEventArgs> SolutionFound;

        public MagicSquare(int n)
        {
            if (n < 3) throw new ArgumentOutOfRangeException(nameof(n));

            matrix = new Matrix(n);
            numbers = new Tokens(1, n * n);

            targetSum = (n * n + 1) * n / 2;
        }

        public MagicSquare(int n, int[] initialValues, int offset)
        {
            if (n < 3) throw new ArgumentOutOfRangeException(nameof(n));
            if (initialValues == null) throw new ArgumentNullException(nameof(initialValues));
            if (initialValues.Length != n * n) throw new ArgumentException("Initial values array must be of length " + n, nameof(initialValues));
            if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));

            this.initialValues = initialValues;
            this.offset = offset;

            matrix = new Matrix(n);
            numbers = new Tokens(1, n * n);
            targetSum = (n * n + 1) * n / 2;
        }

        public void Calculate()
        {
            Initialize();

            while (true)
            {
                bool isValid = Validate();

                if (isValid)
                {
                    SolutionFoundEventArgs eva = new SolutionFoundEventArgs(matrix);
                    OnSolutionFound(eva);
                }

                bool nextStepSuccess = Increment(matrix.N * matrix.N);

                if (!nextStepSuccess)
                    break;
            }
        }

        private void Initialize()
        {
            numbers.Clear();
            matrix.Clear();

            if (initialValues != null)
                matrix.Initialize(initialValues);

            Increment(offset + 1);
        }

        private bool Validate()
        {
            //matrix.CalculateSums();

            int sum = matrix.D1Sum;

            for (int i = 1; i <= matrix.N; i++)
                if (matrix.GetRowSum(i) != sum)
                    return false;

            for (int i = 1; i <= matrix.N; i++)
                if (matrix.GetColumnSum(i) != sum)
                    return false;

            return matrix.D2Sum == sum;
        }

        private bool Increment(int index)
        {
            while (true)
            {
                // If reached the beggining of the matrix -> matrix cannot be filled.
                if (index == offset)
                    return false;

                // If reached the end of the matrix -> matrix is full.
                if (index == matrix.N * matrix.N + 1)
                    return true;

                // If 2 rows are full and their sum doeas not match, go back one step.
                if (index > 1 && (index - 1) % matrix.N == 0 && matrix.GetRowSum((index - 1) / matrix.N) != targetSum)
                    index--;

                // Release the current number.

                int currentNumber = matrix.Get(index);
                numbers.Free(currentNumber);

                // Get the next number that can be used.

                int? nextNumber = numbers.ObtainNext(currentNumber + 1);

                if (nextNumber.HasValue)
                {
                    matrix.Set(index, nextNumber.Value);
                    index++;
                }
                else
                {
                    matrix.Set(index, 0);
                    index--;
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
