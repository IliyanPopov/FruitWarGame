namespace FruitWarGame.Models.Warriors
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Contracts.Essential;
    using Contracts.Fruits;
    using Contracts.Warriors;
    using Essential;

    public abstract class AbstractWarrior : IWarrior
    {
        private readonly ICollection<IFruit> _eatenFruits;

        protected AbstractWarrior(int speedPoints, int powerPoints,char symbol)
        {
            this.SpeedPoints = speedPoints;
            this.PowerPoints = powerPoints;
            this._eatenFruits = new List<IFruit>();
            this.Symbol = symbol;
        }

        protected int SpeedPoints { get; }

        protected int PowerPoints { get; }

        public char Symbol { get; }

        public IPosition CurrentPosition { get; set; }

        public int TotalSpeedPoints
        {
            get { return this.SpeedPoints + this._eatenFruits.Sum(b => b.SpeedPointsBonus); }
        }

        public int TotalPowerPoints
        {
            get { return this.PowerPoints + this._eatenFruits.Sum(b => b.PowerPointsBonus); }
        }

        public void EatFruit(IFruit fruit)
        {
            if (fruit != null)
            {
                this._eatenFruits.Add(fruit);
            }
        }

        public void MakeTurn(IGameGrid grid)
        {
            
        }
    }
}