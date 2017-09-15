namespace FruitWarGame.Models.Contracts.Warriors
{
    using Essential;
    using Fruits;

    public interface IWarrior
    {
        IPosition CurrentPosition { get; set; }

        int TotalSpeedPoints { get; }

        int TotalPowerPoints { get; }

        char Symbol { get; }

        void EatFruit(IFruit fruit);
    }
}