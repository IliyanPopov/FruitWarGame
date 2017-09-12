namespace FruitWarGame.Logic.Contracts
{
    using System.Collections.Generic;

    public interface IGameInitializationStrategy
    {
        void Initialize(IDictionary<char, int> warriorTypes);
    }
}