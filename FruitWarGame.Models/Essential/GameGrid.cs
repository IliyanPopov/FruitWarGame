namespace FruitWarGame.Models.Essential
{
    using System;
    using Common;
    using Contracts.Essential;
    using Contracts.Fruits;
    using Contracts.Warriors;

    public class GameGrid : IGameGrid
    {
        private readonly char[,] _grid;

        public GameGrid()
            : this(GlobalConstants.GameGridRowsCount, GlobalConstants.GameGridColsCount)
        {
        }

        protected GameGrid(int rows = GlobalConstants.GameGridRowsCount, int cols = GlobalConstants.GameGridColsCount)
        {
            this.Rows = rows;
            this.Cols = cols;
            this._grid = new char[rows, cols];
        }

        public int Rows { get; }

        public int Cols { get; }

        public char this[int xPosition, int yPosition]
        {
            get { return this._grid[xPosition, yPosition]; }

            set { this._grid[xPosition, yPosition] = value; }
        }

        public void PlaceWarrior(IWarrior warrior)
        {
            int warriorRow = warrior.CurrentPosition.Row;
            int warriorCol = warrior.CurrentPosition.Col;
            ValidateCell(warriorRow, warriorCol);

            this._grid[warriorRow, warriorCol] = warrior.Symbol;
        }

        public void PlaceFruit(IFruit fruit)
        {
            int fruitRow = fruit.CurrentPosition.Row;
            int fruitCol = fruit.CurrentPosition.Col;
            ValidateCell(fruitRow, fruitCol);

            this._grid[fruitRow, fruitCol] = fruit.Symbol;
        }

        public char GetCell(int row, int col)
        {
            return this._grid[row, col];
        }

        public void SetCell(int row, int col, char value)
        {
            ValidateCell(row, col);
            this._grid[row, col] = value;
        }

        private static void ValidateCell(int row, int col)
        {
            if (row < 0 || row > GlobalConstants.GameGridRowsCount)
            {
                throw new IndexOutOfRangeException($"Invalid row: {row}");
            }
            if (col < 0 || col > GlobalConstants.GameGridRowsCount)
            {
                throw new IndexOutOfRangeException($"Invalid col: {col}");
            }
        }
    }
}