namespace FruitWarGame.ConsoleUI.Contracts.ConsoleIO
{
    public interface IWriter
    {
        void Write(char text);

        void WriteLine(string text);
    }
}
