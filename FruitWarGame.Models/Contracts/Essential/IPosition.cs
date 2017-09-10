namespace FruitWarGame.Models.Contracts.Essential
{
    public interface IPosition
    {
        int CurrentRow { get; set; }

        int CurrentCol { get; set; }
    }
}