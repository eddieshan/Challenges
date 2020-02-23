using System;

using Drills.TennisMatch.Core;

namespace Drills.TennisMatch.Component
{
    public sealed class PointScore : Union<LoveAll, PairPoints, Deuce, Advantage, WinPoints>
    {
        public PointScore(LoveAll points) : base(points) { }
        public PointScore(PairPoints points) : base(points) { }
        public PointScore(Deuce points) : base(points) { }
        public PointScore(Advantage points) : base(points) { }
        public PointScore(WinPoints points) : base(points) { }

        public static readonly PointScore Deuce = new PointScore(Component.Deuce.Instance);
        public static readonly PointScore LoveAll = new PointScore(Component.LoveAll.Instance);
    }

    public sealed class LoveAll: ScorePair<Point>
    {
        public static readonly LoveAll Instance = new LoveAll();

        private LoveAll(): base(Point.Love, Point.Love) { }

        public PointScore PointTo(bool p1Wins)
        {
            var pairPoints = p1Wins? new PairPoints(P1.Next(), P2) : new PairPoints(P1, P2.Next());
            return new PointScore(pairPoints);
        }            
    }

    public sealed class PairPoints: ScorePair<Point>
    {
        public PairPoints(Point player1, Point player2): base(player1, player2) { }

        public PointScore PointTo(bool p1Wins)
        {
            var scorerPoints = Choose(p1Wins);
            var otherPoints = Choose(!p1Wins);

            if (scorerPoints == Point.P30 && otherPoints == Point.P40)
            {
                return PointScore.Deuce;
            }
            else if (scorerPoints == Point.P40)
            {
                return new PointScore(new WinPoints(p1Wins));
            }
            else
            {
                var pairPoints = p1Wins? new PairPoints(P1.Next(), P2) : new PairPoints(P1, P2.Next());
                return new PointScore(pairPoints);
            }
        }
    }

    public sealed class Deuce
    {
        public static readonly Deuce Instance = new Deuce();
        private Deuce() { }

        public PointScore AdvantageTo(bool p1Wins) => new PointScore(new Advantage(p1Wins));
    }

    public sealed class Advantage
    {
        private readonly bool _p1Wins;

        public bool P1Wins { get => _p1Wins; }

        public Advantage(bool p1Wins) => _p1Wins = p1Wins;

        public PointScore WinTo(bool p1Wins) => _p1Wins == p1Wins? new PointScore(new WinPoints(_p1Wins)) : PointScore.Deuce;
    }

    public sealed class WinPoints
    {
        private readonly bool _p1Wins;

        public bool P1Wins { get => _p1Wins; }

        public WinPoints(bool p1Wins) => _p1Wins = p1Wins;
    }
}