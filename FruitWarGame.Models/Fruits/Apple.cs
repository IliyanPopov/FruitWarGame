namespace FruitWarGame.Models.Fruits
{
    using Contracts;

    public class Apple : IBonusPowerPointsProvider
    {
        private const int PowerPointBonusValue = 1;

        public int PowerPointsBonus => PowerPointBonusValue;
    }
}