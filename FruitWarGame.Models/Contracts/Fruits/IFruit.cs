namespace FruitWarGame.Models.Contracts.Fruits
{
    using Essential;

    public interface IFruit : IBonusPowerPointsProvider, IBonusSpeedPointsProvider
    {
        IPosition CurrentPosition { get; set; }

        char Symbol { get; }

        bool IsEaten { get; set; }
    }
}