using System;
using System.Collections.Immutable;

using Drills.TennisMatch.Core;

namespace Drills.TennisMatch.Component
{
    using PointsList = IImmutableList<PointScore>;

    public sealed class Game : Union<LiveGame, WonGame>
    {
        public Game(LiveGame game) : base(game) { }
        public Game(WonGame game) : base(game) { }
    }

    public abstract class BaseGame
    {
        private readonly bool _p1Serves;

        private readonly PointsList _points;

        // Previous IList replaced with plain IImmutableList.
        // I believe the internals of ImmutableList are based on array copying so performance-wise, not a great choice.
        // TODO: understand the internal implementation of ImmutableList and decide if it is a good choice.
        // If not, replace it with a custom immutable linked list.
        public PointsList Scores { get => _points; }

        public bool P1Serves { get => _p1Serves; }

        protected BaseGame(bool p1Serves, IImmutableList<PointScore> scores)
        {
            _p1Serves = p1Serves;
            _points = scores;
        }
    }

    public sealed class LiveGame : BaseGame
    {
        private readonly PointScore _current;

        public PointScore Points { get => _current; }

        public static LiveGame Start(bool p1Serves) => new LiveGame(p1Serves);

        private LiveGame(bool p1Serves): this(PointScore.LoveAll, ImmutableList<PointScore>.Empty, p1Serves) { }

        private LiveGame(PointScore current, PointsList scores, bool p1Serves): base(p1Serves, scores) => _current = current;

        private Game NextLive(PointScore points) => new Game(new LiveGame(points, base.Scores.Add(_current), P1Serves));
        private Game NextWon(bool p1Wins, PointScore score)
        {
            var finalScores = base.Scores.AddRange(new[] { _current, score });
            return new Game(new WonGame(p1Wins, finalScores, P1Serves));
        }

        public Game PointTo(bool p1Wins)
        {
            var nextPoint = _current.Match(
                                     loveAll => loveAll.PointTo(p1Wins),
                                     pair => pair.PointTo(p1Wins),
                                     deuce => deuce.AdvantageTo(p1Wins),
                                     advantage => advantage.WinTo(p1Wins),
                                     win => throw new InvalidOperationException("A LiveGame cannot have a WinPoints state"));

            return nextPoint.Match(
                             loveAll => NextLive(nextPoint),
                             deuce => NextLive(nextPoint),
                             pairAgain => NextLive(nextPoint),
                             advantage => NextLive(nextPoint),
                             winner => NextWon(winner.P1Wins, nextPoint));
        }
    }

    public sealed class WonGame : BaseGame
    {
        private readonly bool _p1Wins;

        public bool P1Wins { get => _p1Wins; }

        // TODO: appending the WinnerPoints at the end might not be the best design 
        // as Winner already expresses which player got the WinningPoint.
        // To be revised later, going with it for the moment.
        internal WonGame(bool p1Wins, PointsList scores, bool p1Serves): base(p1Serves, scores) => _p1Wins = p1Wins;

        internal LiveGame NewGame() => LiveGame.Start(!P1Serves);
    }
}