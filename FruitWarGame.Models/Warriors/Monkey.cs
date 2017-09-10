namespace FruitWarGame.Models.Warriors
{
    using Contracts;

    public class Monkey : AbstractWarrior, IWarrior, ISymbol
    {
        private const int InitialSpeedPoints = 2;
        private const int InitialPowerPoints = 2;

        public Monkey(char playerSymbol) : base(InitialSpeedPoints, InitialPowerPoints)
        {
            this.PlayerSymbol = playerSymbol;
        }

        public char PlayerSymbol { get; }
    }
}