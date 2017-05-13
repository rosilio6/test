
using GameServer.Leagues;
using UserManagement;

namespace GameServer
{
    public static class UserFactory
    {
        //private static readonly UserDataBase UserDataBase = new UserDataBase();
        public static readonly UserDataBase UserDataBase = UserDataBase.Instance;

        private static readonly GameCenter GameCenter = GameCenter.Instance;

        /// <summary>
        /// Creates a user in the system.
        /// </summary>
        /// <returns></returns>
        public static User CreateUser(string name, string password)
        {
            var newUser = UserDataBase.AddNewUser(name, password);
            var defaultLeague = GameCenter.GetDefaultLeague();
            GameCenter.AddUserToLeague(defaultLeague, newUser);
            return newUser;
            
        }
    }
}
