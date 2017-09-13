namespace FruitWarGame.Logic
{
    using System;
    using System.Collections.Generic;
    using Common;
    using Contracts;
    using Data.Contracts;
    using Models.Contracts.Essential;
    using Models.Contracts.Factories;
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
        private readonly IWarriorFactory _warriorFactory;
        private readonly IWarriorRepository _warriorRepository;

        public GameInitializationStrategy(IGameGrid gamegrid, IWarriorRepository warriorRepository,
            IFruitRepository fruitRepository, IWarriorFactory warriorFactory, IFruitFactory fruitFactory)
        {
            this._grid = gamegrid;
            this._warriorRepository = warriorRepository;
            this._fruitRepository = fruitRepository;
            this._warriorFactory = warriorFactory;
            this._fruitFactory = fruitFactory;
        }

        public void Initialize(IDictionary<char, int> warriorTypes)
        {
            InitializeGrid(GridDefaultSymbol);
            CreateFruits();
            AddFruitsToGrid();
            CreateWarriors(warriorTypes);
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
                    while (ValidateSpawningPosition(warrior.CurrentPosition,
                        GlobalConstants.ThreePositionsApartFromEatchother))
                    {
                        warrior.CurrentPosition = GetRandomPositionInGrid();
                    }

                    this._grid.PlaceWarrior(warrior);
                }
            }
        }

        private void CreateWarriors(IDictionary<char, int> warriorTypes)
        {
            foreach (var warriorType in warriorTypes)
            {
                var warrior = this._warriorFactory.CreateWarrior(warriorType.Key, warriorType.Value);
                this._warriorRepository.AddWarrior(warrior);
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
                    while (ValidateSpawningPosition(fruit.CurrentPosition, GlobalConstants.OnePositionsApartFromEatchother))
                    {
                        fruit.CurrentPosition = GetRandomPositionInGrid();
                    }

                    this._grid.PlaceFruit(fruit);
                }
            }
        }

        // Not tested yet
        private bool ValidateSpawningPosition(IPosition entityPosition, int movesApartFromEachother)
        {
            int direction = 0; // The initial direction is "down"
            int stepsCount = 1; // Perform 1 step in current direction
            int stepPosition = 0; // 0 steps already performed
            int stepChange = 0; // Steps count changes after 2 steps
            int initialRowPosition = entityPosition.Row;
            int initialColPosition = entityPosition.Col;

            for (int i = 0; i < movesApartFromEachother; i++)
            {
                if (i == 0)
                {
                    stepPosition++;
                }

                else
                {
                    if (initialRowPosition >= 0 && initialRowPosition <= this._grid.Rows - 1 &&
                        initialColPosition >= 0 && initialColPosition <= this._grid.Cols - 1)
                    {
                        if (this._grid[initialRowPosition, initialColPosition] == GlobalConstants.AppleSymbol ||
                            this._grid[initialRowPosition, initialColPosition] == GlobalConstants.PearSymbol)
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
                        initialColPosition++;
                        break;
                    case 1:
                        initialRowPosition--;
                        break;
                    case 2:
                        initialColPosition--;
                        break;
                    case 3:
                        initialRowPosition++;
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

        // another test
        private bool findingNeighbors(IPosition entityPosition)
        {
            var rowLimit = this._grid.Rows;
            var columnLimit = this._grid.Cols;

            for (var x = Math.Max(0, entityPosition.Row - 1); x <= Math.Min(entityPosition.Row + 1, rowLimit); x++)
            {
                for (var y = Math.Max(0, entityPosition.Col - 1);
                    y <= Math.Min(entityPosition.Col + 1, columnLimit);
                    y++)
                {
                    if (x != entityPosition.Row || y != entityPosition.Col)
                    {
                        if (x < 0 && x >= GlobalConstants.GameGridRowsCount ||
                            y < 0 && y >= GlobalConstants.GameGridColsCount)
                        {
                            if (this._grid[x, y] == GlobalConstants.AppleSymbol ||
                                this._grid[x, y] == GlobalConstants.PearSymbol)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}