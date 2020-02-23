using System;
using System.Collections.Generic;

namespace Drills.TennisMatch.Desktop.ViewModels
{
    public static class TennisLiterals
    {
        public const string Love = "LO";
        public const string P15 = "15";
        public const string P30 = "30";
        public const string P40 = "40";
        public const string Deuce = "DE";
        public const string Advantage = "AD";
        public const string Disadvantage = "-";

        public const string Tiebreak = "TB";
        public const string EqualGames = "-";

        public const string P1 = "P1";
        public const string P2 = "P2";

        public const string P1Wins = "P1  wins";
        public const string P2Wins = "P2  wins";

        private static IDictionary<int, string> _Points = new Dictionary<int, string>
        {
            { 0, Love },
            { 1, P15 },
            { 2, P30 },
            { 3, P40 },
        };

        public static string GetPointLiteral(int value) => _Points[value];
    }
}
