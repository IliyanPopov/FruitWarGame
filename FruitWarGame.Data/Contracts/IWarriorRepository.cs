namespace FruitWarGame.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using Models.Contracts.Warriors;

    public interface IWarriorRepository : IEnumerable<IWarrior>
    {
        void AddWarrior(IWarrior warrior);

        IWarrior GetWarriorBySymbol(char symbol);

        IQueryable<IWarrior> GetAll();
    }
}