﻿namespace FruitWarGame.Logic
{
    using System;
    using System.Collections.Generic;
    using Common;
    using Data.Contracts;
    using Models.Contracts.Essential;
    using Models.Contracts.Factories;
    using Models.Contracts.Fruits;
    using Models.Contracts.Warriors;
    using Models.Essential;

    public class GameInitializationStrategy
    {
        private const char GridDefaultSymbol = '-';
        private const int InitialApplesCount = 4;
        private const int InitialPearsCount = 3;
        private const int MinimumDifferenceInFruitSpawningPosition = 2;
        private const int MinimumDifferenceInWarriorSpawningPosition = 3;

        private static readonly Random Random = new Random();
        private static readonly object SyncLock = new object();
        private readonly IFruitFactory _fruitFactory;
        private readonly IFruitRepository _fruitRepository;
        private readonly IGameGrid _grid;
        private readonly IPosition _position;
        private readonly IWarriorFactory _warriorFactory;
        private readonly IWarriorRepository _warriorRepository;

        public GameInitializationStrategy(IGameGrid gamegrid, IPosition position, IWarriorRepository warriorRepository,
            IFruitRepository fruitRepository, IWarriorFactory warriorFactory, IFruitFactory fruitFactory)
        {
            this._grid = gamegrid;
            this._position = position;
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
            foreach (var warrior in this._warriorRepository)
            {
                while (ValidateWarriorSpawnPosition(warrior))
                {
                    warrior.CurrentPosition = GetRandomPositionInGrid();
                }
            }
        }

        private bool ValidateWarriorSpawnPosition(IWarrior warrior)
        {
            throw new NotImplementedException();
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
            foreach (var fruit in this._fruitRepository)
            {
                fruit.CurrentPosition = GetRandomPositionInGrid();

                while (ValidateSpawningPosition(fruit))
                {
                    fruit.CurrentPosition = GetRandomPositionInGrid();
                }

                this._grid.PlaceFruit(fruit);
            }
        }

      
        private bool ValidateSpawningPosition(object gameObject)
        {
            int positionX = 5;
            int positionY = 5;

            int direction = 0; // The initial direction is "down"
            int stepsCount = 1; // Perform 1 step in current direction
            int stepPosition = 0; // 0 steps already performed
            int stepChange = 0; // Steps count changes after 2 steps

            // for 1 position difference i  = 9
            // for 2 position difference i  = 25
            // for 3 position difference i  = 49
            // for 4 position difference i  = 81

            for (int i = 0; i < 25; i++)
            {

                // Fill the current cell with the current value
                if (i == 0)
                {
                    this._grid[positionY, positionX] = "Z";
                }

                else
                {
                    matrix[positionY, positionX] = "X";
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