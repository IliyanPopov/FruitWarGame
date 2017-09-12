namespace FruitWarGame.ConsoleUI.ConsoleIO.Contracts
{
    public interface IWriter
    {
        void Write(char text);

        void WriteLine(string text);

        void Clear();
    }
}
