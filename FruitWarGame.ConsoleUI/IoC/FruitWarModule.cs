namespace FruitWarGame.ConsoleUI.IoC
{
    using ConsoleIO;
    using Contracts.ConsoleIO;
    using Contracts.Core;
    using Core;
    using Data.Contracts;
    using Data.Repositories;
    using Logic.Concrete;
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
            this.Bind<IEngine>().To<Engine>().InSingletonScope();
            this.Bind<IRenderer>().To<ConsoleRenderer>().InSingletonScope();
            this.Bind<IWriter>().To<ConsoleWriter>().InSingletonScope();
            this.Bind<IReader>().To<ConsoleReader>().InSingletonScope();

            this.Bind<IFruitRepository>().To<FruitRepository>().InSingletonScope();
            this.Bind<IWarriorRepository>().To<WarriorRepository>().InSingletonScope();

            this.Bind<IGameInitializationStrategy>().To<GameInitializationStrategy>().InSingletonScope();
            this.Bind<ISpawningValidator>().To<SpawningValidator>().InSingletonScope();

            this.Bind<IGameGrid>().To<GameGrid>().InSingletonScope();
            this.Bind<IPosition>().To<Position>().InSingletonScope();
            this.Bind<IFruitFactory>().To<FruitFactory>().InSingletonScope();
            this.Bind<IWarriorFactory>().To<WarriorFactory>().InSingletonScope();
            this.Bind<IWarriorCreator>().To<WarriorCreator>().InSingletonScope();
        }
    }
}