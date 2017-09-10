namespace FruitWarGame.Models.Essential
{
    using System;
    using Contracts.Essential;

    public struct Position : IPosition
    {
        private int _row;
        private int _col;
        private readonly IGameGrid _grid;

        public Position(IGameGrid grid, int row, int col)
            : this()
        {
            this.CurrentRow = row;
            this.CurrentCol = col;
            this._grid = grid;
        }

        public int CurrentRow
        {
            get { return this._row; }

            set
            {
                ValidateRow(value);
                this._row = value;
            }
        }

        public int CurrentCol
        {
            get { return this._col; }

            set
            {
                ValidateRow(value);
                this._col = value;
            }
        }

        private void ValidateRow(int row)
        {
            if (row < 0 || row > this._grid.Rows - 1)
            {
                throw new IndexOutOfRangeException($"Invalid row: {row}");
            }
        }
    }
}