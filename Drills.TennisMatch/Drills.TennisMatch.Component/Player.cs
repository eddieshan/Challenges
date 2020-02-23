using System;

namespace Drills.TennisMatch.Component
{
    public class Player
    {
        private string _name;

        public string Name { get => _name; }

        private bool _is1;

        public bool IsP1 { get => _is1; }

        private Player(string name, bool is1)
        {
            _name = name;
            _is1 = is1;
        }

        // Knowing the players' names is only relevant at the Match level.
        // At the Point, Game and Set levels, only isP1 is needed to choose action on P1 or P2.
        // At the Game level, it is necessary to who is server and who is receiver.
        // TODO: replace all Player properties with bool isP1. This will save memory,
        // 1 bool -> 1 byte vs 1 reference to player -> sizeof ObjectReference.
        // For the moment, assume Player1 and Player2 are static dependencies.
        // This simplifies a bit the design and allows me to concentrate on other parts.
        public static readonly Player Player1 = new Player("Player 1", true);
        public static readonly Player Player2 = new Player("Player 2", false);
    }
}
