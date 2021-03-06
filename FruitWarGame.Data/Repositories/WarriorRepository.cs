﻿namespace FruitWarGame.Data.Repositories
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Models.Contracts.Warriors;

    public sealed class WarriorRepository : IWarriorRepository
    {
        private readonly HashSet<IWarrior> _warriors;

        public WarriorRepository()
        {
            this._warriors = new HashSet<IWarrior>();
        }

        public void AddWarrior(IWarrior warrior)
        {
            this._warriors.Add(warrior);
        }

        public IWarrior GetWarriorBySymbol(char symbol)
        {
            var warrior = this._warriors.FirstOrDefault(w => w.Symbol == symbol);

            if (warrior != null)
            {
                return warrior;
            }

            throw new ArgumentException($"Player with symbol: {symbol} does not exist!");
        }

        public IWarrior GetWarriorByPosition(int positionX, int positionY)
        {
            var warrior = this._warriors.FirstOrDefault(w => w.CurrentPosition.Row == positionY &&
                                                             w.CurrentPosition.Col == positionX);

            if (warrior != null)
            {
                return warrior;
            }

            throw new ArgumentException($"No warrior on board with positionX: {positionX}, positionY: {positionY}");
        }

        IQueryable<IWarrior> IWarriorRepository.GetAll()
        {
            return this._warriors.AsQueryable();
        }

        public void RemoveAll()
        {
            this._warriors.Clear();
        }

        public IEnumerator<IWarrior> GetEnumerator()
        {
            return this._warriors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}