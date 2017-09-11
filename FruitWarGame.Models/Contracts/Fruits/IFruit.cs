namespace FruitWarGame.Models.Contracts.Fruits
{
    using Essential;

    public interface IFruit : IBonusPowerPointsProvider, IBonusSpeedPointsProvider
    {
        char Symbol { get; }

        bool IsEaten { get; }

        IPosition CurrentPosition { get; set; }
    }
}