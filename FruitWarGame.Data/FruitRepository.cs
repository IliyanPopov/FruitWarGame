namespace FruitWarGame.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Models.Contracts.Fruits;

    public class FruitRepository : IFruitRepository
    {
        private readonly HashSet<IFruit> _fruitsPlacedOnGameBoard;

        public FruitRepository()
        {
            this._fruitsPlacedOnGameBoard = new HashSet<IFruit>();
        }

        public void AddFruit(IFruit fruit)
        {
            this._fruitsPlacedOnGameBoard.Add(fruit);
        }

        public IFruit GetFruitBySymbol(char symbol)
        {
            var fruit = this._fruitsPlacedOnGameBoard.FirstOrDefault(f => f.Symbol == symbol);

            if (fruit != null)
            {
                return fruit;
            }

            throw new ArgumentException($"No fruit with such a symbol: {symbol} is placed on the game board!");
        }

        IQueryable<IFruit> IFruitRepository.GetAll()
        {
            return this._fruitsPlacedOnGameBoard.AsQueryable();
        }
    }
}