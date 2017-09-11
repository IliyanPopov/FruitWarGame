namespace FruitWarGame.Models.Fruits
{
    using Contracts.Fruits;

    public class Apple : IFruit
    {
        private const char AppleSymbol = 'A';
        private const int ApplePowerPointBonusValue = 1;
        private const int AppleSpeedPointBonusValue = 0;

        public Apple()
        {
            this.Symbol = AppleSymbol;
        }

        public int PowerPointsBonus => ApplePowerPointBonusValue;

        public int SpeedPointsBonus => AppleSpeedPointBonusValue;
        public char Symbol { get; }
        public bool IsEaten { get; }
    }
}