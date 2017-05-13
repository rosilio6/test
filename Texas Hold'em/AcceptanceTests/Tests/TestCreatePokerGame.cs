
using NUnit.Framework;

namespace AcceptanceTests.Tests
{
    [TestFixture]
    class TestCreatePokerGame : TestCases
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
        public void TestCreateNewGameHappyPath()
        {
            int byInPolicy = 50;
            int numOfPlayers = 8;
            bool gameCration = TestCreateNewGame(byInPolicy, numOfPlayers);
            Assert.True(gameCration);

        }

      

        [Test]
        public void TestCreateNewGameHappyIlegalPepole()
        {
            int byInPolicy = 50;
            int numOfPlayers = 1;
            bool gameCration = TestCreateNewGame(byInPolicy, numOfPlayers);
            Assert.False(gameCration);

        }

        [Test]
        public void TestCreateNewGameHappyNegativePepole()
        {
            int byInPolicy = 75;
            int numOfPlayers = -1;
            bool gameCration = TestCreateNewGame(byInPolicy, numOfPlayers);
            Assert.False(gameCration);

        }

        [Test]
        public void TestCreateWithoutPolicy()
        {
            int byInPolicy = 0;
            int numOfPlayers = 8;
            bool gameCration = TestCreateNewGame(byInPolicy, numOfPlayers);
            Assert.True(gameCration);

        }

        [Test]
        public void TestCreateGameWithNagativePolicy()
        {
            int byInPolicy = -50;
            int numOfPlayers = 8;
            bool gameCration = TestCreateNewGame(byInPolicy, numOfPlayers);
            Assert.False(gameCration);

        }

    }
}
