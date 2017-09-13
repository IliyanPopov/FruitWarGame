namespace FruitWarGame.ConsoleUI
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Common;
    using ConsoleIO.Contracts;
    using Data.Contracts;
    using Logic.Contracts;
    using Models.Contracts.Factories;
    using Models.Contracts.Warriors;

    public class Engine : IEngine
    {
        private readonly IGameInitializationStrategy _gameInitializationStrategy;
        private readonly IReader _reader;
        private readonly IRenderer _renderer;
        private readonly IWarriorRepository _warriorRepository;
        private readonly IWarriorFactory _warriorFactory;
        private readonly IWriter _writer;

        public Engine(IGameInitializationStrategy gameInitializationStrategy,IWarriorRepository warriorRepository, IWarriorFactory warriorFactory,
            IRenderer renderer, IReader reader, IWriter writer)
        {
            this._gameInitializationStrategy = gameInitializationStrategy;
            this._warriorRepository = warriorRepository;
            this._warriorFactory = warriorFactory;
            this._renderer = renderer;
            this._reader = reader;
            this._writer = writer;
        }

        public void Run()
        {
            IWarrior player1Warrior = null;

            while (player1Warrior == null)
            {
                player1Warrior = CreateWarrior(GlobalConstants.Player1Symbol, GlobalConstants.Player1CreationMessage,
                    GlobalConstants.AvailableWarriorsMessage);
            }

            IWarrior player2Warrior = null;

            while (player2Warrior == null)
            {
                player2Warrior = CreateWarrior(GlobalConstants.Player2Symbol, GlobalConstants.Player2CreationMessage,
                    GlobalConstants.AvailableWarriorsMessage);
            }
            this._warriorRepository.AddWarrior(player1Warrior);
            this._warriorRepository.AddWarrior(player2Warrior);

            this._gameInitializationStrategy.Initialize();
            this._renderer.RenderGrid();
        }

        private IWarrior CreateWarrior(char warriorSymbol, string playerCreationMessage, string availableWarriorsMessage)
        {
            this._writer.WriteLine(playerCreationMessage);
            this._writer.WriteLine(availableWarriorsMessage);
            int playerWarriorTpye = int.Parse(this._reader.ReadLine());
            IWarrior playerWarrior = null;

            try
            {
                playerWarrior = this._warriorFactory.CreateWarrior(warriorSymbol, playerWarriorTpye);
            }

            catch (Exception)
            {
                Console.WriteLine($"Cannot create warrior of type: {playerWarriorTpye}");
                Thread.Sleep(1500);
                this._writer.Clear();
            }

            return playerWarrior;
        }

        private void AddWarriorsToRepository(IList<IWarrior> warriors)
        {
            foreach (var warrior in warriors)
            {
                this._warriorRepository.AddWarrior(warrior);
            }
        }
    }
}