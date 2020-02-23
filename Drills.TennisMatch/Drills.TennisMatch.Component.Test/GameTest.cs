using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Drills.TennisMatch.Component;

namespace Drills.TennisMatch.Component.Test
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void TestNewGame()
        {
            //var newGame = LiveGame.Start();
        }

        [TestMethod]
        public void TestGameStates()
        {
            var stateVisitor = new StateVisitor();

            stateVisitor.ApplyTest(StateVisitor.NewMatch,
                liveMatch =>
                {
                },
                wonMatch =>
                {
                });
        }
    }
}
