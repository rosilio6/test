
using System;
using System.Collections.Generic;
using UserManagement;
using GameLogic;
using GameServer.Leagues;


namespace ServiceLayer
{
    public interface IGameController
    {

        /// <summary>
        /// Removes the user from the game
        /// </summary>
        /// <param name="userToLeave"> User to remove </param>
        /// <param name="game"> Game to remove the user from </param>
        bool LeaveGame(User userToLeave, IGame game);


        /// <summary>
        /// Adds user to league.
        /// </summary>
        /// <param name="leagueType"> League to which the user is added to.</param>
        /// <param name="userToAdd"> The user which is added to a specific league.</param>
        void AddUserToLeague(LeagueType leagueType, User userToAdd);

        /// <summary>
        /// Checks if the user is in the given league
        /// </summary>
        /// <param name="leagueType">THe league in which the user is searched</param>
        /// <param name="userTosearch">The user to be searched</param>
        /// <returns>True if the user is found in league, False otherwise</returns>
        bool IsUserInLeague(LeagueType leagueType, User userTosearch);

        /// <summary>
        /// Updates the players rank according to the game properties.
        /// </summary>
        /// <param name="player"> The player for which to update the rank </param>
        /// <param name="gameProperties"> THe game properties from which to calculate the new rank</param>
        void UpdateUserRank(User player, GameProperties gameProperties);

        /// <summary>
        /// Activates game in the system to start to play
        /// </summary>
        /// <param name="gameToActivate">The game to activae. The game needs to be imitialized and ready to run</param>
        void ActivateGame(IGame gameToActivate);

        /// <summary>
        /// Removes the game from the active games and archives the game
        /// </summary>
        /// <param name="gameToClose"> The game to close</param>
        void DeactivateGame(IGame gameToClose);

        /// <summary>
        /// Search active games for a playersName 
        /// </summary>
        /// <param name="playerName">The name that the system searches in the active games</param>
        /// <returns>All the active games in which the given user is found</returns>
        List<IGame> SearchActiveGamesBy(string playerName);

        /// <summary>
        /// Filter all the active games by Main Pot size
        /// </summary>
        /// <param name="mainPotSize">The size by which the system searches in the active games</param>
        /// <returns>All the active games that fit the pot size</returns>
        List<IGame> SearchActiveGamesBy(int mainPotSize);

        /// <summary>
        /// Filter all the active games by Game Preferences
        /// </summary>
        /// <param name="gameProperties">The properties by which to search.
        /// If a property is Null, the system will ignore it in its search.</param>
        /// <returns></returns>
        List<IGame> SearchActiveGamesBy(GameProperties gameProperties);

        /// <summary>
        /// Lets a user join a game as a player
        /// </summary>
        /// <param name="userToJoin">The user to join into the game</param>
        /// <param name="gameToJoin">The game to which the user wants to join as a player</param>
        void JoinGameAsPlayer(User userToJoin, IGame gameToJoin);

        /// <summary>
        /// Lets a user join a game as a spectator
        /// </summary>
        /// <param name="userToJoin">The user to join into the game</param>
        /// <param name="gameToJoin">The game to which the user wants to join as a spectator</param>
        void JoinGameAsSpectator(User userToJoin, IGame gameToJoin);


        /// <summary>
        /// Returns all the available active games
        /// </summary>
        /// <returns></returns>
        List<IGame> GetAllActiveGames();


    }
}
