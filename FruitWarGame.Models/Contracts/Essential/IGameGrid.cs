﻿namespace FruitWarGame.Models.Contracts.Essential
{
    using Fruits;
    using Warriors;

    public interface IGameGrid
    {
        int Rows { get; }

        int Cols { get; }

        char this[int xPosition, int yPosition] { get; set; }

        void PlaceWarrior(IWarrior warrior);

        void PlaceFruit(IFruit fruit);

        char GetCell(int row, int col);

        void SetCell(int row, int col, char value);
    }
}