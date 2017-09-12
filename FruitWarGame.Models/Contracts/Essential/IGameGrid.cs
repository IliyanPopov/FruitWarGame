﻿namespace FruitWarGame.Models.Contracts.Essential
{
    using Fruits;
    using Warriors;

    public interface IGameGrid
    {
        int Rows { get; }

        int Cols { get; }

        void PlaceWarrior(IWarrior warrior);

        void PlaceFruit(IFruit fruit);

        char GetCell(IPosition position);

        char GetCell(int row, int col);

        void SetCell(IPosition position, char value);

        void SetCell(int row, int col, char value);
    }
}