namespace FruitWarGame.Models.Fruits
{
    using Contracts.Essential;
    using Contracts.Fruits;

    public abstract class AbstractFruit : IFruit
    {
        protected AbstractFruit(int speedPointsBonus, int powerPointsBonus, char symbol)
        {
            this.SpeedPointsBonus = speedPointsBonus;
            this.PowerPointsBonus = powerPointsBonus;
            this.Symbol = symbol;
        }

        public int PowerPointsBonus { get; }

        public int SpeedPointsBonus { get; }

        public char Symbol { get; }

        public bool IsEaten { get; set; }

        public IPosition CurrentPosition { get; set; }
    }
}