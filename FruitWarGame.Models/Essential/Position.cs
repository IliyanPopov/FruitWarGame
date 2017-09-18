namespace FruitWarGame.Models.Essential
{
    using System;
    using Common;
    using Contracts.Essential;

    public struct Position : IPosition
    {
        private int _row;
        private int _col;

        public Position(int row, int col)
            : this()
        {
            this.Row = row;
            this.Col = col;
        }

        public int Row
        {
            get { return this._row; }

            set
            {
                this.ValidateRow(value);
                this._row = value;
            }
        }

        public int Col
        {
            get { return this._col; }

            set
            {
                this.ValidateCol(value);
                this._col = value;
            }
        }

        private void ValidateRow(int value)
        {
            this.Validate(value, 0, GlobalConstants.GameGridRowsCount, "Invalid Row");
        }

        private void ValidateCol(int value)
        {
            this.Validate(value, 0, GlobalConstants.GameGridColsCount, "Invalid Col");
        }

        private void Validate(int value, int min, int max, string errorMessage)
        {
            if (value < min || value >= max)
            {
                throw new IndexOutOfRangeException(errorMessage);
            }
        }
    }
}