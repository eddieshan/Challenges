using System;
using System.Collections.Generic;

namespace Drills.TennisMatch.Component
{
    public class Point
    {
        private readonly int _value;
        private Point(int value) => _value = value;

        public int Value { get => _value; }

        public int Difference(Point point) => Value - point.Value;

        // A symbol based implementation to define a strict set of possible Point values.
        // Instead of fragile enums.
        public static readonly Point Love = new Point(0);
        public static readonly Point P15 = new Point(1);
        public static readonly Point P30 = new Point(2);
        public static readonly Point P40 = new Point(3);

        private static readonly IList<Point> _sequence = new List<Point> { Point.Love, Point.P15, Point.P30, Point.P40 };

        // For the sake of brevity, I´m using List.IndexOf, knowing in advance that it uses Reference Equality for reference types.
        // This equality test might break if IEquality implementation is overridden in Point.
        // No range checking, this is intentional.
        // If the caller logic is correct the index should never be out of bounds.
        // Otherwise, if index + 1 fails, it will fail early and clearly point out the location of the error.
        // TODO: consider a refactor filtering with Object.ReferenceEquals, mover verbose but also safer and clearer.
        public Point Next() => _sequence[_sequence.IndexOf(this) + 1];

    }
}
