using System;
using System.Linq;

using Drills.TennisMatch.Core;

namespace Drills.TennisMatch.Component
{
    using WonGames = TrailingScore<WonGame>;

    public sealed class Set : Union<LiveSet, WonSet>
    {
        public Set(LiveSet set) : base(set) { }
        public Set(WonSet set) : base(set) { }
    }

    public sealed class LiveSet: LiveState<LiveGame, WonGame>
    {
        public LiveGame Game { get => Current; }

        internal static LiveSet Start(bool p1Serves) => new LiveSet(LiveGame.Start(p1Serves));
        internal static LiveSet Start(WonGame wonGame) => new LiveSet(wonGame.NewGame());

        private LiveSet(LiveGame current): base(current) { }
        private LiveSet(LiveGame current, WonGames games) : base(current, games) { }

        private const int _GamesToWin = 6, _AdvantageToWin = 2, _TiebreakWin = 7;
        private static bool IsWin(int a, int b) => (a == _TiebreakWin) || (a == _GamesToWin && a - b >= _AdvantageToWin);
        private static bool IsWin(NumericScore score) => IsWin(score.P1, score.P2) || IsWin(score.P2, score.P1);

        public Set PointTo(bool p1Wins) =>
            Current.PointTo(p1Wins).Match(
                                    onGoing => new Set(new LiveSet(onGoing, Scores)),
                                    finished => {
                                        var next = Scores.Next(finished, finished.P1Wins);
                                        if (IsWin(next.Total))
                                        {
                                            return new Set(new WonSet(next));
                                        }
                                        else
                                        {
                                            var newGame = finished.NewGame();
                                            return new Set(new LiveSet(newGame, next));
                                        }});
    }

    public sealed class WonSet : WonState<WonGame>
    {
        internal WonSet(WonGames games): base(games) { }

        internal LiveSet NewSet() => LiveSet.Start(Scores.Last());
    }
}