using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Drills.TennisMatch.Component
{
    public class ScorePair<T>
    {
        private T _p1;
        private T _p2;

        public T P1 { get => _p1; }
        public T P2 { get => _p2; }

        public ScorePair(T player1, T player2)
        {
            _p1 = player1;
            _p2 = player2;
        }

        public T Choose(bool isPlayer1) => isPlayer1 ? P1 : P2;
    }
}