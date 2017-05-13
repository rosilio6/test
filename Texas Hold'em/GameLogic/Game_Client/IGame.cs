using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement;

namespace GameLogic
{
    public interface IGame 
    {
        /// <summary>
        /// Creates a game with default settings
        /// </summary>
        IGame CreateGame(List<User> users);

        /// <summary>
        /// Returns the Game properties of the game.
        /// </summary>
        /// <returns></returns>
        GameProperties GetProperties();

        /// <summary>
        /// Returns the current pot size
        /// </summary>
        /// <returns></returns>
        int GetMainPotSize();

        /// <summary>
        /// Adds a player to the game
        /// </summary>
        /// <param name="spectator"></param>
        void AddSpectator(User spectator);

        /// <summary>
        /// Adds a spectator to the game
        /// </summary>
        /// <param name="player"></param>
        void AddPlayer(User player);

        /// <summary>
        /// Gets all players that are active during the turn number
        /// </summary>
        /// <param name="numOfTurn">The number of turn (starts from 1 = The first turn of the game)</param>
        /// <returns></returns>
        List<User> GetUsersInRound(int numOfTurn);

        // Running the Istance Game
        void Run();


        //the user decide to leave the game therefore he deleted from the game.
        void DeleteUser(User user);

        //notify the players on the changes in the game.
        void NotifyAllUsers(PlayerNotification notifications);
    }
}
