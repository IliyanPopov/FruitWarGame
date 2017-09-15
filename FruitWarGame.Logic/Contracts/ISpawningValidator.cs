namespace FruitWarGame.Logic.Contracts
{
    using Models.Contracts.Essential;

    public interface ISpawningValidator
    {
        bool ValidateFruitSpawningPosition(IPosition placableEntity, PlacableEntities entityType,
            int movesApartFromEachother);
    }
}