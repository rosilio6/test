using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement;

namespace GameLogic
{
    class Pot
    {
        private Collection<Player> players { get; set; }
        public Chip PotChip { get; set; }


        public Pot(Collection<Player> players)
        {
            this.players = players;
            this.PotChip = new Chip(0);
        }

        public void AddChips(Chip addedchip)
        {
            PotChip.Add(addedchip.Sum);
        }
        public void AddChips(int value)
        {
            PotChip.Add(value);
        }

        public void playerFolded(Player player)
        {
            players.Remove(player);
        }

        public void GiveTheWinningMoney(Collection<Player> winners)
        {
            int winnersSplit = 0;
            foreach (Player P in winners)
            {
                if (players.Contains(P))
                {
                    winnersSplit++;
                    
                }
            }

            if(winnersSplit>0)
            foreach (Player P in winners)
            {
                    P.AddChips(PotChip.Sum / winnersSplit);
            }
            else
                {
                    SplitMoney();
                }
        }
        

        private void SplitMoney()
        {
            foreach (var p in players)
            {
                p.AddChips(PotChip.Sum/players.Count);
            }
            PotChip.Sum = 0;
        }

    }
}
