using System;
using System.Collections.Generic;
using GameLogic;
using GameServer.Interfaces;
using GameServer.Leagues;
using UserManagement;

namespace ServiceLayer
{

    
    public class GameController : IGameController
    {
        private  IGameCenter _game;

        public GameController(IGameCenter game)
        {
            _game = game;
        }

        public void AddUserToLeague(LeagueType leagueType, User userToAdd)
        {
            throw new NotImplementedException();
        }

        public bool IsUserInLeague(LeagueType leagueType, User userTosearch)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserRank(User player, GameProperties gameProperties)
        {
            throw new NotImplementedException();
        }

        public void ActivateGame(IGame gameToActivate)
        {
            _game.ActivateGame(gameToActivate);
        }

        public void DeactivateGame(IGame gameToClose)
        {
            throw new NotImplementedException();
        }

        public List<IGame> SearchActiveGamesBy(string playerName)
        {
            return  _game.SearchActiveGamesBy(playerName);
        }

        public List<IGame> SearchActiveGamesBy(int mainPotSize)
        {
            throw new NotImplementedException();
        }

        public List<IGame> SearchActiveGamesBy(GameProperties gameProperties)
        {
            throw new NotImplementedException();
        }

        public void JoinGameAsPlayer(User userToJoin, IGame gameToJoin)
        {
            _game.JoinGameAsPlayer(userToJoin,gameToJoin);
        }

        public void JoinGameAsSpectator(User userToJoin, IGame gameToJoin)
        {
            _game.JoinGameAsSpectator(userToJoin,gameToJoin);
        }

        public List<IGame> GetAllActiveGames()
        {
           return _game.GetAllActiveGames();
        }

        public bool LeaveGame(User userToLeave, IGame game)
        {
            try
            {
                _game.LeaveGame(userToLeave, game);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
