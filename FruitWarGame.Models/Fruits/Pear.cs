namespace FruitWarGame.Models.Fruits
{
    using Contracts.Fruits;

    public class Pear : IFruit
    {
        private const int PearPowerPointBonusValue = 0;
        private const int PearSpeedPointBonusValue = 1;

        public Pear(char fruitSymbol)
        {
            this.Symbol = fruitSymbol;
        }

        public int PowerPointsBonus => PearPowerPointBonusValue;

        public int SpeedPointsBonus => PearSpeedPointBonusValue;
        public char Symbol { get; }
    }
}