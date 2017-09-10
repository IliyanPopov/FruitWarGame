﻿namespace FruitWarGame.Models.Warriors
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts.Fruits;
    using Contracts.Warriors;

    public abstract class AbstractWarrior : IWarrior
    {
        private readonly ICollection<IFruit> _eatenFruits;


        protected AbstractWarrior(int speedPoints, int powerPoints)
        {
            this.SpeedPoints = speedPoints;
            this.PowerPoints = powerPoints;
            this._eatenFruits = new List<IFruit>();
        }

        protected int SpeedPoints { get; }

        protected int PowerPoints { get; }

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
    }
}