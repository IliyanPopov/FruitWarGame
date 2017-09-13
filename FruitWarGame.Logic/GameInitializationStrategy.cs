namespace FruitWarGame.Logic
{
    using System;
    using Common;
    using Contracts;
    using Data.Contracts;
    using Models.Contracts.Essential;
    using Models.Contracts.Factories;
    using Models.Contracts.Fruits;
    using Models.Contracts.Warriors;
    using Models.Essential;

    public class GameInitializationStrategy : IGameInitializationStrategy
    {
        private const char GridDefaultSymbol = '-';
        private const int InitialApplesCount = 4;
        private const int InitialPearsCount = 3;

        private static readonly Random Random = new Random();
        private static readonly object SyncLock = new object();
        private readonly IFruitFactory _fruitFactory;
        private readonly IFruitRepository _fruitRepository;
        private readonly IGameGrid _grid;
        private readonly IWarriorRepository _warriorRepository;

        public GameInitializationStrategy(IGameGrid gamegrid, IWarriorRepository warriorRepository,
            IFruitRepository fruitRepository, IFruitFactory fruitFactory)
        {
            this._grid = gamegrid;
            this._warriorRepository = warriorRepository;
            this._fruitRepository = fruitRepository;
            this._fruitFactory = fruitFactory;
        }

        public void Initialize()
        {
            InitializeGrid(GridDefaultSymbol);
            CreateFruits();
            AddFruitsToGrid();
            AddWarriorsToGrid();
        }

        private void AddWarriorsToGrid()
        {
            bool isFirstIteration = true;
            foreach (var warrior in this._warriorRepository)
            {
                warrior.CurrentPosition = GetRandomPositionInGrid();

                if (isFirstIteration)
                {
                    this._grid.PlaceWarrior(warrior);
                    isFirstIteration = false;
                }
                else
                {
                    while (ValidateWarriorSpawningPosition(warrior, GlobalConstants.TwoPositionsApartFromEatchother))
                    {
                        warrior.CurrentPosition = GetRandomPositionInGrid();
                    }

                    this._grid.PlaceWarrior(warrior);
                }
            }
        }


        private void CreateFruits()
        {
            for (int i = 0; i < InitialApplesCount; i++)
            {
                var apple = this._fruitFactory.CreateFruit(GlobalConstants.AppleSymbol);
                this._fruitRepository.AddFruit(apple);
            }

            for (int i = 0; i < InitialPearsCount; i++)
            {
                var pear = this._fruitFactory.CreateFruit(GlobalConstants.PearSymbol);
                this._fruitRepository.AddFruit(pear);
            }
        }

        private void AddFruitsToGrid()
        {
            bool isFirstIteration = true;
            foreach (var fruit in this._fruitRepository)
            {
                fruit.CurrentPosition = GetRandomPositionInGrid();

                if (isFirstIteration)
                {
                    this._grid.PlaceFruit(fruit);
                    isFirstIteration = false;
                }
                else
                {
                    while (ValidateFruitSpawningPosition(fruit, 6))
                    {
                        fruit.CurrentPosition = GetRandomPositionInGrid();
                    }

                    this._grid.PlaceFruit(fruit);
                }
            }
        }

        // kind of works
        private bool ValidateFruitSpawningPosition(IFruit fruit, int movesApartFromEachother)
        {
            int direction = 0; // The initial direction is "down"
            int stepsCount = 1; // Perform 1 step in current direction
            int stepPosition = 0; // 0 steps already performed
            int stepChange = 0; // Steps count changes after 2 steps
            int positionX = fruit.CurrentPosition.Row;
            int positionY = fruit.CurrentPosition.Col;

            for (int i = 0; i < movesApartFromEachother; i++)
            {
                if (i == 0)
                {
                    stepPosition++;
                }

                else
                {
                    if (positionX >= 0 && positionX <= this._grid.Rows - 1 &&
                        positionY >= 0 && positionY <= this._grid.Cols - 1)
                    {
                        if (this._grid[positionX, positionY] == GlobalConstants.AppleSymbol ||
                            this._grid[positionX, positionY] == GlobalConstants.PearSymbol)
                        {
                            return true;
                        }
                    }

                    // Check for direction / step changes
                    if (stepPosition < stepsCount)
                    {
                        stepPosition++;
                    }
                    else
                    {
                        stepPosition = 1;
                        if (stepChange == 1)
                        {
                            stepsCount++;
                        }
                        stepChange = (stepChange + 1) % 2;
                        direction = (direction + 1) % 4;
                    }
                }

                // Move to the next cell in the current direction
                switch (direction)
                {
                    case 0:
                        positionY++;
                        break;
                    case 1:
                        positionX--;
                        break;
                    case 2:
                        positionY--;
                        break;
                    case 3:
                        positionX++;
                        break;
                }
            }

            return false;
        }

        private bool ValidateWarriorSpawningPosition(IWarrior warrior, int movesApartFromEachother)
        {
            int direction = 0; // The initial direction is "down"
            int stepsCount = 1; // Perform 1 step in current direction
            int stepPosition = 0; // 0 steps already performed
            int stepChange = 0; // Steps count changes after 2 steps
            int positionX = warrior.CurrentPosition.Row;
            int positionY = warrior.CurrentPosition.Col;

            for (int i = 0; i < movesApartFromEachother; i++)
            {
                if (i == 0)
                {
                    stepPosition++;
                }

                else
                {
                    if (positionX >= 0 && positionX <= this._grid.Rows - 1 &&
                        positionY >= 0 && positionY <= this._grid.Cols - 1)
                    {
                        if (
                            this._grid[positionX, positionY] == GlobalConstants.Player1Symbol ||
                            this._grid[positionX, positionY] == GlobalConstants.Player2Symbol)
                        {
                            return true;
                        }
                    }

                    // Check for direction / step changes
                    if (stepPosition < stepsCount)
                    {
                        stepPosition++;
                    }
                    else
                    {
                        stepPosition = 1;
                        if (stepChange == 1)
                        {
                            stepsCount++;
                        }
                        stepChange = (stepChange + 1) % 2;
                        direction = (direction + 1) % 4;
                    }
                }

                // Move to the next cell in the current direction
                switch (direction)
                {
                    case 0:
                        positionY++;
                        break;
                    case 1:
                        positionX--;
                        break;
                    case 2:
                        positionY--;
                        break;
                    case 3:
                        positionX++;
                        break;
                }
            }

            return false;
        }

        private void InitializeGrid(char symbol)
        {
            for (int row = 0; row < this._grid.Rows; row++)
            {
                for (int col = 0; col < this._grid.Cols; col++)
                {
                    this._grid.SetCell(row, col, symbol);
                }
            }
        }

        private IPosition GetRandomPositionInGrid()
        {
            int row;
            int col;

            lock (SyncLock)
            {
                row = Random.Next(0, this._grid.Rows);
                col = Random.Next(0, this._grid.Cols);
            }

            return new Position(row, col);
        }
    }
}