namespace FruitWarGame.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Models.Contracts.Warriors;

    public class InMemoryPlayerRepository : IRepository<IWarrior>
    {
        private readonly HashSet<IWarrior> _players;

        public InMemoryPlayerRepository()
        {
            this._players = new HashSet<IWarrior>();
        }

        public void Add(IWarrior player)
        {
            this._players.Add(player);
        }

        public IWarrior GetBySymbol(char symbol)
        {
            var warrior = this._players.FirstOrDefault(w => w.PlayerSymbol == symbol);

            if (warrior != null)
            {
                return warrior;
            }

            throw new ArgumentException($"Player with symbol: {symbol} does not exist!");
        }

        public ICollection<IWarrior> GetAll()
        {
            return this._players;
        }
    }
}