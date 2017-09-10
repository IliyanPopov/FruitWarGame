namespace FruitWarGame.Models.Fruits
{
    using Contracts.Fruits;

    public class Apple : IFruit
    {
        private const int ApplePowerPointBonusValue = 1;
        private const int AppleSpeedPointBonusValue = 0;

        public Apple(char fruitSymbol)
        {
            this.Symbol = fruitSymbol;
        }

        public int PowerPointsBonus => ApplePowerPointBonusValue;

        public int SpeedPointsBonus => AppleSpeedPointBonusValue;
        public char Symbol { get; }
    }
}