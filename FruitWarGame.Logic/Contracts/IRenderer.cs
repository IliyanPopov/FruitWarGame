namespace FruitWarGame.Logic.Contracts
{
    using Models.Contracts.Essential;

    public interface IRenderer
    {
        void RenderGrid();

        void UpdateGrid(IPosition position, char value);

        void Clear();

        void ClearGridFromSymbols(char[] symbolsToRemoveFromGrid);
    }
}