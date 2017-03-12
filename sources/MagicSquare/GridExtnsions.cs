namespace MagicSquare
{
    public static class GridExtnsions
    {
        public static int GetSnakeValue(this int[,] grid, int index)
        {
            int columnCount = grid.GetLength(1);

            int line = index / columnCount;
            int column = index % columnCount;

            return grid[line, column];
        }

        public static void SetSnakeValue(this int[,] grid, int index, int value)
        {
            int columnCount = grid.GetLength(1);

            int line = index / columnCount;
            int column = index % columnCount;

            grid[line, column] = value;
        }
    }
}