using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Drills.TennisMatch.Component
{
    public class NumericScore: ScorePair<int>
    {
        public NumericScore(): base(0, 0) { }

        private NumericScore(int player1, int player2): base(player1, player2) { }

        public bool P1Wins() => P1 > P2;

        public NumericScore Next(bool p1Wins) => p1Wins? new NumericScore(P1 + 1, P2) : new NumericScore(P1, P2 + 1);
    }
}