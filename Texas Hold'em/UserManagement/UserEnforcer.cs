using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texas_Hold_em.Loggers;


namespace UserManagement
{
    public class UserEnforcer : IUserEnforcer
    {
        private static readonly IGameLogger ErrorLogger = LoggerFactory.GetErrorLogger();

        public User CreateUser(string name, string password, int id)
	    {
	        var newUser = new User(name, password,id);
            newUser.ChangePlayerState(PlayerState.InLobby);
	        return newUser;
	    }

        public void ChangePlayerState(User user, PlayerState player)
        {
            user.ChangePlayerState(player);
            
        }

        public bool CheckLogInDetails(User user, string password)
		{
			return user.CheckPassword(password);
		}

        public bool RemoveThisUserFromDataBase(User user)
        {
            UserDataBase userData = UserDataBase.Instance;
            return userData.RemoveUser(user);
        }

        public bool ChangePassword(User user, string oldPass, string newPass)
		{
			if (ValidPassword(newPass))
				return user.ChangePassword(oldPass, newPass);
		
            return false;
		}

		//weak password
		public bool ValidPassword(string newPass)
		{
			if (newPass.Length > 6)
				return true;
			return false;
		}

		public bool ChangeEmail(User user, string newEmail)
		{
			if (ValidEmail(newEmail))
				return user.Set_email(newEmail);
			return false;
		}


		public bool ValidEmail(string newEmail)
		{
			if (newEmail.Length > 6 && newEmail.Contains("@"))
				return true;
			return false;
		}

        public int AddMoney(User user, int value)
        {
            int number = user.GetMoney() + value;
            user.SetMoney(number);
            return number;

        }

        public int UseMoney(User user, int value)
        {
            if (user.GetMoney() - value < 0)
            {
                ErrorLogger.Warn("UseMoney -> user dont have enough money in his wallet. should have used ValidMoneyUse first ");
                return -1;

            }
            user.SetMoney(user.GetMoney() - value);
            return user.GetMoney();
        }

        public int GetMoney(User user)
        {
            return user.GetMoney();
        }

        public bool ValidMoneyUse(User user, int value)
        {
            return user.GetMoney() - value >= 0;
        }

        public int GetId(User user)
        {
            return user.GetId();
        }

        public string GetPathToImage(User user)
        {
            return user.Get_pathToImage();
        }

        public bool SetPathToImage(User user, string value)
        {
            //if path not valid return false. not needed in the moment
            user.Set_pathToImage(value);
            return true;
        }

        public string GetEmail(User user)
        {
            return user.Get_email();
        }

        public int GetRank(User user)
        {
            return user.GetRank();
        }

        public bool SetRank(User user, int value)
        {
            if (value < 0) return false;
            user.SetRank(value);
            return true;
        }
    }

}