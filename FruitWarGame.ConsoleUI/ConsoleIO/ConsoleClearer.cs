namespace FruitWarGame.ConsoleUI.ConsoleIO
{
    using System;
    using Contracts;

    public class ConsoleClearer : IClearer
    {
        public void Clear()
        {
            Console.Clear();
        }
    }
}
