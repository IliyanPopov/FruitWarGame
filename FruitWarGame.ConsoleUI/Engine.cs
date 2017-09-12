namespace FruitWarGame.ConsoleUI
{
    using System.Collections.Generic;
    using Common;
    using ConsoleIO.Contracts;
    using Contracts;
    using Logic.Contracts;

    public class Engine : IEngine
    {
        private readonly IGameInitializationStrategy _gameInitializationStrategy;
        private readonly IRenderer _renderer;
        private readonly IReader _reader;
        private readonly IWriter _writer;

        public Engine(IGameInitializationStrategy gameInitializationStrategy, IRenderer renderer, IReader reader, IWriter writer)
        {
            this._gameInitializationStrategy = gameInitializationStrategy;
            this._renderer = renderer;
            this._reader = reader;
            this._writer = writer;
        }

        public void Run()
        {
            this._writer.WriteLine("Player1, please choose a warrior.");
            this._writer.WriteLine("Insert 1 for turtle / 2 for monkey / 3 for pigeon");
            int player1Warrior = int.Parse(this._reader.ReadLine());

            this._writer.WriteLine("Player2, please choose a warrior.");
            this._writer.WriteLine("Insert 1 for turtle / 2 for monkey / 3 for pigeon");
            int player2Warrior = int.Parse(this._reader.ReadLine());

            IDictionary<char, int> warriors = new Dictionary<char, int>();
            warriors.Add(GlobalConstants.Player1Symbol, player1Warrior);
            warriors.Add(GlobalConstants.Player2Symbol, player2Warrior);

            this._gameInitializationStrategy.Initialize(warriors);
            this._renderer.RenderGrid();
        }
    }
}