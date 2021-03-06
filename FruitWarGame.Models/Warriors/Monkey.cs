﻿namespace FruitWarGame.Models.Warriors
{
    using Contracts;
    using Contracts.Warriors;

    public class Monkey : AbstractWarrior, IWarrior
    {
        private const int InitialSpeedPoints = 2;
        private const int InitialPowerPoints = 2;

        public Monkey(char symbol) : base(InitialSpeedPoints, InitialPowerPoints, symbol)
        {
        }
    }
}