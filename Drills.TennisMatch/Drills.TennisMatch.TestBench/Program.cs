using System;
using System.Linq;

using Drills.TennisMatch.Core;
using Drills.TennisMatch.Component;
using Drills.TennisMatch.Component.Test;

namespace Drills.TennisMatch.TestBench
{

    /// <summary>
    /// Console app to help test StateVisitor.
    /// This is faster to start than Unit Tests as MS Test takes a lot of time bootstrapping.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            int numLive = 0, numWon = 0, p1Wins = 0, p2Wins = 0;

            var stateVisitor = new StateVisitor();

            stateVisitor.ApplyTest(StateVisitor.NewMatch,
                liveMatch =>
                {
                    numLive += 1;
                    var lastPoint = liveMatch.Set.Game.Points;
                    var set = liveMatch.Set.GetType().Name;
                    var point = lastPoint.GetType().Name;
                    //Console.WriteLine($"Live: {set} | {point}");
                },
                wonMatch =>
                {
                    numWon += 1;
                    if(wonMatch.Total.P1Wins())
                    {
                        p1Wins += 1;
                    }
                    else
                    {
                        p2Wins += 1;
                    }
                    Console.WriteLine($"Won: {numLive} | {numWon} | {wonMatch.Total.P1Wins()}");
                    //Console.ReadKey();
                });

            Console.WriteLine($"Test finished: P1Wins({p1Wins}) | P2Wins({p2Wins}) {numLive} | {numWon}");
            Console.ReadKey();
        }
    }
}