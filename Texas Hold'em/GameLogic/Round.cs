using System.Collections.ObjectModel;
using UserManagement;

namespace GameLogic
{
    public class Round
    {
        public Collection<Turn> turns { get; set; }
        public Collection<Player> players { get; set; }
        public Chip roundChip { get; set; }

        public Round(Collection<Player> players)
        {
            this.players = players;
            this.turns = new Collection<Turn>();
            this.roundChip = new Chip(0);
        }

        public void AddTurn(Turn turn)
        {
            turns.Add(turn);
        }
    }
}