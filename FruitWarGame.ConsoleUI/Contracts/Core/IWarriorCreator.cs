namespace FruitWarGame.ConsoleUI.Contracts.Core
{
    using Models.Contracts.Warriors;

    public interface IWarriorCreator
    {
        IWarrior CreateWarrior(char warriorSymbol, string playerCreationMessage, string availableWarriorsMessage);
    }
}