
using AcceptanceTests.Data;
using NUnit.Framework;

namespace AcceptanceTests.Tests
{
    [TestFixture]
    class TestJoinToPokerGame : TestCases
    {
        private User _user555;
        private User _user5567;
        private User _user6;

        [SetUp]
        public void Init()
        {
            SetUpGame(520, 8);
            _user555 = new User("user555", "IamOnly555", "user555@gmail.com");
            TestRegister(_user555.UserName, _user555.Password, _user555.Email);
            TestLogin(_user555.UserName, _user555.Password);

            _user6 = new User("user6", "IamOnly6", "user6@gmail.com");
            TestRegister(_user6.UserName, _user6.Password, _user6.Email);
            TestLogin(_user6.UserName, _user6.Password);

            _user5567 = new User("user557", "IamOnly557", "user5567@gmail.com");
            TestRegister(_user5567.UserName, _user5567.Password, _user5567.Email);
            TestLogin(_user5567.UserName, _user5567.Password);

        }

        [Test]
        public void TestJoinToGameAsPlayerHappyPath()
        {
            bool joinGame = TestJoinToGame(Game,_user555,State.Player);
            Assert.True(joinGame);

        }

        [Test]
        public void TestJoinToGameAsSpectatorHappyPath()
        {
            bool joinGame = TestJoinToGame(Game, _user6, State.Spectator);
            Assert.True(joinGame);

        }

        [Test]
        public void TestJoinToFullTableGameAsSpectator()
        {
            Game.AmountOfPlayers = 4;
            bool joinGame = TestJoinToGame(Game, _user555, State.Spectator);
            Assert.True(joinGame);

        }

        [Test]
        public void TestJoinToFullTableGameAsPlayer()
        {
            Game.AmountOfPlayers = 4; 
            bool joinGame = TestJoinToGame(Game, _user5567, State.Player);
            Assert.False(joinGame);

        }

        [TearDown]
        public void Destroy()
        {
            KillGame();
            TestLogout(_user555);
            _user555 = null;
            _user6 = null;
        }
    }
}
