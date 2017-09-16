namespace FruitWarGame.ConsoleUI.Core
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using Common;
    using Contracts.ConsoleIO;
    using Data.Contracts;
    using Logic.Contracts;
    using Models.Contracts.Essential;
    using Models.Contracts.Factories;
    using Models.Contracts.Fruits;
    using Models.Contracts.Warriors;

    public class Engine : IEngine
    {
        private readonly IFruitRepository _fruitRepository;
        private readonly IGameInitializationStrategy _gameInitializationStrategy;
        private readonly IGameGrid _grid;
        private readonly IReader _reader;
        private readonly IRenderer _renderer;
        private readonly IWarriorFactory _warriorFactory;
        private readonly IWarriorRepository _warriorRepository;
        private readonly IWriter _writer;

        public Engine(IGameInitializationStrategy gameInitializationStrategy, IWarriorRepository warriorRepository,
            IFruitRepository fruitRepository,
            IWarriorFactory warriorFactory, IGameGrid grid,
            IRenderer renderer, IReader reader, IWriter writer)
        {
            this._gameInitializationStrategy = gameInitializationStrategy;
            this._warriorRepository = warriorRepository;
            this._fruitRepository = fruitRepository;
            this._warriorFactory = warriorFactory;
            this._renderer = renderer;
            this._reader = reader;
            this._writer = writer;
            this._grid = grid;
        }

        public void Run()
        {
            // make preperations to start the game
            IWarrior player1Warrior = CreateWarrior(GlobalConstants.Player1Symbol,
                GlobalConstants.Player1CreationMessage,
                GlobalConstants.ChooseWarriorsMessage);

            IWarrior player2Warrior = CreateWarrior(GlobalConstants.Player2Symbol,
                GlobalConstants.Player2CreationMessage,
                GlobalConstants.ChooseWarriorsMessage);

            this._warriorRepository.AddWarrior(player1Warrior);
            this._warriorRepository.AddWarrior(player2Warrior);

            this._writer.Clear();
            this._gameInitializationStrategy.Initialize();
            this._renderer.RenderGrid();
            this._writer.WriteLine(GetAllPlayersStats());

            // process player turns and movement
            int playerTurns = 0;
            while (true)
            {
                if (playerTurns % 2 == 0)
                {
                    // first player makes turn
                    MovePlayerDependingOnSpeedPoints(player1Warrior, GlobalConstants.Player1MakeMoveMessage);
                }
                if (playerTurns % 2 == 1)
                {
                    // second player turns player makes turn
                    MovePlayerDependingOnSpeedPoints(player2Warrior, GlobalConstants.Player2MakeMoveMessage);
                }

                playerTurns++;
            }
        }

        private void MovePlayerDependingOnSpeedPoints(IWarrior playerwarrior, string makeMoveMessage)
        {
            int initialSpeedPoints = playerwarrior.TotalSpeedPoints;

            for (int i = 0; i < initialSpeedPoints; i++)
            {
                this._writer.WriteLine(makeMoveMessage);
                var direction = Console.ReadKey();
                MoveWarriorInDirection(playerwarrior, direction);
                this._writer.WriteLine(GetAllPlayersStats());
            }
        }

        private void MoveWarriorInDirection(IWarrior warrior, ConsoleKeyInfo direction)
        {
            int positionX = warrior.CurrentPosition.Row;
            int positionY = warrior.CurrentPosition.Col;
            switch (direction.Key)
            {
                case ConsoleKey.UpArrow:
                    positionX = (warrior.CurrentPosition.Row - 1) % (this._grid.Rows);

                    if (positionX < 0)
                    {
                        positionX = this._grid.Rows - 1;
                    }

                    break;
                case ConsoleKey.DownArrow:
                    positionX = (warrior.CurrentPosition.Row + 1) % (this._grid.Rows);
                    if (positionX > this._grid.Rows - 1)
                    {
                        positionX = 0;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    positionY = (warrior.CurrentPosition.Col - 1) % this._grid.Cols;
                    if (positionY < 0)
                    {
                        positionY = this._grid.Cols - 1;
                    }

                    break;
                case ConsoleKey.RightArrow:
                    positionY = (warrior.CurrentPosition.Col + 1) % this._grid.Cols;
                    if (positionY > this._grid.Cols - 1)
                    {
                        positionY = 0;
                    }
                    break;
                default:
                    throw new ArgumentException("Direction is not supported!");
            }

            if (this._grid.GetCell(positionX, positionY) == GlobalConstants.AppleSymbol ||
                this._grid.GetCell(positionX, positionY) == GlobalConstants.PearSymbol)
            {
                var fruit = GetFruitByPosition(positionX, positionY);

                warrior.EatFruit(fruit);
            }

            // update grid old warriors position to default symbol
            this._grid.SetCell(warrior.CurrentPosition.Row, warrior.CurrentPosition.Col,
                GlobalConstants.GridDefaultSymbol);

            //update grid to new warrior's position

            warrior.CurrentPosition.Row = positionX;
            warrior.CurrentPosition.Col = positionY;

            this._grid.SetCell(warrior.CurrentPosition.Row, warrior.CurrentPosition.Col, warrior.Symbol);

            this._writer.Clear();
            this._renderer.RenderGrid();
        }

        private string GetAllPlayersStats()
        {
            StringBuilder sb = new StringBuilder();
            int i = 1;

            foreach (var warrior in this._warriorRepository.GetAll())
            {
                sb.AppendLine($"Player{i}: {warrior.TotalPowerPoints} Power; {warrior.TotalSpeedPoints} Speed");
                i++;
            }

            return sb.ToString();
        }

        private IFruit GetFruitByPosition(int positionX, int positionY)
        {
            var fruit = this._fruitRepository.FirstOrDefault(f => f.CurrentPosition.Row == positionX &&
                                                                  f.CurrentPosition.Col == positionY);

            return fruit;
        }

        private IWarrior CreateWarrior(char warriorSymbol, string playerCreationMessage,
            string availableWarriorsMessage)
        {
            this._writer.WriteLine(playerCreationMessage);
            this._writer.WriteLine(availableWarriorsMessage);
            IWarrior playerWarrior = null;

            while (playerWarrior == null)
            {
                try
                {
                    int playerWarriorTpye;
                    bool isParseSuccessfull = int.TryParse(this._reader.ReadLine(), out playerWarriorTpye);
                    if (!isParseSuccessfull)
                    {
                        throw new ArgumentException();
                    }
                    playerWarrior = this._warriorFactory.CreateWarrior(warriorSymbol, playerWarriorTpye);
                }

                catch (NotImplementedException e)
                {
                    Console.WriteLine($"{e.Message}");
                    Thread.Sleep(2000);
                    this._writer.Clear();
                }

                catch (ArgumentException)
                {
                    Console.WriteLine($"Wrong input!");
                    Thread.Sleep(2000);
                    this._writer.Clear();
                }
            }

            return playerWarrior;
        }
    }
}