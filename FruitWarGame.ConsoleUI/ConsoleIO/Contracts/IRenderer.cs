﻿namespace FruitWarGame.ConsoleUI.ConsoleIO.Contracts
{
    using Models.Contracts.Essential;

    public interface IRenderer
    {
        void RenderGrid();

        void UpdateGrid(IPosition position);

        void Clear();
    }
}