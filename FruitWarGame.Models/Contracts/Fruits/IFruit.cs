namespace FruitWarGame.Models.Contracts.Fruits
{
    public interface IFruit : IBonusPowerPointsProvider, IBonusSpeedPointsProvider
    {
        char Symbol { get; }

        bool IsEaten { get; }
    }
}