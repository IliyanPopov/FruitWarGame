namespace FruitWarGame.Models.Essential
{
    using System;
    using Common;
    using Contracts.Essential;
    using Contracts.Warriors;

    public class GameGrid : IGameGrid
    {

        private readonly char[,] _grid;

        public GameGrid(int rows = GlobalConstants.GameGridRowsCount, int cols = GlobalConstants.GameGridColsCount)
        {
            this.Rows = rows;
            this.Cols = cols;
            this._grid = new char[rows, cols];
        }

        public int Rows { get; }

        public int Cols { get; }

        public void PlaceWarrior(IWarrior warrior)
        {
            //int warriorRow = warrior.
            //int shipCol = ship.TopLeft.CurrentCol;
            //  this._grid[shipRow, shipCol] = ship.Image;
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
            if (row < 0 || row > GlobalConstants.GameGridRowsCount - 1)
            {
                throw new IndexOutOfRangeException($"Invalid row: {row}");
            }
            if (col < 0 || col > GlobalConstants.GameGridRowsCount - 1)
            {
                throw new IndexOutOfRangeException($"Invalid col: {col}");
            }
        }
    }
}