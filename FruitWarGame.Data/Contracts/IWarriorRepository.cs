namespace FruitWarGame.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using Models.Contracts.Warriors;

    public interface IWarriorRepository : IEnumerable<IWarrior>
    {
        void AddWarrior(IWarrior warrior);

        IWarrior GetWarriorBySymbol(char symbol);

        IWarrior GetWarriorByPosition(int positionX, int positionY);

        IQueryable<IWarrior> GetAll();

        void RemoveAll();
    }
}