namespace FruitWarGame.Models.Essential
{
    using System;
    using Contracts.Essential;
    using Contracts.Warriors;

    public class GameGrid : IGameGrid
    {
        private const int GameGridRowsCount = 5;
        private const int GameGridColsCount = 5;

        private readonly char[,] _grid;

        public GameGrid(int rows = GameGridRowsCount, int cols = GameGridColsCount)
        {
            this.Rows = rows;
            this.Cols = cols;
            this._grid = new char[rows, cols];
        }

        public int Rows { get; private set; }

        public int Cols { get; private set; }

        public void PlaceWarrior(IWarrior warrior)
        {
            //int shipRow = ship.TopLeft.CurrentRow;
            //int shipCol = ship.TopLeft.CurrentCol;
        }

        public char GetCell(IPosition position)
        {
            return this._grid[position.CurrentRow, position.CurrentCol];
        }

        public char GetCell(int row, int col)
        {
            return this._grid[row, col];
        }

        public void SetCell(IPosition position, char value)
        {
            this._grid[position.CurrentRow, position.CurrentCol] = value;
        }

        public void SetCell(int row, int col, char value)
        {
            ValidateCell(row, col);
            this._grid[row, col] = value;
        }

        private static void ValidateCell(int row, int col)
        {
            if (row < 0 || row > GameGridRowsCount - 1)
            {
                throw new IndexOutOfRangeException($"Invalid row: {row}");
            }
            if (col < 0 || col > GameGridColsCount - 1)
            {
                throw new IndexOutOfRangeException($"Invalid col: {col}");
            }
        }
    }
}