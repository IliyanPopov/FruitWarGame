namespace FruitWarGame.Models.Fruits
{
    using Contracts.Fruits;

    public class Pear : AbstractFruit, IFruit
    {
        private const char PearSymbol = 'P';
        private const int PearPowerPointBonusValue = 0;
        private const int PearSpeedPointBonusValue = 1;

        public Pear()
            : base(PearSpeedPointBonusValue, PearPowerPointBonusValue, PearSymbol)
        {
        }
    }
}