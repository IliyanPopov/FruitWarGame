﻿namespace FruitWarGame.Logic
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
            foreach (var warrior in this._warriorRepository)
            {
                warrior.CurrentPosition = GetRandomPositionInGrid();

                while (!ValidateSpawningPosition(warrior.CurrentPosition,
                    GlobalConstants.ThreePositionsApartFromEatchother))
                {
                    warrior.CurrentPosition = GetRandomPositionInGrid();
                }

                this._grid.PlaceWarrior(warrior);
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
            foreach (var fruit in this._fruitRepository)
            {
                fruit.CurrentPosition = GetRandomPositionInGrid();

                while (!ValidateSpawningPosition(fruit.CurrentPosition,
                    GlobalConstants.TwoPositionsApartFromEatchother))
                {
                    fruit.CurrentPosition = GetRandomPositionInGrid();
                }

                this._grid.PlaceFruit(fruit);
            }
        }

        // Not tested yet
        private bool ValidateSpawningPosition(IPosition entityPosition, int movesApartFromEachother)
        {
            int direction = 0; // The initial direction is "down"
            int stepsCount = 1; // Perform 1 step in current direction
            int stepPosition = 1; // 0 steps already performed
            int stepChange = 0; // Steps count changes after 2 steps
            int initialRowPosition = entityPosition.Row;
            int initialColPosition = entityPosition.Col;

            for (int i = 1; i < movesApartFromEachother; i++)
            {
                if (initialRowPosition < 0 || initialRowPosition >= GlobalConstants.GameGridRowsCount ||
                    initialColPosition < 0 || initialColPosition >= GlobalConstants.GameGridColsCount)
                {
                    continue;
                }

                if (this._grid[initialColPosition, initialRowPosition] == GlobalConstants.AppleSymbol ||
                    this._grid[initialColPosition, initialRowPosition] == GlobalConstants.PearSymbol)
                {
                    return false;
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

            return true;
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