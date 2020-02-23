using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Drills.TennisMatch.Component
{
    /// <summary>
    /// Sport scores are naturally sequential so it makes sense to model them
    /// with a Next operation and an exposing them with IEnumerable.
    /// </summary>
    /// <typeparam name="T">Type of a score artifact in state Finished</typeparam>
    public class TrailingScore<T>
    {
        private readonly IImmutableList<T> _trail;
        public IEnumerable<T> Trail { get => _trail; }

        private readonly NumericScore _score;

        public NumericScore Total { get => _score; }

        public TrailingScore() : this(ImmutableList<T>.Empty, new NumericScore()) { }

        // Previous IList replaced with plain IImmutableList.
        // I believe the internals of ImmutableList are based on array copying so performance-wise, not a great choice.
        // TODO: understand the internal implementation of ImmutableList and decide if it is a good choice.
        // If not, replace it with a custom immutable linked list.
        protected TrailingScore(IImmutableList<T> trail, NumericScore score)
        {
            _trail = trail;
            _score = score;
        }

        public TrailingScore<T> Next(T finished, bool p1Wins) => new TrailingScore<T>(_trail.Add(finished), Total.Next(p1Wins));
    }
}