﻿namespace FruitWarGame.Models.Essential
{
    using Common;
    using Contracts.Essential;

    public struct Position : IPosition
    {
        private int _row;
        private int _col;

        public Position(int row, int col)
            : this()
        {
            this.CurrentRow = row;
            this.CurrentCol = col;
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
                ValidateCol(value);
                this._col = value;
            }
        }


        private void ValidateRow(int value)
        {
            Validator.Validate(value, 0, GlobalConstants.GameGridRowsCount, "Invalid Row");
        }

        private void ValidateCol(int value)
        {
            Validator.Validate(value, 0, GlobalConstants.GameGridColsCount, "Invalid Col");
        }
    }
}