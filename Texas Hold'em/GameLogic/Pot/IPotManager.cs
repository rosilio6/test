using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    interface IPotManager
    {
        void putMoney(Player player,int value);
        void GoAllIn(Collection<Player> players);
        void Winner(Collection<Player> player);
        void playerFolded(Player player);
    }
}
