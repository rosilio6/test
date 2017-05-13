using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLogic;
using GameLogic.Game_Client;
using UserManagement;


namespace GameServer.Interfaces
{
    public abstract class GameFactory
    {
        /// <summary>
        /// Creates a game with already active users and returns an active game with them
        /// </summary>
        /// <param name="users"> The users that will be in the new game</param>
        /// <param name="minPlayers"> Minimim number of players allowed in the game</param>
        /// <param name="maxPlayers">Maximum number of players allowed in the game</param>
        /// <param name="allowedSpectator"> Does the game allows spectators</param>
        /// <param name="chipPolicy">Chip policy for the game</param>
        /// <returns></returns>
        public abstract IGame CreateAndActivateGame(List<User> users, GameType gameType, int buyInPolicy, int enteranceFee,
            int minPlayers, int maxPlayers, bool allowedSpectator, Chip chipPolicy);
    }

  
    }
