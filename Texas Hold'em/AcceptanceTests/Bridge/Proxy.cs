using System.Net.Mail;
using AcceptanceTests.Data;

namespace AcceptanceTests.Bridge
{
    class Proxy : IBridge
    {
        private IBridge _real;

        public Proxy()
        {
            _real = null;
        }

        public void SetRealBridge(IBridge implementation)
        {
            if (_real == null)
                _real = implementation;
        }

        public int Login(string userName, string password)
        {  
           return 0;  
        }

        public void Register(string userName, string password, string email)
        {
        }

        public bool Logout(User user)
        {
           return true;
        }

        public User EditUserDetails(User user)
        {
            return user;
        }

        public void AddUser(User u)
        {
        }

        public int Register(string userName, string password, MailAddress email)
        {
            return 0;
        }

        public bool CreateNewGame(int byInPolicy, int numOfPlayers)
        {
            return true;
        }

        public bool JoinToGame(GameClient game, User user, State state)
        {
            return true;
        }

        public bool LeaveActiveGame(GameClient game, User user)
        {
            return true;
        }

        public bool ReplayInactiveGam(GameClient game)
        {
            return true;
        }

        public bool SaveTurnFromInactiveGame(string userName, GameClient game, int turnNum)
        {
            return true;
        }

        public int FindAllActiveGames()
        {
            return 8;
        }

        public int FindAllActiveGames(State state)
        {
            return 8;
        }

        public bool JoinUserToFirstActiveGame(User user, State state)
        {
            return true;
        }

        public bool CreateNewGameOnlyForPlayers(int byInPolicy, int numOfPlayers)
        {
            return true;
        }

        public User TestfindUserByUserName(string userName)
        {
            User a = new User("User5", "pass", "bla@gamil.com");
            a.LeagueRank = 0;
            return a;
        }

        public int FilterActiveGamesByUserName(string username)
        {
            return 9;
        }

        public bool CreateNewGameFromInstance(GameClient game)
        {
            throw new System.NotImplementedException();
        }

        public User EditUserDetails(User olduser, User newuser)
        {
            throw new System.NotImplementedException();
        }
    }
}
