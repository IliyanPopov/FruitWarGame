namespace FruitWarGame.Logic
{
    using System;
    using System.Linq;
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

            bool areFruitsPlaced = TryAddFruitsToGrid();
            while (!areFruitsPlaced)
            {
                areFruitsPlaced = TryAddFruitsToGrid();
            }

            bool areWarriorsPlaced = TryAddWarriorsToGrid();
            while (!areWarriorsPlaced)
            {
                areWarriorsPlaced = TryAddWarriorsToGrid();
            }

           

        }

        private bool TryAddWarriorsToGrid()
        {
            foreach (var warrior in this._warriorRepository)
            {
                warrior.CurrentPosition = GetRandomPositionInGrid();

                while (ValidateWarriorSpawningPosition(warrior, GlobalConstants.ThreePositionsApartFromEatchother))
                {
                    warrior.CurrentPosition = GetRandomPositionInGrid();
                }

                this._grid.PlaceWarrior(warrior);
            }

            if (CurrentNumberOfWarriorsOnGrid() != GlobalConstants.NumberOfPlayers)
            {
                // if this happens, than the random positions are bad, and all fruits cannot be placed correctly.
                char[] warriorSymbols = this._warriorRepository.GetAll().Select(w => w.PlayerSymbol).Distinct().ToArray();

                ClearGridFromSymbols(warriorSymbols);
                return false;
            }

            return true;
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

        private bool TryAddFruitsToGrid()
        {
            foreach (var fruit in this._fruitRepository)
            {
                fruit.CurrentPosition = GetRandomPositionInGrid();

                while (ValidateFruitSpawningPosition(fruit, GlobalConstants.TwoPositionsApartFromEatchother))
                {
                    fruit.CurrentPosition = GetRandomPositionInGrid();
                }

                this._grid.PlaceFruit(fruit);
            }

            if (CurrentNumberOfFruitssOnGrid() != InitialApplesCount + InitialPearsCount)
            {
                // if this happens, than the random positions are bad, and all fruits cannot be placed correctly.
                char[] fruitSymbols = this._fruitRepository.GetAll().Select(w => w.Symbol).Distinct().ToArray();
                ClearGridFromSymbols(fruitSymbols);
                return false;
            }

            return true;
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

        private int CurrentNumberOfWarriorsOnGrid()
        {
            int countOfWarriors = 0;
            char[] warriorSymbols = this._warriorRepository.GetAll().Select(w => w.PlayerSymbol).Distinct().ToArray();

            for (int i = 0; i < this._grid.Rows; i++)
            {
                for (int j = 0; j < this._grid.Cols; j++)
                {
                    foreach (var symbol in warriorSymbols)
                    {
                        if (this._grid.GetCell(i, j) == symbol)
                        {
                            countOfWarriors++;
                        }
                    }
                }
            }

            return countOfWarriors;
        }

        private int CurrentNumberOfFruitssOnGrid()
        {
            int countOfFruits = 0;
            char[] fruitSymbols = this._fruitRepository.GetAll().Select(w => w.Symbol).Distinct().ToArray();

            for (int i = 0; i < this._grid.Rows; i++)
            {
                for (int j = 0; j < this._grid.Cols; j++)
                {
                    foreach (var symbol in fruitSymbols)
                    {
                        if (this._grid.GetCell(i, j) == symbol)
                        {
                            countOfFruits++;
                        }
                    }
                }
            }

            return countOfFruits;
        }

        private void ClearGridFromSymbols(char[] symbolsToRemoveFromGrid)
        {
            for (int i = 0; i < this._grid.Rows; i++)
            {
                for (int j = 0; j < this._grid.Cols; j++)
                {
                    foreach (var symbol in symbolsToRemoveFromGrid)
                    {
                        if (this._grid.GetCell(i, j) == symbol)
                        {
                            this._grid[i, j] = GridDefaultSymbol;
                        }
                    }
                }
            }
        }

        private bool ValidateWarriorSpawningPosition(IWarrior warrior, int movesApartFromEachother)
        {
            // maybe using an enum as third parameter will shrink 2 to 1 method only
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