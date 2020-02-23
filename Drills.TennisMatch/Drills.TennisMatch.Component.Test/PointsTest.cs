using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Drills.TennisMatch.Component;

namespace Drills.TennisMatch.Component.Test
{
    [TestClass]
    public class PointsTest
    {
        private void FailState(string to, string from) => Assert.Fail($"{to} cannot be the next state of {from}");

        [TestMethod]
        public void LoveAllHasDoubleLove()
        {
            Assert.IsTrue(LoveAll.Instance.P1 == Point.Love);
            Assert.IsTrue(LoveAll.Instance.P2 == Point.Love);
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void LoveAllGoesToP15(bool p1Wins)
        {
            var next = LoveAll.Instance.PointTo(p1Wins);
            next.Match(
                loveAll => FailState("LoveAll", "LoveAll"),
                pair => {
                    Assert.AreEqual(p1Wins? Point.P15 : Point.Love, pair.P1);
                    Assert.AreEqual(p1Wins? Point.Love : Point.P15, pair.P2);
                },
                deuce => FailState("Deuce", "LoveAll"),
                advantage => FailState("Advantage", "LoveAll"),
                win => FailState("Win", "LoveAll"));
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void DeuceGoesToAdvantange(bool p1Wins)
        {
            Deuce.Instance.AdvantageTo(p1Wins)
                 .Match(
                 loveAll => FailState("LoveAll", "Deuce"),
                 pair => FailState("PairPoints", "Deuce"),
                 deuce => FailState("Deuce", "Deuce"),
                 advantage => { },
                 win => FailState("Win", "Deuce"));
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void AdvantangeGoesBackToDeuce(bool p1Wins)
        {
            var advantage = new Advantage(p1Wins);

            advantage.WinTo(!p1Wins)
                     .Match(
                      loveAll => FailState("LoveAll", "Advantage"),
                      pair => FailState("PairPoints", "Advantage"),
                      deuce => { },
                      advantageAgain => FailState("Advantage", "Advantage"),
                      win => FailState("Win", "Advantage"));
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void AdvantangeGoesToWin(bool p1Wins)
        {
            var advantage = new Advantage(p1Wins);
            advantage.WinTo(p1Wins)
                     .Match(
                      loveAll => FailState("LoveAll", "Advantage"),
                      pair => FailState("PairPoints", "Advantage"),
                      deuce => FailState("Deuce", "Advantage"),
                      advantageAgain => FailState("Advantage", "Advantage"),
                      win => { });
        }
    }
}