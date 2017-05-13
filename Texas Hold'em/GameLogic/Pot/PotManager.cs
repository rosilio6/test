using System.Collections.ObjectModel;
using System.Linq;
using UserManagement;

namespace GameLogic
{
    public class PotManager : IPotManager
    {
        private Collection<Pot> Pots { get; set; }
        private bool allIn = false;
        private int currPotForBetting;
        private readonly GameType _gameType;
        public Chip _previousRaise { get; set; }
        private Chip _bigBlind;
        public PotManager(Collection<Player> players, GameType gameType, Chip bigBlind)
        {
            _gameType = gameType;
            _bigBlind = bigBlind;
            this.currPotForBetting = 0;
            this._previousRaise = new Chip(0);
            this.Pots = new Collection<Pot>();
            Pots.Add(new Pot(players));
        }

        public void putMoney(Player player, int value)
        {
          //player.ReduceChips(value);
          Pots[currPotForBetting].AddChips(value);
        }

        public void GoAllIn(Collection<Player> players)
        {
            Collection<Player> playersInPot = new Collection<Player>(players.ToList());
            int minPot = GetMinPlayerPot(players);
            foreach (Player P in players)
            {
                P.PlayerPot -= minPot;
                Pots[currPotForBetting].AddChips(minPot);
                if (P.PlayerPot==0)
                {
                    playersInPot.Remove(P);
                }
            }
            
            while (playersInPot.Count!=0)
            { 
                minPot = GetMinPlayerPot(playersInPot);
                currPotForBetting += 1;
                Pots.Add(new Pot(playersInPot));
                Collection<Player> playersNewInPot = new Collection<Player>(playersInPot.ToList());
                foreach (Player P in playersInPot)
                {
                    P.PlayerPot -= minPot;
                    Pots[currPotForBetting].AddChips(minPot);
                    if (P.PlayerPot == 0)
                    {
                        playersNewInPot.Remove(P);
                    }
                }
                playersInPot= new Collection<Player>(playersNewInPot.ToList());

            }
        }

        private int GetMinPlayerPot(Collection<Player> players)
        {
            int minPot = players[0].PlayerPot;
            foreach (Player P in players)
            {
                if (P.PlayerPot<minPot)
                {
                    minPot = P.PlayerPot;
                }
            }
            return minPot;
        }
        public int GetMaxPlayerPot(Collection<Player> players)
        {
            int maxPot = players[0].PlayerPot;
            foreach (Player P in players)
            {
                if (P.PlayerPot > maxPot)
                {
                    maxPot = P.PlayerPot;
                }
            }
            return maxPot;
        }

        public void Winner(Collection<Player> player)
        {
            foreach (var p in Pots)
            {
                p.GiveTheWinningMoney(player);
            }
        }

        public void playerFolded(Player player)
        {
            foreach (var p in Pots)
            {
                p.playerFolded(player);
            }
        }
        public bool LegalRaise(Chip RaiseChip, int RoundNum)
        {
            switch (_gameType)
            {
                case GameType.Limit:
                    if ((RoundNum > 0) && ((RoundNum < 3)))
                        return (RaiseChip.Sum == _bigBlind.Sum);
                    else if ((RoundNum > 2) && ((RoundNum < 5)))
                        return (RaiseChip.Sum == 2 * _bigBlind.Sum);
                    break;
                case GameType.NoLimit:
                    return (RaiseChip.Sum >= 2 * _previousRaise.Sum);
                case GameType.PotLimit:
                    return (RaiseChip.Sum <= Pots[currPotForBetting].PotChip.Sum);
            }
            return false;
        }

        public int GetSum()
        {
            int sum = 0;
            foreach (Pot pot in Pots)
            {
                sum += pot.PotChip.Sum;
            }
            return sum;
        }
    }
}