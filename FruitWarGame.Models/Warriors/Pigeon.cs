namespace FruitWarGame.Models.Warriors
{
    using Contracts;

    public class Pigeon : AbstractWarrior, IWarrior, ISymbol
    {
        private const int InitialSpeedPoints = 3;
        private const int InitialPowerPoints = 1;

        public Pigeon(char playerSymbol) : base(InitialSpeedPoints, InitialPowerPoints)
        {
            this.PlayerSymbol = playerSymbol;
        }

        public char PlayerSymbol { get; }
    }
}