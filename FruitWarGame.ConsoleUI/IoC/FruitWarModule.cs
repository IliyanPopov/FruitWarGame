namespace FruitWarGame.ConsoleUI.IoC
{
    using ConsoleIO;
    using ConsoleIO.Contracts;
    using Data.Contracts;
    using Data.Repositories;
    using Logic;
    using Logic.Contracts;
    using Models.Contracts.Essential;
    using Models.Contracts.Factories;
    using Models.Essential;
    using Models.Factories;
    using Ninject.Modules;

    public class FruitWarModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IEngine>().To<Engine>();
            Bind<IRenderer>().To<ConsoleRenderer>();
            Bind<IWriter>().To<ConsoleWriter>();
            Bind<IReader>().To<ConsoleReader>();

            Bind<IFruitRepository>().To<FruitRepository>();
            Bind<IWarriorRepository>().To<WarriorRepository>();

            Bind<IGameInitializationStrategy>().To<GameInitializationStrategy>();

            Bind<IGameGrid>().To<GameGrid>();
            Bind<IPosition>().To<Position>();
            Bind<IFruitFactory>().To<FruitFactory>();
            Bind<IWarriorFactory>().To<WarriorFactory>();
        }
    }
}