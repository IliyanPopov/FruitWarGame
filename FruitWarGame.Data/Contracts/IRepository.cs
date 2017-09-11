using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitWarGame.Data.Contracts
{
    using Models.Contracts.Warriors;

    public interface IRepository
    {
        void AddPlayer(IWarrior player);

        IWarrior GetPlayerBySymbol(char symbol);

        ICollection<IWarrior> GetAll();
    }
}
