namespace FruitWarGame.ConsoleUI.ConsoleIO
{
    using System;
    using ConsoleUI.Contracts.ConsoleIO;

    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            var text = Console.ReadLine();

            return text;
        }
    }
}
