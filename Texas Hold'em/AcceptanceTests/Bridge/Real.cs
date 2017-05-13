using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Mail;
using AcceptanceTests.Data;
using GameLogic;
using ServiceLayer;
using UserManagement;
using User = AcceptanceTests.Data.User;

namespace AcceptanceTests.Bridge
{
    class Real : IBridge
    {
        private IUserController _user;
        private IGameController _game;
        private int _userCreationIndex ;
        private int _userId;

        public Real(IUserController user, IGameController game)
        {
            _user = user;
            _game = game;
            _userCreationIndex = 0;
            _userId = 0;
        }

        public int Login(string userName, string password)
        {
            int v = 1; 
            UserManagement.User user = new UserManagement.User(userName, password,0);
            try
            {
                if (_user.UserLogIn(user.PlayerName, password)!=null)
                {
                    v = 0; 
                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return v; 
     }

        public void Register(string userName, string password, string email)
        {
            UserManagement.User user = new UserManagement.User(userName, password, 0);
            try
            {
                 _user.AddNewUser(userName, password);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool Logout(User User)
        {
            bool isExistOnSys = _user.CanAddNewUser(User.UserName, User.Password);
            if (!isExistOnSys) return true;
               return false;
        }

        public User EditUserDetails(User oldeuser , User newuser)
        {
            UserManagement.User user = new UserManagement.User(oldeuser.UserName, oldeuser.Password,0);
            user.Set_email(oldeuser.Email.Address);
            bool mail = _user.ChangeEmail(user , newuser.Email.Address);
            bool pass = _user.ChangePassword(user,oldeuser.Password,newuser.Password);
            return (mail && pass && oldeuser.UserName == newuser.UserName) ? newuser : null;
        }


        public int Register(string userName, string password, MailAddress email)
        {
            UserManagement.User user =  _user.AddNewUser(userName,password);
            if (user != null) return 0;
               return 1;
        }

        public bool CreateNewGame(int byInPolicy, int numOfPlayers)
        {
          try
          {
                Collection<UserManagement.User> users = new Collection<UserManagement.User>();
                UserManagement.User user = _user.AddNewUser("temp"+_userCreationIndex, "12344234");
                UserManagement.User user2 = _user.AddNewUser("temp2" + _userCreationIndex, "12344234");
              _userCreationIndex++;

                user.SetMoney(80);
                users.Add(user);
                if(numOfPlayers>1)
                users.Add(user2);

                IGame client = new GameLogic.Game_Client.GameClient(users, GameType.Limit, byInPolicy,10,2 , 10, true, new GameLogic.Chip(10));
                _game.ActivateGame(client);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool JoinToGame(GameClient game, User user, State state)
        {
            GameLogic.Game_Client.GameClient client;
            UserManagement.User userToJoin = _user.GetUserFromUserList(user.UserName);
            Collection< UserManagement.User> users = new Collection<UserManagement.User>();
            bool val = false;

            foreach (KeyValuePair<string, User> entry in game.Players)
            {
                User tmp = entry.Value;
                UserManagement.User U = new UserManagement.User(tmp.UserName, tmp.Password, 0);
                U.Set_email(tmp.Email.Address);
                U.ChangePlayerState(PlayerState.Player);
                U.SetMoney(100);
                users.Add(U);
            }

             client = new GameLogic.Game_Client.GameClient(users, GameType.Limit, 0, game.AmountOfPlayers, 4,game.AmountOfPlayers, true, new GameLogic.Chip(20));
            int before = client.GetPlayers().Count; 
            if (state == State.Player)
            {
                try
                {
                    _game.JoinGameAsPlayer(userToJoin, client);
                    if(client.GetPlayers().Count == before + 1)
                    val = true;

                }
                catch (Exception)
                {
                    return false;
                }
            }
            else if (state == State.Spectator)
            {
                try
                {
                    _game.JoinGameAsSpectator(userToJoin, client);
                    val = true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return val;
        }

        public bool LeaveActiveGame(GameClient game, User user)
        {
            GameLogic.Game_Client.GameClient client;
            UserManagement.User userToDelete = new UserManagement.User(user.UserName, user.Password,0);
            Collection<UserManagement.User> users = new Collection<UserManagement.User>();
            bool val;

            foreach (KeyValuePair<string, User> entry in game.Players)
            {
                User tmp = entry.Value;
                UserManagement.User U = new UserManagement.User(tmp.UserName, tmp.Password, _userId);
                U.Set_email(tmp.Email.Address);
                U.ChangePlayerState(PlayerState.Player);
                U.SetMoney(100);
                users.Add(U);
                _userId++;
            }
            client = new GameLogic.Game_Client.GameClient(users, GameType.Limit, 2, 10, 2, 10, true, new GameLogic.Chip(10));
            _game.ActivateGame(client);

            try
            {
                if (_game.LeaveGame(userToDelete, client))
                    val = true;
                else
                {
                    val = false;
                }
            }
            catch (Exception)
            {
                val = false;
            }
            return val;
        }

        public bool ReplayInactiveGam(GameClient game)
        {
            return true;
        }

        public bool SaveTurnFromInactiveGame(string userName, GameClient game, int turnNum)
        {
            throw new NotImplementedException();
        }

        public int FindAllActiveGames()
        {
            return _game.GetAllActiveGames().Count;

        }

        public int FindAllActiveGames(State state)
        {
            return _game.GetAllActiveGames().Count;
        }

        public bool JoinUserToFirstActiveGame(User user, State state)
        {
            try
            {

                IGame client = _game.GetAllActiveGames()[0];
                UserManagement.User U = new UserManagement.User(user.UserName, user.Password, 0);
                U.SetMoney(100);
                U.ChangePlayerState(PlayerState.Player);
                client.AddPlayer(U);
                _game.JoinGameAsPlayer(U, client);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreateNewGameOnlyForPlayers(int byInPolicy, int numOfPlayers)
        {
            try
            {
                Collection<UserManagement.User> users = new Collection<UserManagement.User>();
                UserManagement.User user = _user.AddNewUser("temp" + _userCreationIndex, "12344234");
                UserManagement.User user2 = _user.AddNewUser("temp2" + _userCreationIndex, "12344234");
                _userCreationIndex++;

                user.SetMoney(80);
                users.Add(user);
                if (numOfPlayers > 1)
                    users.Add(user2);

                IGame client = new GameLogic.Game_Client.GameClient(users, GameType.Limit, byInPolicy, 10, 2, 10, false, new GameLogic.Chip(10));
                _game.ActivateGame(client);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
         
        }

        public User TestfindUserByUserName(string userName)
        {
            User u; 
            try
            {
                UserManagement.User user = _user.GetUserFromUserList(userName);
                u = new User(user.PlayerName, "not relevanr", "notrel@gmail.com");
                switch (user.GetRank())
                {
                    case 0:
                    {
                        u.LeagueRank = LeagueType.Beginner;
                        break;
                    }
                    case 1:
                    {
                        u.LeagueRank = LeagueType.Normal;
                        break;
                    }
                    case 3:
                        {
                            u.LeagueRank = LeagueType.Advanced;
                            break;
                        }
                }

            }
            catch (Exception e)
            {
                 u = new User("undef", "not relevanr", "undef@gmail.com");
                 u.LeagueRank = LeagueType.Undefined;

            }

            return u; 
        }

        public int FilterActiveGamesByUserName(string username)
        {
            int numberOfGames;

            try
            {
                numberOfGames = _game.SearchActiveGamesBy(username).Count;
            }
            catch (Exception)
            {
                numberOfGames = 0;
            }

            return numberOfGames;
        }

        public bool CreateNewGameFromInstance(GameClient game)
        {
            IGame client;
            Collection<UserManagement.User> users = new Collection<UserManagement.User>();
            bool val;

            foreach (KeyValuePair<string, User> entry in game.Players)
            {
                User tmp = entry.Value;
                UserManagement.User U = new UserManagement.User(tmp.UserName, tmp.Password, 0);
                U.Set_email(tmp.Email.Address);
                U.SetMoney(100);
                U.ChangePlayerState(PlayerState.Player);
                users.Add(U);
            }
            try
            {
                client = new GameLogic.Game_Client.GameClient(users, GameType.Limit, 2, 10, 2, 10, true, new GameLogic.Chip(10));
                _game.ActivateGame(client);
                 val = true;
            }
            catch (Exception)
            {
                val = false;
            }


           
            return val; 
        }
    }
}
