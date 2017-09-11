namespace FruitWarGame.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Models.Contracts.Fruits;

    public class InMemoryFruitRepository : IRepository<IFruit>
    {
        private readonly HashSet<IFruit> _fruitsPlacedOnGameBoard;

        public InMemoryFruitRepository()
        {
            this._fruitsPlacedOnGameBoard = new HashSet<IFruit>();
        }

        public void Add(IFruit fruit)
        {
            this._fruitsPlacedOnGameBoard.Add(fruit);
        }

        public IFruit GetBySymbol(char symbol)
        {
            var fruit = this._fruitsPlacedOnGameBoard.FirstOrDefault(f => f.Symbol == symbol);

            if (fruit != null)
            {
                return fruit;
            }

            throw new ArgumentException($"No fruit with such a symbol: {symbol} is placed on the game board!");
        }

        public ICollection<IFruit> GetAll()
        {
            return this._fruitsPlacedOnGameBoard;
        }
    }
}