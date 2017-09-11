namespace FruitWarGame.Logic
{
    using System;
    using Data.Contracts;
    using Models.Contracts.Essential;
    using Models.Contracts.Factories;

    public class GameInitializationStrategy
    {
        private IGameGrid _grid;
        private IFruitFactory _fruitFactory;
        private IFruitRepository _fruitRepository;
        private IWarriorFactory _warriorFactory;
        private IWarriorRepository _warriorRepository;
        private static readonly Random _random = new Random();
        private static readonly object _syncLock = new object();

        private const char GridDefaultSymbol = '-';
        private const int InitialApplesCount = 4;
        private const int InitialPearsCount = 3;


        public GameInitializationStrategy(IGameGrid gamegrid, IWarriorRepository warriorRepository, IFruitRepository fruitRepository, IWarriorFactory warriorFactory, IFruitFactory fruitFactory)
        {
            this._grid = gamegrid;
            this._warriorRepository = warriorRepository;
            this._fruitRepository = fruitRepository;
            this._warriorFactory = warriorFactory;
            this._fruitFactory = fruitFactory;
        }

        public void Initialize()
        {
            this.InitializeGrid(GridDefaultSymbol);
            this.AddWarriors();
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

        private static int RandomNumber(int min, int max)
        {
            lock (_syncLock)
            { // synchronize
                return _random.Next(min, max);
            }
        }
    }
}