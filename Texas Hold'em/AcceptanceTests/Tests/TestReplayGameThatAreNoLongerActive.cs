
using AcceptanceTests.Data;
using NUnit.Framework;

namespace AcceptanceTests.Tests
{

    [TestFixture]
    class TestReplayGameThatAreNoLongerActive : TestCases
    {


        [SetUp]
        public void Init()
        {
            CreateReplay();
            LogGames();
        }

        [Test]
        public void TestReplayInactiveGame()
        {
            bool result = TestReplayInactiveGame(Game);
            Assert.True(result);
        }

        public void LogGames()
        {
            GameLog.GetGameLog().Log("1722", Game);
        }

        [TearDown]
        public void Destroy()
        {
            KillGame();
        }

    }
}
