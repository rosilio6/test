
using System.Collections.Generic;
using AcceptanceTests.Data;
using NUnit.Framework;

namespace AcceptanceTests.Tests
{
    [TestFixture]
    class TestFilterActiveGames : TestCases
    {
        private int _byIn;
        private int _numOfUsers;

        private LinkedList<GameClient> _games;
         
        [SetUp]
        public void Init()
        {
            _games = new LinkedList<GameClient>();
            SetUpGame(1522,2);
            _games.AddLast(Game);
            SetUpBunchOfGames();
            CreateListOfNewGame();
        }

        private void CreateListOfNewGame()
        {
            foreach (GameClient game in _games)
            {
                TestCreateNewGameFromInstance(game);
            }
        }

      

        private void SetUpBunchOfGames()
        {
            for (int j = 0; j < 10; j++)
            {
                _byIn++;
                _numOfUsers = (_numOfUsers%4 == 0)? 1 : (_numOfUsers+1);
                int i = 1;
                Game = new GameClient(_byIn,_numOfUsers);
                foreach (var u in Users)
                {
                    if (i <= _numOfUsers)
                    {
                        Game.Players.Add(u.UserName, u);
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }
                _games.AddLast(Game);
            }
        }
    

        [Test]
        public void TestFilterAllActiveGamesByName()
        {

            int user1 = TestFilterActiveGamesByUserName("User1");
            Assert.Zero(user1);
            int user2 = TestFilterActiveGamesByUserName("User2");
            Assert.Zero(user2);
            int user3 = TestFilterActiveGamesByUserName("User3");
            Assert.Zero(user3);
            int user4 = TestFilterActiveGamesByUserName("User4");
            Assert.Zero(user4);

        }



        [Test]
        public void TestFindAllActiveGamesByPotSize()
        {       

        }

      

        [TearDown]
        public void Destroy()
        {
            KillGame();
        }

    }
}
