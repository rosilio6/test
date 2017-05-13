
using System.Linq;
using AcceptanceTests.Data;
using NUnit.Framework;

namespace AcceptanceTests.Tests
{
    [TestFixture]
    class TeastLeaveActiveGame : TestCases
    {

        [SetUp]
        public void Init()
        {
            SetUpGame(520,8);
        }

     

        [Test]
        public void TestLeaveActiveGame()
        {
            TestJoinToGame(Game, Game.Players["User1"],State.Player);
            bool gameCapacity1 = TestLeaveActiveGame(Game, Game.Players["User1"]);
            Assert.True(gameCapacity1);
         
        }

        [Test]
        public void TestLeaveActiveGameAllPlayers()
        {
            TestCreateNewGameFromInstance(Game);
            int size_before = TestFindAllActiveGamesForState(State.Player);
            Assert.AreEqual(1, size_before);

            Assert.True(TestLeaveActiveGame(Game, Game.Players["User1"]));
            Assert.True(TestLeaveActiveGame(Game, Game.Players["User2"]));
            Assert.True(TestLeaveActiveGame(Game, Game.Players["User3"]));
            Assert.True(TestLeaveActiveGame(Game, Game.Players["User4"]));

        }

        [Test]
        public void TestLeaveActiveGameUserExistsButNotInCurrGame()
        {
            TestCreateNewGameFromInstance(Game);
            int size_before = TestFindAllActiveGamesForState(State.Player);
            Assert.AreEqual(1, size_before);

            User _user555 = new User("user555", "IamOnly555", "user555@gmail.com");
            TestRegister(_user555.UserName, _user555.Password, _user555.Email);
            TestLogin(_user555.UserName, _user555.Password);

            Assert.False(TestLeaveActiveGame(Game, _user555));

        }


        [Test]
        public void TestLeaveActiveGameTwicefail()
        {
            TestCreateNewGameFromInstance(Game);
            int size_before = TestFindAllActiveGamesForState(State.Player);
            Assert.AreEqual(1, size_before);
            User user = new User("User1", Game.Players["User1"].Password, Game.Players["User1"].Email.Address);
            Assert.True(TestLeaveActiveGame(Game, user));
            Assert.True(TestLeaveActiveGame(Game, user));

        }


        [Test]
        public void TestLeaveActiveGameUserNotExists()
        {
            TestCreateNewGameFromInstance(Game);
            int size_before = TestFindAllActiveGamesForState(State.Player);
            Assert.AreEqual(1, size_before);
            User u = new User("User48","bla","bla@gmail.com");
            Assert.False(TestLeaveActiveGame(Game, u));

        }

        [TearDown]
        public void Destroy()
        {
            KillGame();
        }


    }
}
