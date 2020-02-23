using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Drills.TennisMatch.Component;

namespace Drills.TennisMatch.Component.Test
{
    [TestClass]
    public class MatchTest
    {
        [TestMethod]
        public void TestMatchStates()
        {
            var stateVisitor = new StateVisitor();

            stateVisitor.ApplyTest(StateVisitor.NewMatch,
                liveMatch =>
                {
                    if(liveMatch.Scores.Trail.Any())
                    {

                    }
                    else
                    {
                        Assert.IsTrue(liveMatch.Scores.Total.P1 == 0);
                        Assert.IsTrue(liveMatch.Scores.Total.P2 == 0);
                    }                    
                },
                wonMatch =>
                {
                    if (wonMatch.Total.P1Wins())
                    {
                        Assert.IsTrue(wonMatch.Total.P1 > wonMatch.Total.P2);
                        Assert.IsTrue(wonMatch.Total.P1 == 3);
                    }
                    else
                    {
                        Assert.IsTrue(wonMatch.Total.P2 > wonMatch.Total.P1);
                        Assert.IsTrue(wonMatch.Total.P2 == 3);
                    }
                });
        }
    }
}
