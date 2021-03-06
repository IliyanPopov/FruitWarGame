﻿namespace FruitWarGame.ConsoleUI.ConsoleIO
{
    using System;
    using System.Text;
    using Contracts.ConsoleIO;
    using Logic.Contracts;
    using Models.Contracts.Essential;

    public class ConsoleRenderer : IRenderer
    {
        private readonly IGameGrid _grid;
        private readonly IWriter _writer;
        private readonly StringBuilder _gameScene;

        public ConsoleRenderer(IGameGrid grid, IWriter writer)
        {
            this._grid = grid;
            this._writer = writer;
            this._gameScene = new StringBuilder();
        }

        public void RenderGrid()
        {
            for (int row = 0; row < this._grid.Rows; row++)
            {
                for (int col = 0; col < this._grid.Cols; col++)
                {
                    this._gameScene.Append(this._grid.GetCell(row, col) + " ");
                }

                this._gameScene.Append(Environment.NewLine);
                this._gameScene.Append(Environment.NewLine);
            }

            this._writer.WriteLine(this._gameScene.ToString());
            this._gameScene.Clear();
        }

        public void Clear()
        {
            this._writer.Clear();
        }
    }
}