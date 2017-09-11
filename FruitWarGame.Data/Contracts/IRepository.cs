using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitWarGame.Data.Contracts
{
    using Models.Contracts.Warriors;

    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        T GetBySymbol(char symbol);

        ICollection<T> GetAll();
    }
}
