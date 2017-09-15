namespace FruitWarGame.Logic
{
    using Common;
    using Contracts;
    using Models.Contracts.Essential;

    public class SpawningValidator : ISpawningValidator
    {
        private readonly IGameGrid _grid;

        public SpawningValidator(IGameGrid grid)
        {
            this._grid = grid;
        }

        public bool ValidateFruitSpawningPosition(IPosition placableEntity, PlacableEntities entityType,
            int movesApartFromEachother)
        {
            int direction = 0; // The initial direction is "down"
            int stepsCount = 1; // Perform 1 step in current direction
            int stepPosition = 0; // 0 steps already performed
            int stepChange = 0; // Steps count changes after 2 steps
            int positionX = placableEntity.Row;
            int positionY = placableEntity.Col;

            if (this._grid[positionX, positionY] != GlobalConstants.GridDefaultSymbol)
            {
                return false;
            }

            for (int i = 0; i < movesApartFromEachother; i++)
            {
                if (i == 0)
                {
                    stepPosition++;
                }

                else
                {
                    if (positionX >= 0 && positionX <= this._grid.Rows - 1 &&
                        positionY >= 0 && positionY <= this._grid.Cols - 1)
                    {
                        switch (entityType)
                        {
                            case PlacableEntities.Fruit:
                                if (this._grid[positionX, positionY] == GlobalConstants.AppleSymbol ||
                                    this._grid[positionX, positionY] == GlobalConstants.PearSymbol)
                                {
                                    return true;
                                }
                                break;
                            case PlacableEntities.Warrior:
                                if (
                                    this._grid[positionX, positionY] == GlobalConstants.Player1Symbol ||
                                    this._grid[positionX, positionY] == GlobalConstants.Player2Symbol)
                                {
                                    return true;
                                }
                                break;
                        }
                    }

                    // Check for direction / step changes
                    if (stepPosition < stepsCount)
                    {
                        stepPosition++;
                    }
                    else
                    {
                        stepPosition = 1;
                        if (stepChange == 1)
                        {
                            stepsCount++;
                        }
                        stepChange = (stepChange + 1) % 2;
                        direction = (direction + 1) % 4;
                    }
                }

                // Move to the next cell in the current direction
                switch (direction)
                {
                    case 0:
                        positionY++;
                        break;
                    case 1:
                        positionX--;
                        break;
                    case 2:
                        positionY--;
                        break;
                    case 3:
                        positionX++;
                        break;
                }
            }

            return false;
        }
    }
}