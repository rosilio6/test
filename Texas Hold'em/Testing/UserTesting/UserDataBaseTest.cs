
using GameServer;
using NUnit.Framework;
using UserManagement;

namespace Testing.UserTesting
{
    /// <summary>
    /// Tests the GameCenter methods
    /// </summary>
    [TestFixture]
    public class UnitDataBaseTest
    {
      
      //private readonly UserDataBase UserDataBase = new UserDataBase();
        UserDataBase UserDataBase = UserDataBase.Instance;



        [SetUp]
        public void SetUp()
            {
                UserDataBase s1 = UserDataBase.Instance;
            UserDataBase.CreateNewUserList();
        }

        [Test]
        public void AddNewUser()
        {
            UserDataBase.AddNewUser("boaz", "boazboaz");
            Assert.IsTrue(UserDataBase.CheckIfUserNameExist("boaz"));

            UserDataBase.AddNewUser("boaz12", "boazboaz");
            Assert.IsFalse(UserDataBase.CheckIfUserNameExist("DontExist"));

            
        }

        [Test]
        public void GetUserFromUserList()
        {
            
            User boaz = UserDataBase.AddNewUser("boaz", "boazboaz");
            Assert.IsTrue(boaz.Equals(UserDataBase.GetUserFromUserList("boaz")));

            User notBoaz = UserDataBase.AddNewUser("notBoaz", "boazboaz");
            Assert.IsFalse(boaz.Equals(UserDataBase.GetUserFromUserList("notBoaz")));

        }

        [Test]
        public void UserLogIn()
        {
            User boaz = UserDataBase.AddNewUser("boaz", "boazboaz");
            User notBoaz = UserDataBase.AddNewUser("notBoaz", "boazboaz");

            Assert.IsTrue(boaz.Equals(UserDataBase.UserLogIn("boaz","boazboaz")));

            Assert.IsFalse(boaz.Equals(UserDataBase.UserLogIn("boaz", "WrongPassword")));

            Assert.IsFalse(boaz.Equals(UserDataBase.UserLogIn("notBoaz", "boazboaz")));

            Assert.IsFalse(boaz.Equals(UserDataBase.UserLogIn("userDontExist", "idonthaveapassword")));

        }

    }

   }
