namespace FruitWarGame.Models.Factories
{
    using System;
    using Contracts.Fruits;
    using Fruits;

    public class FruitFactory
    {
        public IFruit CreateFruit(char fruitSymbol)
        {
            switch (fruitSymbol)
            {
                case 'A':
                    IFruit apple = new Apple(fruitSymbol);
                    return apple;
                case 'P':
                    IFruit pear = new Pear(fruitSymbol);
                    return pear;

                default:
                    throw new NotImplementedException("Fruit not implemented!");
            }
        }
    }
}