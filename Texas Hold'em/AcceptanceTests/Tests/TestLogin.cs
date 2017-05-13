
using AcceptanceTests.Data;
using NUnit.Framework;


namespace AcceptanceTests.Tests
{
    [TestFixture]
    class TestLogin : TestCases
    {
        [SetUp]
        public void Init()
        {
        }
        [TearDown]
        public void Destroy()
        {
        }

        [Test]
        public void TestHappyLogin()
        {
            User user = Users.First.Value;
            int retCode = TestLogin(user.UserName, user.Password);
            Assert.AreEqual(retCode , 0);
        }

        [Test]
        public void TestHappyLoginSameUserTwice()
        {
            User user = Users.First.Value;
            int retCode = TestLogin(user.UserName, user.Password);
            Assert.AreEqual(retCode, 0);
            int retCode2 = TestLogin(user.UserName, user.Password);
            Assert.AreEqual(retCode2, 0);

        }

        [Test]
        public void TestUserNotExits()
        {
            User user = new User("# Not Exists User #","p1","deadlyMotherFucker@gmail.com");
            int retCode = TestLogin(user.UserName, user.Password);
            Assert.AreEqual(retCode, 1);
        }

        [Test]
        public void TestWrongPassword()
        {
            User user = Users.First.Value;
            int retCode = TestLogin(user.UserName, "Wrong Password");
            Assert.AreEqual(retCode, 1);
        }

        [Test]
        public void TestHappyLoginNewUserInFirstTime()
        {
            User _user555 = new User("user555", "IamOnly555", "user555@gmail.com");
            TestRegister(_user555.UserName, _user555.Password, _user555.Email);
            TestLogin(_user555.UserName, _user555.Password);
            bool userState = TestLogout(_user555);
            Assert.True(userState);
        }
    }
}
