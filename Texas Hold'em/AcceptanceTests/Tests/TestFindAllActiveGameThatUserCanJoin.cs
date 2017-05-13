using AcceptanceTests.Data;
using NUnit.Framework;

namespace AcceptanceTests.Tests
{
    [TestFixture]
    class TestFindAllActiveGameThatUserCanJoin : TestCases
    {

        [SetUp]
        public void Init()
        {
        }

        [Test]
        public void TestFindAllActiveGamesWithoutGames()
        {
            int numOfActive = TestFindAllActiveGamesForState(State.Player);
            Assert.AreEqual(0, numOfActive);
        }

        [Test]
        public void TestFindAllActiveGamesHappyPath()
        {
            TestCreateNewGame(75, 2);
            TestCreateNewGame(72, 4);
            TestCreateNewGame(170, 15);
            TestCreateNewGame(3000, 15);
            TestCreateNewGame(10000, 4);
            TestCreateNewGame(10000, 8);
            TestCreateNewGame(250000, 20);
            TestCreateNewGame(400, 4);

            int numOfActive = TestFindAllActiveGamesForState(State.Player);

            Assert.AreEqual(8, numOfActive);
        }


        [Test]
        public void TestFindAllActiveDisActiveGamesHappyPath()
        {
            TestCreateNewGame(75, 2);
            TestCreateNewGame(72, 4);
            TestCreateNewGame(170, 15);
            TestCreateNewGame(3000, 15);
            int numOfActive = TestFindAllActiveGamesForState(State.Player);
            Assert.AreEqual(4, numOfActive);
        }


        [Test]
        public void TestFindAllActiveGamesOneRoomTaken()
        {
            TestCreateNewGame(75, 2);

            int numOfActive = TestFindAllActiveGamesForState(State.Player);
            Assert.AreEqual(1,numOfActive);

            User user = Users.Last.Value;
            User user2 = Users.Last.Value;

            TestJoinUserToFirstActiveGame(user,State.Player);
            TestJoinUserToFirstActiveGame(user2,State.Player);
            
            numOfActive = TestFindAllActiveGamesForState(State.Player);
            Assert.AreEqual(1,numOfActive);

        }

  
        [TearDown]
        public void Destroy()
        {
        }

    }
}
