namespace FruitWarGame.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Models.Contracts.Warriors;

    public class InMemoryWarriorRepository : IInMemoryWarriorRepository
    {
        private readonly HashSet<IWarrior> _warriors;

        public InMemoryWarriorRepository()
        {
            this._warriors = new HashSet<IWarrior>();
        }

        public void AddWarrior(IWarrior warrior)
        {
            this._warriors.Add(warrior);
        }

        public IWarrior GetWarriorBySymbol(char symbol)
        {
            var warrior = this._warriors.FirstOrDefault(w => w.PlayerSymbol == symbol);

            if (warrior != null)
            {
                return warrior;
            }

            throw new ArgumentException($"Player with symbol: {symbol} does not exist!");
        }

        IQueryable<IWarrior> IInMemoryWarriorRepository.GetAll()
        {
            return this._warriors.AsQueryable();
        }
    }
}