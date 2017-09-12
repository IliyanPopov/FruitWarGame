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
            Bind<IEngine>().To<Engine>().InSingletonScope();
            Bind<IRenderer>().To<ConsoleRenderer>().InSingletonScope();
            Bind<IWriter>().To<ConsoleWriter>().InSingletonScope();
            Bind<IReader>().To<ConsoleReader>().InSingletonScope();

            Bind<IFruitRepository>().To<FruitRepository>().InSingletonScope();
            Bind<IWarriorRepository>().To<WarriorRepository>().InSingletonScope();

            Bind<IGameInitializationStrategy>().To<GameInitializationStrategy>().InSingletonScope();

            Bind<IGameGrid>().To<GameGrid>().InSingletonScope();
            Bind<IPosition>().To<Position>().InSingletonScope();
            Bind<IFruitFactory>().To<FruitFactory>().InSingletonScope();
            Bind<IWarriorFactory>().To<WarriorFactory>().InSingletonScope();
        }
    }
}