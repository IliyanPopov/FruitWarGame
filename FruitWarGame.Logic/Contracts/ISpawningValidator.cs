namespace FruitWarGame.Logic.Contracts
{
    using Concrete;
    using Models.Contracts.Essential;

    public interface ISpawningValidator
    {
        bool ValidateSpawningPosition(IPosition placableEntity, PlacableEntities entityType, int movesApartFromEachother);
    }
}