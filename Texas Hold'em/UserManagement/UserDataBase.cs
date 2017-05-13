using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texas_Hold_em.Loggers;
using UserManagement;

namespace UserManagement
{
    public class UserDataBase : IUserDataBase
    {
        private static UserDataBase _instance;

        private List<UserManagement.User> _userList;
        private List<UserManagement.User> _logInUserList;

        private static readonly IGameLogger ErrorLogger = LoggerFactory.GetErrorLogger();
        private static readonly UserEnforcer UserHandler = new UserEnforcer();

        private UserDataBase() { }

        public static UserDataBase Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserDataBase();
                    _instance.CreateNewUserList();
                }
                return _instance;
            }
        }

        public void CreateNewUserList()
        {
            _userList = new List<User>();
            _logInUserList = new List<User>();
        }

        public User AddNewUser(string userName, string password)
        {
            if (CheckIfUserNameExist(userName))
                { ErrorLogger.Error("trying to add user name that is already taken.  should have checked before with 'canAddNewUser'"); return null;}
            if (!UserHandler.ValidPassword(password))
                { ErrorLogger.Error("trying to add new user with not valided passwrod. should have checked before with 'canAddNewUser'"); return null; }

            User returnUser = UserHandler.CreateUser(userName, password, _userList.Count + 1);

            if(_userList == null) _userList = new List<User>();

            _userList.Add(returnUser);

            return returnUser;
        }

        public bool RemoveUser(User user)
        {
            if (_logInUserList.Contains(user))
            {
                UserLogOut(user);
            }
            return _userList.Remove(user);
        }

        public bool CanAddNewUser(string userName, string password)
        {
            if (CheckIfUserNameExist(userName)) return false;
            if(!UserHandler.ValidPassword(password)) return false;
            return true;
        }

        public bool CheckIfUserNameExist(string userName)
        {
            if(_userList == null || _userList.Count == 0)
                  return false;
            foreach (var user in _userList)
            {
                if (user.PlayerName == userName) return true;
            }
            return false;
        }

        public User GetUserFromUserList(string userName)
        {
            if (_userList == null || _userList.Count == 0)
            {
                ErrorLogger.Error("getUserFromList -> user name dont exit. should have checked before with CheckIfUserNameExist");
                return null;
            }

            foreach (var user in _userList)
            {
                if (user.PlayerName == userName) return user;
            }

            ErrorLogger.Error("getUserFromList -> user name dont exit. should have checked before with CheckIfUserNameExist");
            return null;
        }


        public User UserLogIn(string userName, string password)
        {
            if (!CheckIfUserNameExist(userName)) return null;
            User user = GetUserFromUserList(userName);
            if (!user.CheckPassword(password)) return null;

            if(_logInUserList == null) _logInUserList = new List<User>();
            _logInUserList.Add(user);

            return user;
        }

        public List<User> GetLogInUserList() => _logInUserList;

        public bool IsUserLogIn(User user)
        {
            return (_logInUserList.Contains(user));
        }

        public bool UserLogOut(User user)
        {
            if(_logInUserList == null) return false;
            
            if (_logInUserList.Contains(user))
            {
                _logInUserList.Remove(user);
                return true;
            }
            return false;
        }
    }
}
