namespace FruitWarGame.Models.Warriors
{
    using Contracts;
    using Contracts.Warriors;

    public class Pigeon : AbstractWarrior, IWarrior
    {
        private const int InitialSpeedPoints = 3;
        private const int InitialPowerPoints = 1;

        public Pigeon(char symbol) : base(InitialSpeedPoints, InitialPowerPoints, symbol)
        {
        }
    }
}