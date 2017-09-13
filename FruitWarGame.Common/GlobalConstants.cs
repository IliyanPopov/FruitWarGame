namespace FruitWarGame.Common
{
    public static class GlobalConstants
    {
        public const char Player1Symbol = '1';
        public const char Player2Symbol = '2';

        public const char AppleSymbol = 'A';
        public const char PearSymbol = 'P';

        public const int GameGridRowsCount = 8;
        public const int GameGridColsCount = 8;


        // for 1 position difference i  = 9
        // for 2 position difference i  = 25
        // for 3 position difference i  = 49
        // for 4 position difference i  = 81
        public const int OnePositionsApartFromEatchother = 9;
        public const int TwoPositionsApartFromEatchother = 25;
        public const int ThreePositionsApartFromEatchother = 49;
    }
}