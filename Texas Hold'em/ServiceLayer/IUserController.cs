using UserManagement;

namespace ServiceLayer
{

    public interface IUserController
    {

        /// <summary>
        /// make an empty user list. for intilize only
        /// </summary>
        void CreateNewUserList();

        /// <summary>
        /// add a new user to the data base
        /// </summary>
        /// <param name="userName"> .</param>
        /// <param name="password"> .</param>

        User AddNewUser(string userName, string password);

        /// <summary>
        /// check if a new user can be created with this username and password
        /// </summary>
        /// <param name="userName"> .</param>
        /// <param name="password"> .</param>

        bool CanAddNewUser(string userName, string password);


        /// <summary>
        /// check if this user name exist in the data base
        /// </summary>
        /// <param name="userName"> .</param>

        bool CheckIfUserNameExist(string userName);

        /// <summary>
        /// return a User from the data base
        /// </summary>
        /// <param name="userName"> .</param>

        User GetUserFromUserList(string userName);

        /// <summary>
        /// user log in
        /// </summary>
        /// <param name="userName"> .</param>
        /// <param name="password"> .</param>

        User UserLogIn(string userName, string password);

        /// <summary>
        /// create a new user
        /// </summary>
        /// <param name="name"> new user name.</param>
        /// <param name="password"> new user password.</param>
        /// <param name="id"> user id in list .</param>

        User CreateUser(string name, string password, int id);

        /// <summary>
        /// check the user password and log in
        /// </summary>
        /// <param name="user"> user name .</param>
        /// <param name="password"> user password .</param>


        bool CheckLogInDetails(User user, string password);

        /// <summary>
        /// change this user password
        /// </summary>
        /// <param name="user"> .</param>
        /// <param name="oldPass"> .</param>
        /// <param name="newPass"> .</param>


        bool ChangePassword(User user, string oldPass, string newPass);

        /// <summary>
        /// make sure the new password is fallowing the syntex rules
        /// </summary>
        /// <param name="newPass"> .</param>

        bool ValidPassword(string newPass);

        /// <summary>
        /// change the user email
        /// </summary>
        /// <param name="user"> .</param>
        /// <param name="newEmail"> .</param>


        bool ChangeEmail(User user, string newEmail);

        /// <summary>
        /// make sure the new Email is fallowing the syntex rules
        /// </summary>
        /// <param name="newEmail"> .</param>

        bool ValidEmail(string newEmail);

        /// <summary>
        /// add money to that user wallet, return new value
        /// </summary>
        /// <param name="user"> </param>
        /// <param name="value"></param>

        int AddMoney(User user, int value);

        /// <summary>
        /// use money from user wallet, return false if wallet - value < 0
        /// </summary>
        /// <param name="user"> </param>
        /// <param name="value"></param>

        int UseMoney(User user, int value);

        /// <summary>
        /// get user money. in error return -1
        /// </summary>
        /// <param name="user"> </param>

        int GetMoney(User user);

        /// <summary>
        /// check if user have enough money to use
        /// </summary>
        /// <param name="user"> </param>
        /// <param name="value"></param>

        bool ValidMoneyUse(User user, int value);

        /// <summary>
        /// check if user have enough money to use
        /// </summary>
        /// <param name="user"> </param>

        int GetId(User user);


        /// <summary>
        /// get path to image
        /// </summary>
        /// <param name="user"> </param>

        string GetPathToImage(User user);


        /// <summary>
        /// set path to image
        /// </summary>
        /// <param name="user"> </param>
        /// <param name="value"></param>

        bool SetPathToImage(User user, string value);


        /// <summary>
        /// get user email
        /// </summary>
        /// <param name="user"> </param>

        string GetEmail(User user);


        /// <summary>
        /// get user rank
        /// </summary>
        /// <param name="user"> </param>
        /// <param name="value"></param>

        int GetRank(User user);


        /// <summary>
        /// set user rank
        /// </summary>
        /// <param name="user"> </param>
        /// <param name="value"></param>

        bool SetRank(User user, int value);
    }
}
