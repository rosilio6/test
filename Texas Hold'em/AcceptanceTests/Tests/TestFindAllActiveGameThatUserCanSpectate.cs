using System;
using AcceptanceTests.Data;
using NUnit.Framework;

namespace AcceptanceTests.Tests
{
    [TestFixture]
    class TestFindAllActiveGameThatUserCanSpectate : TestCases
    {

        [SetUp]
        public void Init()
        {
        }

        [Test]
        public void TestFindAllActiveGamesWithoutGames()
        {
            int numOfActive = TestFindAllActiveGamesForState(State.Spectator);
            Assert.AreEqual(0, numOfActive);
        }

        [Test]
        public void TestFindAllActiveGamesHappyPath()
        {
            TestCreateNewGame(75, 2);
            TestCreateNewGame(72, 4);
            TestCreateNewGame(170, 15);
            TestCreateNewGameOnlyForPlayers(100, 25);
            TestCreateNewGame(3000, 15);
            TestCreateNewGame(10000, 4);
            TestCreateNewGameOnlyForPlayers(200, 25);
            TestCreateNewGame(10000, 8);
            TestCreateNewGame(250000, 20);
            TestCreateNewGame(400, 4);

            int numOfActive = TestFindAllActiveGamesForState(State.Spectator);

            Assert.AreEqual(8, numOfActive);
        }


        [Test]
        public void TestFindAllActiveGamesOnlyPlayerAllowedCase()
        {
            TestCreateNewGameOnlyForPlayers(100, 25);
            TestCreateNewGameOnlyForPlayers(200, 35);
            TestCreateNewGameOnlyForPlayers(300, 45);
            TestCreateNewGameOnlyForPlayers(400, 55);

            int numOfActive = TestFindAllActiveGamesForState(State.Spectator);
            Assert.AreEqual(0,numOfActive);

        }


        [Test]
        public void TestFindAllActiveGamesOneRoomTaken()
        {
            TestCreateNewGame(75, 2);

            int numOfActive = TestFindAllActiveGamesForState(State.Spectator);
            Assert.AreEqual(1, numOfActive);

            User user = Users.Last.Value;
            User user2 = Users.Last.Value;

            bool joinGame = TestJoinUserToFirstActiveGame(user, State.Spectator);
            Assert.True(joinGame);
            joinGame = TestJoinUserToFirstActiveGame(user2, State.Spectator);
            Assert.True(joinGame);

            numOfActive = TestFindAllActiveGamesForState(State.Spectator);
            Assert.AreEqual(1, numOfActive);

        }

        [TearDown]
        public void Destroy()
        {
        }

    }
}
