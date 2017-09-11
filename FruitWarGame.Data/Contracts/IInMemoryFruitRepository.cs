namespace FruitWarGame.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using Models.Contracts.Fruits;

    public interface IInMemoryFruitRepository
    {
        void AddFruit(IFruit fruit);

        IFruit GetFruitBySymbol(char symbol);

        IQueryable<IFruit> GetAll();
    }
}