
using AcceptanceTests.Data;
using NUnit.Framework;

namespace AcceptanceTests.Tests
{
    [TestFixture]
    class TestLogout : TestCases
    {
        [SetUp]
        public void Init()
        {
        }

        [Test]
        public void TestHappyLogout()
        {
            User user = Users.Last.Value;
            TestLogin(user.UserName, user.Password);
            bool userState = TestLogout(user);
            Assert.True(userState);
        }

   
        [Test]
        public void TestLogoutUserNotLogedIn()
        {
            User user = new User("not logged in","fdfdfdfs","someMail@gmial.com");
            bool userState = TestLogout(user);
            Assert.False(userState);
        }

        [Test]
        public void TestHappyLoginLogoutInRaw()
        { 
            User _user555 = new User("user555", "IamOnly555", "user555@gmail.com");
            TestRegister(_user555.UserName, _user555.Password, _user555.Email);
            TestLogin(_user555.UserName, _user555.Password);
            bool userState = TestLogout(_user555);
            Assert.True(userState);
            bool userState2 = TestLogout(_user555);
            Assert.True(userState2);
        }


        [Test]
        public void TestHappyLoginLogoutInRawTwice()
        {
            User _user555 = new User("user555", "IamOnly555", "user555@gmail.com");
            TestRegister(_user555.UserName, _user555.Password, _user555.Email);
            TestLogin(_user555.UserName, _user555.Password);
            bool userState = TestLogout(_user555);
            Assert.True(userState);
            bool userState2 = TestLogout(_user555);
            Assert.True(userState2);
            bool userState3 = TestLogout(_user555);
            Assert.True(userState3);
        }


        [TearDown]
        public void Destroy()
        {
        }

    }
}
