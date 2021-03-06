﻿namespace FruitWarGame.Data.Repositories
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Models.Contracts.Fruits;

    public sealed class FruitRepository : IFruitRepository
    {
        private readonly HashSet<IFruit> _fruits;

        public FruitRepository()
        {
            this._fruits = new HashSet<IFruit>();
        }

        public void AddFruit(IFruit fruit)
        {
            this._fruits.Add(fruit);
        }

        public IFruit GetFruitBySymbol(char symbol)
        {
            var fruit = this._fruits.FirstOrDefault(f => f.Symbol == symbol);

            if (fruit != null)
            {
                return fruit;
            }

            throw new ArgumentException($"No fruit with such a symbol: {symbol} is placed on the game board!");
        }

        public IFruit GetFruitByPosition(int positionX, int positionY)
        {
            var fruit = this._fruits.FirstOrDefault(f => f.CurrentPosition.Row == positionX &&
                                                         f.CurrentPosition.Col == positionY);

            if (fruit != null)
            {
                return fruit;
            }

            throw new ArgumentException($"No fruit on board with positionX: {positionX}, positionY: {positionY}");
        }

        IQueryable<IFruit> IFruitRepository.GetAll()
        {
            return this._fruits.AsQueryable();
        }

        public void RemoveAll()
        {
            this._fruits.Clear();
        }

        public IEnumerator<IFruit> GetEnumerator()
        {
            return this._fruits.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}