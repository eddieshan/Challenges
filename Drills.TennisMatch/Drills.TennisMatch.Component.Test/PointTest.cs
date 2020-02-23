using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Drills.TennisMatch.Component;

namespace Drills.TennisMatch.Component.Test
{
    [TestClass]
    public class PointTest
    {
        private static IEnumerable<object[]> PointValues
        {
            get
            {
                yield return new[] { (object)0, Point.Love };
                yield return new[] { (object)1, Point.P15 };
                yield return new[] { (object)2, Point.P30 };
                yield return new[] { (object)3, Point.P40 };
            }
        }

        private static IEnumerable<object[]> PointsOrder
        {
            get
            {
                yield return new[] { Point.Love, Point.P15 };
                yield return new[] { Point.P15, Point.P30 };
                yield return new[] { Point.P30, Point.P40 };
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(PointValues), DynamicDataSourceType.Property)]
        public void PointsHaveValidValues(int value, Point point) => Assert.AreEqual(value, point.Value);

        [DataTestMethod]
        [DynamicData(nameof(PointsOrder), DynamicDataSourceType.Property)]
        public void PointsAreOrdered(Point current, Point next) => Assert.AreEqual(next, current.Next());

        [TestMethod]
        public void NextOfP40ThrowsException() => Assert.ThrowsException<ArgumentOutOfRangeException>(() => Point.P40.Next());
    }
}
