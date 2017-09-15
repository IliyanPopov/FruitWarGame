namespace FruitWarGame.Models.Factories
{
    using System;
    using Contracts.Factories;
    using Contracts.Warriors;
    using Warriors;

    public class WarriorFactory : IWarriorFactory
    {
        public IWarrior CreateWarrior(char playerSymbol, int warriorType)
        {
            switch (warriorType)
            {
                case 1:
                    IWarrior turtle = new Turtle(playerSymbol);
                    return turtle;
                case 2:
                    IWarrior monkey = new Monkey(playerSymbol);
                    return monkey;
                case 3:
                    IWarrior pigeon = new Pigeon(playerSymbol);
                    return pigeon;
                default:
                    throw new NotImplementedException("Weapon not implemented!");
            }
        }
    }
}