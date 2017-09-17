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

            this._renderer.Clear();
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
                    try
                    {
                        MoveWarriorDependingOnSpeedPoints(player1Warrior, GlobalConstants.Player1MakeMoveMessage);
                        playerTurns++;
                    }
                    catch (ArgumentException e)
                    {
                        ProcessInvalidDirectionException(e);
                    }
                }
                if (playerTurns % 2 == 1)
                {
                    // second player turns player makes turn
                    try
                    {
                        MoveWarriorDependingOnSpeedPoints(player2Warrior, GlobalConstants.Player2MakeMoveMessage);
                        playerTurns++;
                    }
                    catch (ArgumentException e)
                    {
                        ProcessInvalidDirectionException(e);
                    }
                }
            }
        }

        private IWarrior CreateWarrior(char warriorSymbol, string playerCreationMessage,
            string availableWarriorsMessage)
        {

            IWarrior playerWarrior = null;

            while (playerWarrior == null)
            {
                this._renderer.Clear();
                this._writer.WriteLine(playerCreationMessage);
                this._writer.WriteLine(availableWarriorsMessage);

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
                    this._renderer.Clear();
                }

                catch (ArgumentException)
                {
                    Console.WriteLine($"Wrong input!");
                    Thread.Sleep(2000);
                    this._renderer.Clear();
                }
            }

            return playerWarrior;
        }

        private void MoveWarriorDependingOnSpeedPoints(IWarrior playerwarrior, string makeMoveMessage)
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
            int positionX = warrior.CurrentPosition.Col;
            int positionY = warrior.CurrentPosition.Row;
            //TODO i need a boolean suicide flag, need to remember the old position of the player so i can update the grid, and pass the key variable to the method
            switch (direction.Key)
            {
                case ConsoleKey.UpArrow:
                    positionY = (warrior.CurrentPosition.Row - 1) % (this._grid.Rows);

                    if (positionY < 0)
                    {
                        positionY = this._grid.Rows - 1;
                    }

                    break;
                case ConsoleKey.DownArrow:
                    positionY = (warrior.CurrentPosition.Row + 1) % (this._grid.Rows);
                    if (positionY > this._grid.Rows - 1)
                    {
                        positionY = 0;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    positionX = (warrior.CurrentPosition.Col - 1) % this._grid.Cols;
                    if (positionX < 0)
                    {
                        positionX = this._grid.Cols - 1;
                    }

                    break;
                case ConsoleKey.RightArrow:
                    positionX = (warrior.CurrentPosition.Col + 1) % this._grid.Cols;
                    if (positionX > this._grid.Cols - 1)
                    {
                        positionX = 0;
                    }
                    break;
                default:
                    throw new ArgumentException("Direction is not supported. Please use the arrow keys to navigate!");
            }

            if (this._grid.GetCell(positionY, positionX) == GlobalConstants.AppleSymbol ||
                this._grid.GetCell(positionY, positionX) == GlobalConstants.PearSymbol)
            {
                var fruit = GetFruitByPosition(positionY, positionX);

                warrior.EatFruit(fruit);
            }

            if (this._grid.GetCell(positionY, positionX) == GlobalConstants.Player1Symbol ||
                this._grid.GetCell(positionY, positionX) == GlobalConstants.Player2Symbol)
            {
                ProcessEndGame(positionX, positionY);
            }

            // update grid old warriors position to default symbol
            this._grid.SetCell(warrior.CurrentPosition.Row, warrior.CurrentPosition.Col,
                GlobalConstants.GridDefaultSymbol);

            //update grid to new warrior's position
            warrior.CurrentPosition.Row = positionY;
            warrior.CurrentPosition.Col = positionX;
            this._grid.SetCell(warrior.CurrentPosition.Row, warrior.CurrentPosition.Col, warrior.Symbol);

            this._renderer.Clear();
            this._renderer.RenderGrid();
        }


        private IFruit GetFruitByPosition(int positionX, int positionY)
        {
            var fruit = this._fruitRepository.FirstOrDefault(f => f.CurrentPosition.Row == positionX &&
                                                                  f.CurrentPosition.Col == positionY);

            return fruit;
        }

        private void ProcessEndGame(int positionX, int positionY)
        {
            IWarrior warrior1 = this._warriorRepository.GetAll()
                .FirstOrDefault(w => w.Symbol == GlobalConstants.Player1Symbol);

            IWarrior warrior2 = this._warriorRepository.GetAll()
                .FirstOrDefault(w => w.Symbol == GlobalConstants.Player2Symbol);
            //TODO 50% done. Need to fix when player commits suicide logic
            if (warrior1.TotalPowerPoints > warrior2.TotalPowerPoints)
            {
                // update grid old warriors position to default symbol
                this._grid.SetCell(warrior1.CurrentPosition.Row, warrior1.CurrentPosition.Col,
                    GlobalConstants.GridDefaultSymbol);

                //update grid to new warrior's position
                warrior1.CurrentPosition.Row = positionY;
                warrior1.CurrentPosition.Col = positionX;
                this._grid.SetCell(warrior1.CurrentPosition.Row, warrior1.CurrentPosition.Col, warrior1.Symbol);

                this._renderer.Clear();
                this._renderer.RenderGrid();

                this._writer.WriteLine(GetFinishingStatsForWarrior(warrior1));

            }
            else if (warrior1.TotalPowerPoints < warrior2.TotalPowerPoints)
            {
                // update grid old warriors position to default symbol
                this._grid.SetCell(warrior2.CurrentPosition.Row, warrior2.CurrentPosition.Col,
                    GlobalConstants.GridDefaultSymbol);

                //update grid to new warrior's position
                warrior2.CurrentPosition.Row = positionY;
                warrior2.CurrentPosition.Col = positionX;
                this._grid.SetCell(warrior2.CurrentPosition.Row, warrior2.CurrentPosition.Col, warrior2.Symbol);

                this._renderer.Clear();
                this._renderer.RenderGrid();

                this._writer.WriteLine(GetFinishingStatsForWarrior(warrior2));

            }
            else
            {
                this._writer.WriteLine(Environment.NewLine);
                this._writer.WriteLine("Draw game.");
            }

            ProcessRestartGame();
        }

        private string GetFinishingStatsForWarrior(IWarrior warrior)
        {
            var warriorTypeName = warrior.GetType().Name;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Environment.NewLine);
            sb.AppendLine(
                $"Player {warrior.Symbol} wins the game.");
            sb.AppendLine(
                $"{warriorTypeName} with Power: {warrior.TotalPowerPoints}, Speed: {warrior.TotalSpeedPoints}");
            return sb.ToString();
        }

        private void ProcessRestartGame()
        {
            this._writer.WriteLine("Do you want to start a rematch? (y/n)");
            string answer = this._reader.ReadLine().ToLower();

            switch (answer)
            {
                case "y":
                    this._renderer.Clear();
                    this._warriorRepository.RemoveAll();
                    this._fruitRepository.RemoveAll();
                    Run();
                    break;
                case "n":
                    Environment.Exit(0);
                    break;
                default:
                    this._writer.WriteLine("Unsuported symbol!");
                    this._writer.WriteLine("Quiting game...");
                    Thread.Sleep(1000);
                    Environment.Exit(0);
                    break;
            }
        }

        private void ProcessInvalidDirectionException(ArgumentException e)
        {
            this._writer.WriteLine(Environment.NewLine);
            this._writer.WriteLine(e.Message);
            Thread.Sleep(2500);
            this._renderer.Clear();
            this._renderer.RenderGrid();
            this._writer.WriteLine(GetAllPlayersStats());
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
    }
}