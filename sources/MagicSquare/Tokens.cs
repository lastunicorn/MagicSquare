using System;

namespace DustInTheWind.MagicSquare
{
    internal class Tokens
    {
        private readonly int start;
        private readonly int length;
        private readonly bool[] slots;

        public Tokens(int start, int length)
        {
            if (length <= 0) throw new ArgumentOutOfRangeException(nameof(length));

            this.start = start;
            this.length = length;
            slots = new bool[length];
        }

        public bool Free(int token)
        {
            if (token < start || token > length)
                return false;

            slots[token - start] = false;
            return true;
        }

        public bool Obtain(int token)
        {
            int index = token - start;

            if (slots[index])
                return false;

            slots[index] = true;
            return true;
        }

        public int? ObtainNext(int startToken)
        {
            int currentIndex = startToken - start - 1;

            while (true)
            {
                currentIndex++;

                if (currentIndex >= slots.Length)
                    return null;

                if (slots[currentIndex])
                    continue;

                slots[currentIndex] = true;
                return currentIndex + start;
            }
        }

        public void Clear()
        {
            for (int i = 0; i < slots.Length; i++)
                slots[i] = false;
        }
    }
}