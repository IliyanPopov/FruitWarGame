namespace FruitWarGame.Models.Warriors
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;

    public abstract class AbstractWarrior : IWarrior
    {
        public IEnumerable<IBonusPowerPointsProvider> BonusToPowerPointsProviders;

        public IEnumerable<IBonusSpeedPointsProvider> BonusToSpeedPointsProviders;

        protected AbstractWarrior(int speedPoints, int powerPoints)
        {
            this.SpeedPoints = speedPoints;
            this.PowerPoints = powerPoints;
            this.BonusToSpeedPointsProviders = new List<IBonusSpeedPointsProvider>();
            this.BonusToPowerPointsProviders = new List<IBonusPowerPointsProvider>();
        }

        protected int SpeedPoints { get; }
        protected int PowerPoints { get; }

        public int TotalSpeedPoints
        {
            get { return this.SpeedPoints + this.BonusToSpeedPointsProviders.Sum(b => b.SpeedPointsBonus); }
        }

        public int TotalPowerPoints
        {
            get { return this.PowerPoints + this.BonusToPowerPointsProviders.Sum(b => b.PowerPointsBonus); }
        }
    }
}