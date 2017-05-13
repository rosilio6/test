using System.Collections.ObjectModel;
using GameLogic;
using NUnit.Framework;
using UserManagement;

namespace Testing.GameTesting
{
    [TestFixture]
    class DealerTest
    {
        private Collection<Player> _players = new Collection<Player>();
        private Dealer _dealer;
        [SetUp]
        public void SetUp()
        {
                _players.Clear();
                User user;
                for (int i = 0; i < 4; i++)
                {
                    user = new User("Yossi", "1234", i);
                    user.SetMoney(500);
                    _players.Add(new Player(user));

                }
                _dealer = new Dealer(_players, GameType.NoLimit, 10, 4);
                _dealer.PrepareForGame();           
        }

        
        [TestCase] 
        public void TestIfGameCanBegin()
        {
            Assert.IsTrue(_dealer.CheckIfGameCanBegin());
            _dealer._minimumUsersForPlaying = 5;
            Assert.IsFalse(_dealer.CheckIfGameCanBegin());
            _dealer._minimumUsersForPlaying = 4;
        }

        [TestCase]
        public void TestPrepareGame()
        {
            Assert.IsTrue(_players[(_dealer._dealerButtonIndex + 1) % _players.Count].PlayerPot==10);
            Assert.IsTrue(_players[(_dealer._dealerButtonIndex + 2) % _players.Count].PlayerPot == 20);
            Assert.IsTrue(_dealer._currPlayerIndex==(_dealer._dealerButtonIndex+3)%_players.Count);
        }

        [TestCase]
        public void TestGetSumPots()
        {
            Player currPlayer = _players[_dealer._currPlayerIndex];
            _dealer.CheckIfRoundIsFinished(currPlayer);
            Assert.IsTrue(_dealer.GetSumPots() == 30);
        }


        [TestCase]
        public void TestAllIn()
        {
            Player currPlayer = _players[_dealer._currPlayerIndex];
            _dealer.AllIn(currPlayer);
            Assert.IsTrue(currPlayer.AmIInAllIn);
            Assert.IsTrue(currPlayer.Chip.Sum==0);
        }


        [TestCase]
        public void TestCheckPlayer()
        {
            Assert.IsTrue(_dealer.CheckPlayer(_players[_dealer._currPlayerIndex]));
            Assert.IsFalse(_dealer.CheckPlayer(_players[(_dealer._currPlayerIndex+1)%_players.Count]));
        }

        [TestCase]
        public void TestRaise()
        {
            Player currPlayer = _players[_dealer._currPlayerIndex];
            int prePotPlayer = currPlayer.PlayerPot;
            int preChip = currPlayer.Chip.Sum;
            _dealer.Raise(currPlayer,new Chip(100));
            Assert.IsTrue(currPlayer.PlayerPot==prePotPlayer+100);
            Assert.IsTrue(currPlayer.Chip.Sum==preChip-100);
        }

        [TestCase]
        public void TestCall()
        {
            Player currPlayer = _players[_dealer._currPlayerIndex];
            int prePotPlayer = currPlayer.PlayerPot;
            int preChip = currPlayer.Chip.Sum;
            int preRaise = _dealer._potManager._previousRaise.Sum;
            int difference = preRaise - currPlayer.PlayerPot;
            _dealer.Call(currPlayer);
            Assert.IsTrue(currPlayer.PlayerPot == prePotPlayer + difference);
            Assert.IsTrue(currPlayer.PlayerPot == preRaise);
            Assert.IsTrue(currPlayer.Chip.Sum == preChip - difference);
        }


        [TestCase]
        public void TestNextPlayer()
        {
            Player currPlayer = _players[_dealer._currPlayerIndex];
            int prePlayer = _dealer._currPlayerIndex;
            _dealer.NextPlayer(currPlayer,Turn.ActionType.Check,new Chip(0));
            Assert.IsTrue((prePlayer+1)%_players.Count==_dealer._currPlayerIndex);
        }

        [TestCase]
        public void TestIsEveryoneElseInAllIn()
        {
            
            Assert.IsFalse(_dealer.IsEveryoneElseInAllIn());
            foreach (Player P in _players)
            {
                P.AmIInAllIn = true;
            }
            Assert.IsTrue(_dealer.IsEveryoneElseInAllIn());

        }

        [TestCase]
        public void TestFold()
        {
            Player currPlayer = _players[_dealer._currPlayerIndex];
            _dealer.Fold(currPlayer);
            Assert.IsTrue(_dealer._standBy.Contains(currPlayer));
            Assert.IsFalse(_players.Contains(currPlayer));
            _players.Add(currPlayer);
        }

        [TestCase]
        public void TestAddStandByPlayer()
        {
            Player newPlayer = new Player();
            _dealer.AddStandByPlayer(newPlayer);
            Assert.IsTrue(_dealer._standBy.Contains(newPlayer));
            _dealer._standBy.Remove(newPlayer);
        }

        [TestCase]
        public void TestHasStandByPlayers()
        {
            Collection<Player>_prePlayers =_dealer.CheckIfStandByPlayers();
            Assert.IsTrue(_prePlayers == _players);
            Player newPlayer = new Player();
            _dealer.AddStandByPlayer(newPlayer);
            _prePlayers= _dealer.CheckIfStandByPlayers();
            Assert.IsTrue(_prePlayers.Contains(newPlayer));
            _prePlayers.Remove(newPlayer);

        }








    }

}
