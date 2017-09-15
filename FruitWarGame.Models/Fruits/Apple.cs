namespace FruitWarGame.Models.Fruits
{
    using Contracts.Fruits;

    public class Apple : AbstractFruit, IFruit
    {
        private const char AppleSymbol = 'A';
        private const int ApplePowerPointBonusValue = 1;
        private const int AppleSpeedPointBonusValue = 0;

        public Apple()
            : base(AppleSpeedPointBonusValue, ApplePowerPointBonusValue, AppleSymbol)
        {
        }
    }
}