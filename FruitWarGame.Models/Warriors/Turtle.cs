namespace FruitWarGame.Models.Warriors
{
    using Contracts;
    using Contracts.Warriors;

    public class Turtle : AbstractWarrior, IWarrior, ISymbol
    {
        private const int InitialSpeedPoints = 1;
        private const int InitialPowerPoints = 3;

        public Turtle(char playerSymbol) : base(InitialSpeedPoints, InitialPowerPoints)
        {
            this.PlayerSymbol = playerSymbol;
        }

        public char PlayerSymbol { get; }
    }
}