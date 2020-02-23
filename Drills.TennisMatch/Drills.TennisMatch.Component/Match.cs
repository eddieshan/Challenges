using System;

using Drills.TennisMatch.Core;

namespace Drills.TennisMatch.Component
{
    using WonSets = TrailingScore<WonSet>;

    public sealed class Match : Union<LiveMatch, WonMatch>
    {
        public Match(LiveMatch match) : base(match) { }
        public Match(WonMatch match) : base(match) { }
    }

    public sealed class LiveMatch : LiveState<LiveSet, WonSet>
    {
        public LiveSet Set { get => Current; }

        public static Match Start(bool p1Serves) => new Match(new LiveMatch(LiveSet.Start(p1Serves), new WonSets()));

        private LiveMatch(LiveSet current, WonSets sets) : base(current, sets) { }

        private const int _SetsToWin = 3;

        private static bool IsBestOfFive(ScorePair<int> score) => score.P1 == _SetsToWin || score.P2 == _SetsToWin;

        private Match NextWon(WonSets sets) => new Match(new WonMatch(sets));
        private Match NextLive(LiveSet current, WonSets sets) => new Match(new LiveMatch(current, sets));

        public Match PointTo(bool p1Wins) => 
            Set.PointTo(p1Wins).Match(
                                onGoing => NextLive(onGoing, Scores),
                                finished => {
                                    var next = Scores.Next(finished, finished.Total.P1Wins());
                                    return IsBestOfFive(next.Total)? NextWon(next) : NextLive(finished.NewSet(), next);
                                });
    }

    public sealed class WonMatch : WonState<WonSet>
    {
        internal WonMatch(WonSets sets) : base(sets) { }
    }
}