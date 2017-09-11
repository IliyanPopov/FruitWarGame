namespace FruitWarGame.Models.Fruits
{
    using Contracts.Essential;
    using Contracts.Fruits;

    public class Apple : IFruit
    {
        private const char AppleSymbol = 'A';
        private const int ApplePowerPointBonusValue = 1;
        private const int AppleSpeedPointBonusValue = 0;

        public Apple(IPosition position)
        {
            this.Symbol = AppleSymbol;
            this.CurrentPosition = position;
        }

        public int PowerPointsBonus => ApplePowerPointBonusValue;

        public int SpeedPointsBonus => AppleSpeedPointBonusValue;
        public char Symbol { get; }

        public bool IsEaten { get; }

        public IPosition CurrentPosition { get; set; }
    }
}