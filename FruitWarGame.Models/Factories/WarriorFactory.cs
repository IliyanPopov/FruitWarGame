namespace FruitWarGame.Models.Factories
{
    using System;
    using Contracts.Warriors;
    using Warriors;

    public class WarriorFactory : IWarriorFactory
    {
        public IWarrior CreateWarrior(char playerSymbol, int warriorType)
        {
            switch (warriorType)
            {
                case 1:
                    IWarrior monkey = new Monkey(playerSymbol);
                    return monkey;
                case 2:
                    IWarrior pigeon = new Pigeon(playerSymbol);
                    return pigeon;
                case 3:
                    IWarrior turtle = new Turtle(playerSymbol);
                    return turtle;
                default:
                    throw new NotImplementedException("Weapon not implemented!");
            }
        }
    }
}