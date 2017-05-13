using System.Collections.Generic;

namespace UserManagement
{
    public interface IUserDataBase
    {
        /// <summary>
        /// make an empty user list. for intilize only
        /// </summary>
           void CreateNewUserList();
        
        /// <summary>
        /// return a list of log in users
        /// </summary>
           List<User> GetLogInUserList();
        
        /// <summary>
        /// Remove the user from user list. if user is login it log out first.
        /// <param name="user"> .</param>
        /// </summary>
     
        bool RemoveUser(User user);

        /// <summary>
        /// return true if user is loged in
        /// </summary>
        bool IsUserLogIn(User user);

        /// <summary>
        /// return true if user log out successfuly
        /// </summary>
        bool UserLogOut(User user);

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
    }
}