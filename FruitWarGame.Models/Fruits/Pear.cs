namespace FruitWarGame.Models.Fruits
{
    using Contracts;

    public class Pear : IBonusSpeedPointsProvider
    {
        private const int SpeedPointBonusValue = 1;

        public int SpeedPointsBonus => SpeedPointBonusValue;
    }
}