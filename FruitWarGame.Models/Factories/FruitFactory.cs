namespace FruitWarGame.Models.Factories
{
    using System;
    using Contracts.Essential;
    using Contracts.Factories;
    using Contracts.Fruits;
    using Fruits;

    public class FruitFactory : IFruitFactory
    {
        private readonly IPosition _position;

        public FruitFactory(IPosition position)
        {
            this._position = position;
        }

        public IFruit CreateFruit(char fruitSymbol)
        {
            switch (fruitSymbol)
            {
                case 'A':
                    IFruit apple = new Apple();
                    return apple;
                case 'P':
                    IFruit pear = new Pear();
                    return pear;

                default:
                    throw new NotImplementedException("Fruit not implemented!");
            }
        }
    }
}