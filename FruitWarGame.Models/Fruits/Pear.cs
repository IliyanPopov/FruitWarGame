namespace FruitWarGame.Models.Fruits
{
    using Contracts.Essential;
    using Contracts.Fruits;

    public class Pear : IFruit
    {
        private const char PearSymbol = 'P';
        private const int PearPowerPointBonusValue = 0;
        private const int PearSpeedPointBonusValue = 1;

        public Pear(IPosition position)
        {
            this.Symbol = PearSymbol;
            this.CurrentPosition = position;
        }

        public int PowerPointsBonus => PearPowerPointBonusValue;

        public int SpeedPointsBonus => PearSpeedPointBonusValue;

        public char Symbol { get; }

        public bool IsEaten { get; }

        public IPosition CurrentPosition { get; set; }
    }
}