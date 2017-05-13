using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using GameLogic;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Texas_Hold_em.Loggers;
using UserManagement;

namespace Testing.UserTesting
{
    /// <summary>
    /// Tests the GameCenter methods
    /// </summary>
    [TestFixture]
    public class UserHandlerTest
    {
   
        private readonly UserEnforcer UserHandler = new UserEnforcer();

        [SetUp]
        public void SetUp()
        {
       

        }


        [Test]
        public void CheckLogInDetailsTest()
        {
            User user = new User("name", "password", 22);
            Assert.IsTrue(UserHandler.CheckLogInDetails(user, "password"));

            Assert.IsFalse(UserHandler.CheckLogInDetails(user, "wrong password"));

            User user2 = new User("name2", "another password", 222);
            Assert.IsFalse(UserHandler.CheckLogInDetails(user2, "password")); //another user password

        }

        [Test]
        public void ChangePasswordTest()
        {
            User user = new User("name", "oldPass", 221);
            Assert.IsTrue(UserHandler.ChangePassword(user, "oldPass", "newPass"));
            Assert.IsTrue(user.CheckPassword("newPass"));


            User user2 = new User("name2", "oldPass", 225);
            Assert.IsTrue(UserHandler.ChangePassword(user2, "oldPass", "newPass"));
            Assert.IsFalse(user2.CheckPassword("oldPass"));

            User user3 = new User("name3", "oldPassword", 227);
            Assert.IsFalse(UserHandler.ChangePassword(user3, "oldPassword", "nop")); //to short
            Assert.IsFalse(user3.CheckPassword("nop"));
            Assert.IsTrue(user3.CheckPassword("oldPassword"));

        }

        [Test]
        public void ValidPasswordTest()
        {
            Assert.IsTrue(UserHandler.ValidPassword("aNiceNewPassword"));
            Assert.IsTrue(UserHandler.ValidPassword("2!4$paFDsword"));

            Assert.IsFalse(UserHandler.ValidPassword(""));  //to short
            Assert.IsFalse(UserHandler.ValidPassword("  "));//to short
            Assert.IsFalse(UserHandler.ValidPassword("Nop"));   //to short


        }

        [Test]
        public void ChangeEmailTest()
        {
            User user = new User("name", "oldPass", 221);
            Assert.IsTrue(UserHandler.ChangeEmail(user, "boaz@gmail.com"));
            Assert.IsTrue(UserHandler.ChangeEmail(user, "b!o$a54z@gmail.com"));
            Assert.IsTrue(UserHandler.ChangeEmail(user, "z@gmail.co.il"));


            User user2 = new User("name2", "oldPass", 225);
            Assert.IsTrue(UserHandler.ChangeEmail(user2, "boaz@gmail.com"));
            Assert.IsTrue(user2.Get_email().Equals("boaz@gmail.com"));
            Assert.IsFalse(user2.Get_email().Equals("WrongEmail@gmail.com"));


            User user3 = new User("name3", "oldPassword", 228);
            Assert.IsTrue(UserHandler.ChangeEmail(user3, "boaz@gmail.com"));
            Assert.IsFalse(UserHandler.ChangeEmail(user3, "b@g")); ;    //to short
            Assert.IsTrue(user3.Get_email().Equals("boaz@gmail.com"));
            Assert.IsFalse(user3.Get_email().Equals("b@g"));


            User user4 = new User("name3", "oldPassword", 2212);
            Assert.IsTrue(UserHandler.ChangeEmail(user4, "boaz@gmail.com"));
            Assert.IsFalse(UserHandler.ChangeEmail(user4, "boaz.gmail.com")); ; //no @
            Assert.IsTrue(user4.Get_email().Equals("boaz@gmail.com"));
            Assert.IsFalse(user4.Get_email().Equals("boaz.gmail.com"));
        }

        [Test]
        public void ValidEmailTest()
        {
            Assert.IsTrue(UserHandler.ValidEmail("sdsd@gmail.com"));
            Assert.IsTrue(UserHandler.ValidEmail("2!4$paFDsword@gmail.com"));

            Assert.IsFalse(UserHandler.ValidEmail(""));  //to short
            Assert.IsFalse(UserHandler.ValidEmail("  "));//to short
            Assert.IsFalse(UserHandler.ValidEmail("Nop"));   //to short
            Assert.IsFalse(UserHandler.ValidEmail("BestEmail.co.il"));   //no @
        }
    }
}