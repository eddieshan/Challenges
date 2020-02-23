using System;
using System.Linq;
using System.Collections.Generic;

namespace Drills.TennisMatch.Component
{
    public abstract class LiveState<TLive, TWon>
    {
        private readonly TrailingScore<TWon> _scores;

        public TrailingScore<TWon> Scores { get => _scores; }

        private TLive _current;

        protected TLive Current { get => _current; }

        protected LiveState(TLive current, TrailingScore<TWon> history)
        {
            _current = current;
            _scores = history;
        }

        protected LiveState(TLive current): this(current, new TrailingScore<TWon>()) { }
    }

    public abstract class WonState<TWon>
    {
        private readonly IEnumerable<TWon> _scores;
        private readonly NumericScore _score;

        public IEnumerable<TWon> Scores { get => _scores; }
        public NumericScore Total { get => _score; }

        protected WonState(TrailingScore<TWon> scores)
        {
            _scores = scores.Trail;
            _score = scores.Total;
        }
    }
}