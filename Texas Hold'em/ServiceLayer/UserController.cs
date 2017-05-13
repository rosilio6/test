using UserManagement;

namespace ServiceLayer
{
  
    public class UserController : IUserController
    {

        private IUserEnforcer _user;
        private IUserDataBase _udb;

        public UserController(IUserEnforcer user, IUserDataBase udb)
        {
            _user = user;
            _udb = udb;
        }

        public void CreateNewUserList()
        {
            throw new System.NotImplementedException();
        }

        public User AddNewUser(string userName, string password)
        {
            return  _udb.AddNewUser(userName, password);
        }

        public bool CanAddNewUser(string userName, string password)
        {
            return _udb.CanAddNewUser(userName, password);
        }

        public bool CheckIfUserNameExist(string userName)
        {
            return true;
        }

        public User GetUserFromUserList(string userName)
        {
            return  _udb.GetUserFromUserList(userName);
        }

        public User UserLogIn(string userName, string password)
        {
            return _udb.UserLogIn(userName, password);
        }

        public User CreateUser(string name, string password, int id)
        {
            throw new System.NotImplementedException();
        }

        public bool CheckLogInDetails(User user, string password)
        {
            return _user.CheckLogInDetails(user, password);
        }

        public bool ChangePassword(User user, string oldPass, string newPass)
        {
            return  _user.ChangePassword(user, oldPass, newPass);
        }

        public bool ValidPassword(string newPass)
        {
            return _user.ValidPassword(newPass);
        }

        public bool ChangeEmail(User user, string newEmail)
        {
            return _user.ChangeEmail(user, newEmail);

        }

        public bool ValidEmail(string newEmail)
        {
            throw new System.NotImplementedException();
        }

        public int AddMoney(User user, int value)
        {
            throw new System.NotImplementedException();
        }

        public int UseMoney(User user, int value)
        {
            throw new System.NotImplementedException();
        }

        public int GetMoney(User user)
        {
            throw new System.NotImplementedException();
        }

        public bool ValidMoneyUse(User user, int value)
        {
            throw new System.NotImplementedException();
        }

        public int GetId(User user)
        {
            throw new System.NotImplementedException();
        }

        public string GetPathToImage(User user)
        {
            throw new System.NotImplementedException();
        }

        public bool SetPathToImage(User user, string value)
        {
            throw new System.NotImplementedException();
        }

        public string GetEmail(User user)
        {
            throw new System.NotImplementedException();
        }

        public int GetRank(User user)
        {
            throw new System.NotImplementedException();
        }

        public bool SetRank(User user, int value)
        {
            throw new System.NotImplementedException();
        }
    }
}
