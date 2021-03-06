﻿namespace FruitWarGame.Logic.Concrete
{
    using System;
    using Common;
    using Contracts;
    using Data.Contracts;
    using Models.Contracts.Essential;
    using Models.Contracts.Factories;
    using Models.Essential;

    public class GameInitializationStrategy : IGameInitializationStrategy
    {
        private const int InitialApplesCount = 4;
        private const int InitialPearsCount = 3;

        private static readonly Random Random = new Random();
        private static readonly object SyncLock = new object();
        private readonly IFruitFactory _fruitFactory;
        private readonly IFruitRepository _fruitRepository;
        private readonly IGameGrid _grid;
        private readonly ISpawningValidator _spawningValidator;
        private readonly IWarriorRepository _warriorRepository;

        public GameInitializationStrategy(IGameGrid gamegrid, IWarriorRepository warriorRepository, IFruitRepository fruitRepository, IFruitFactory fruitFactory, ISpawningValidator spawningValidator)
        {
            this._grid = gamegrid;
            this._warriorRepository = warriorRepository;
            this._fruitRepository = fruitRepository;
            this._fruitFactory = fruitFactory;
            this._spawningValidator = spawningValidator;
        }

        public void Initialize()
        {
            this.InitializeGrid(GlobalConstants.GridDefaultSymbol);
            this.CreateFruits();
            this.AddFruitsToGrid();
            this.AddWarriorsToGrid();
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
                fruit.CurrentPosition = this.GetRandomPositionInGrid();

                while (this._spawningValidator.ValidateSpawningPosition(fruit.CurrentPosition, PlacableEntities.Fruit, GlobalConstants.TwoPositionsApartFromEatchother))
                {
                    fruit.CurrentPosition = this.GetRandomPositionInGrid();
                }

                this._grid.PlaceFruit(fruit);
            }
        }

        private void AddWarriorsToGrid()
        {
            foreach (var warrior in this._warriorRepository)
            {
                warrior.CurrentPosition = this.GetRandomPositionInGrid();

                while (this._spawningValidator.ValidateSpawningPosition(warrior.CurrentPosition, PlacableEntities.Warrior, GlobalConstants.ThreePositionsApartFromEatchother))
                {
                    warrior.CurrentPosition = this.GetRandomPositionInGrid();
                }

                this._grid.PlaceWarrior(warrior);
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

                while (this._grid[row, col] != GlobalConstants.GridDefaultSymbol)
                {
                    row = Random.Next(0, this._grid.Rows);
                    col = Random.Next(0, this._grid.Cols);
                }
            }

            return new Position(row, col);
        }
    }
}