namespace FruitWarGame.Logic
{
    using System;
    using Common;
    using Data.Contracts;
    using Models.Contracts.Essential;
    using Models.Contracts.Factories;
    using Models.Contracts.Fruits;
    using Models.Essential;

    public class GameInitializationStrategy
    {
        private const char GridDefaultSymbol = '-';
        private const int InitialApplesCount = 4;
        private const int InitialPearsCount = 3;
        private static readonly Random Random = new Random();
        private static readonly object SyncLock = new object();
        private readonly IFruitFactory _fruitFactory;
        private readonly IFruitRepository _fruitRepository;
        private readonly IGameGrid _grid;
        private IWarriorFactory _warriorFactory;
        private IWarriorRepository _warriorRepository;


        public GameInitializationStrategy(IGameGrid gamegrid, IWarriorRepository warriorRepository,
            IFruitRepository fruitRepository, IWarriorFactory warriorFactory, IFruitFactory fruitFactory)
        {
            this._grid = gamegrid;
            this._warriorRepository = warriorRepository;
            this._fruitRepository = fruitRepository;
            this._warriorFactory = warriorFactory;
            this._fruitFactory = fruitFactory;
        }

        public void Initialize()
        {
            InitializeGrid(GridDefaultSymbol);
            CreateFruits();
            AddFruitsToGrid();
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
                while (this.ValidateFruitSpawnPosition(fruit))
                {
                    fruit.CurrentPosition = GetRandomPositionInGrid();
                }
            }
        }

        private bool ValidateFruitSpawnPosition(IFruit fruit)
        {
            // TODO This is next
            throw new NotImplementedException();
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