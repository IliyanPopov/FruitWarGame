namespace FruitWarGame.Models.Contracts.Factories
{
    using Fruits;

    public interface IFruitFactory
    {
        IFruit CreateFruit(char fruitSymbol);
    }
}