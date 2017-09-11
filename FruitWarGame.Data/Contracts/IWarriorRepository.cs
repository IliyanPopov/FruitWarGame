namespace FruitWarGame.Data.Contracts
{
    using System.Linq;
    using Models.Contracts.Warriors;

    public interface IWarriorRepository
    {
        void AddWarrior(IWarrior warrior);

        IWarrior GetWarriorBySymbol(char symbol);

        IQueryable<IWarrior> GetAll();
    }
}