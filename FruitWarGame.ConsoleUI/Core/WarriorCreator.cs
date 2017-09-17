namespace FruitWarGame.ConsoleUI.Core
{
    using System;
    using System.Threading;
    using Contracts.ConsoleIO;
    using Contracts.Core;
    using Logic.Contracts;
    using Models.Contracts.Factories;
    using Models.Contracts.Warriors;


    public class WarriorCreator : IWarriorCreator
    {
        private readonly IReader _reader;
        private readonly IRenderer _renderer;
        private readonly IWarriorFactory _warriorFactory;
        private readonly IWriter _writer;

        public WarriorCreator(IRenderer renderer, IWriter writer, IReader reader, IWarriorFactory warriorFactory)
        {
            this._renderer = renderer;
            this._writer = writer;
            this._reader = reader;
            this._warriorFactory = warriorFactory;
        }

        public IWarrior CreateWarrior(char warriorSymbol, string playerCreationMessage,
            string availableWarriorsMessage)
        {
            IWarrior playerWarrior = null;

            while (playerWarrior == null)
            {
                this._renderer.Clear();
                this._writer.WriteLine(playerCreationMessage);
                this._writer.WriteLine(availableWarriorsMessage);

                try
                {
                    int playerWarriorTpye;
                    bool isParseSuccessfull = int.TryParse(this._reader.ReadLine(), out playerWarriorTpye);
                    if (!isParseSuccessfull)
                    {
                        throw new ArgumentException();
                    }
                    playerWarrior = this._warriorFactory.CreateWarrior(warriorSymbol, playerWarriorTpye);
                }

                catch (NotImplementedException e)
                {
                    Console.WriteLine($"{e.Message}");
                    Thread.Sleep(2000);
                    this._renderer.Clear();
                }

                catch (ArgumentException)
                {
                    Console.WriteLine($"Wrong input!");
                    Thread.Sleep(2000);
                    this._renderer.Clear();
                }
            }

            return playerWarrior;
        }
    }
}