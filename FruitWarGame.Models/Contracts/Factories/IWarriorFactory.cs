namespace FruitWarGame.Models.Contracts.Factories
{
    using Warriors;

    public interface IWarriorFactory
    {
        IWarrior CreateWarrior(char playerSymbol, int warriorType);
    }
}