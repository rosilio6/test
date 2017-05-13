
using System.Net.Mail;
using AcceptanceTests.Data;
using NUnit.Framework;

namespace AcceptanceTests.Tests
{
    [TestFixture]
    class TeatEditUserDetails : TestCases
    {
        [SetUp]
        public void Init()
        {
            AutoLogin();
        }
        [TearDown]
        public void Destroy()
        {
        }

        [Test]
        public void TestEditUserDetailsHappyPath()
        {
         
            User newuser = Users.Last.Value;
          //  newuser.UserName = "newUser##";
            newuser.Password = "newPassword##";
            newuser.Email= new MailAddress("newMail##@gmail.com", "newUser##");
            User updatedUser = TestEditUserDetails(Users.Last.Value , newuser);
          //  Assert.AreEqual(updatedUser.UserName, "newUser##");
            Assert.NotNull(updatedUser);
            Assert.AreEqual(updatedUser.Password, "newPassword##");
            Assert.AreEqual(updatedUser.Email.Address, "newMail##@gmail.com");
          //  Assert.AreEqual(updatedUser.Email.User, user.UserName = "newUser##");
     
        }

        [Test]
        public void TestEditUserDetailsWithWrongMailStructure()
        {

            User user = Users.Last.Value;
            user.Password = "newPassword##";
            user.Email = new MailAddress("newMail##@gmail.com", "newUser##");
            User updatedUser = TestEditUserDetails(Users.Last.Value , user);
            Assert.NotNull(updatedUser);

        }


        [Test]
        public void TestEditUserDetailsWithTakingUserName()
        {

            User user = new User(Users.Last.Value.UserName,"dsfsffs","fdsfs@fdfsd.com");
            user.UserName = "User1";
            user.Password = "newPassword##";
            user.Email = new MailAddress("newMail##@gmail.com", "newUser##");
            User updatedUser = TestEditUserDetails(Users.Last.Value, user);
            Assert.IsNull(updatedUser);

        }

        [Test]
        public void TestEditUserDetailsWithIlligalChar()
        {

            User user = Users.Last.Value;
          //  user.UserName = "בוזי הרצוג";
            user.Password = "מנסה להתאושש מההפסד";
            user.Email = new MailAddress("newMail##@gmail.com", "newUser##");
            User updatedUser = TestEditUserDetails(Users.Last.Value , user);
            Assert.NotNull(updatedUser);

        }

        [Test]
        public void TestEditUserThatDosntExists()
        {

            User user = new User("I am not exist", "passs", "passs@walla.com");
            user.UserName = "someone";
            user.Password = "newPassword##";
            user.Email = new MailAddress("newMail##@gmail.com", "newUser##");
            User updatedUser = TestEditUserDetails(Users.Last.Value, user);
            Assert.IsNull(updatedUser);

        }

        [Test]
        public void TestEditUserDetailsWithIleagalPassword()
        {

            User user = new User(Users.Last.Value.UserName, "dsfsffs", "fdsfs@fdfsd.com");
            user.UserName = "User1";
            user.Password = "12/";
            user.Email = new MailAddress("newMail##@gmail.com", "newUser##");
            User updatedUser = TestEditUserDetails(Users.Last.Value, user);
            Assert.IsNull(updatedUser);

        }

        [Test]
        public void TestEditUserMailHappyPath()
        {

            User user = new User(Users.Last.Value.UserName, "dsfsffs", "fdsfs@fdfsd.com");
            user.Email = new MailAddress("newMaileee@gmail.com", "User1");
            User updatedUser = TestEditUserDetails(Users.Last.Value, user);
            Assert.NotNull(updatedUser);

        }

        private void AutoLogin()
        {
            foreach (var u in Users)
            {
                TestLogin(u.UserName, u.Password);
            }
        }
    }
}
