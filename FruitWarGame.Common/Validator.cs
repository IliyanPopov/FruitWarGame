namespace FruitWarGame.Common
{
    using System;

    public static class Validator
    {
        public static void Validate(int value, int min, int max, string errorMessage)
        {
            if (value < min || value >= max)
            {
                throw new IndexOutOfRangeException(errorMessage);
            }
        }

        public static bool Validate(int value, int min, int max)
        {
            if (value < min || value >= max)
            {
                return false;
            }

            return true;
        }
    }
}