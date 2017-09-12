namespace FruitWarGame.Models.Contracts.Warriors
{
    using Essential;

    public interface IWarrior
    {
        IPosition CurrentPosition { get; set; }

        int TotalSpeedPoints { get; }

        int TotalPowerPoints { get; }

        char PlayerSymbol { get; }
    }
}