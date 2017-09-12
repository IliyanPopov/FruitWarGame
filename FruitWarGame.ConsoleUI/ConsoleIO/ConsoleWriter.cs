namespace FruitWarGame.ConsoleUI.ConsoleIO
{
    using System;
    using Contracts;

    public class ConsoleWriter : IWriter
    {
        public void Write(char text)
        {
            Console.Write(text);
        }

        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
