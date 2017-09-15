namespace FruitWarGame.Models.Warriors
{
    using Contracts;
    using Contracts.Warriors;

    public class Turtle : AbstractWarrior, IWarrior
    {
        private const int InitialSpeedPoints = 1;
        private const int InitialPowerPoints = 3;

        public Turtle(char symbol) : base(InitialSpeedPoints, InitialPowerPoints,symbol)
        {
        }
    }
}