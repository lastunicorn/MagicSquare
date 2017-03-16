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
        private readonly int n;
        private readonly Matrix grid;
        private readonly Tokens numbers;

        public event EventHandler<SolutionFoundEventArgs> SolutionFound;

        public MagicSquare(int n)
        {
            if (n < 3) throw new ArgumentOutOfRangeException(nameof(n));

            this.n = n;

            grid = new Matrix(n);
            numbers = new Tokens(1, n * n);
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

                bool nextStepSuccess = Increment(n * n);

                if (!nextStepSuccess)
                    break;
            }
        }

        private void Initialize()
        {
            numbers.Clear();
            grid.Clear();
            Increment(1);
        }

        private bool Validate()
        {
            int sum = grid.D1Sum;

            for (int i = 1; i <= n; i++)
                if (grid.GetRowSum(i) != sum)
                    return false;

            for (int i = 1; i <= n; i++)
                if (grid.GetColumnSum(i) != sum)
                    return false;

            return grid.D2Sum == sum;
        }

        private bool Increment(int index)
        {
            while (true)
            {
                // If reached the beggining of the matrix -> matrix cannot be filled.
                if (index == 0)
                    return false;

                // If reached the end of the matrix -> matrix is full.
                if (index == n * n)
                    return true;

                // If 2 rows are full and their sum doeas not match, go back one step.
                if (index == 2 * n + 1 && grid.GetRowSum(1) != grid.GetRowSum(2))
                    index--;

                //Console.WriteLine("-----------------------------------");
                //Console.WriteLine("index = " + index);
                //Console.WriteLine();
                //Console.WriteLine(grid.ToString());

                int currentNumber = grid.Get(index);
                numbers.Free(currentNumber);

                // Get the next number that can be used.

                int? nextNumber = numbers.ObtainNext(currentNumber + 1);

                if (nextNumber.HasValue)
                {
                    grid.Set(index, nextNumber.Value);
                    //Console.WriteLine(grid.ToString());

                    index++;
                }
                else
                {
                    grid.Set(index, 0);
                    //Console.WriteLine(grid.ToString());

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
