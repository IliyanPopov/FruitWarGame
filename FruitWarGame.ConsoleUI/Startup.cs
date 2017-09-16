namespace FruitWarGame.ConsoleUI
{
    using Core;
    using IoC;
    using Ninject;

    public class Startup
    {
        public static void Main()
        {
            var kernel = new StandardKernel(new FruitWarModule());
            var engine = kernel.Get<Engine>();

            engine.Run();
        }
    }
}