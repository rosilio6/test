using System;
using System.Collections.Generic;
using System.Linq;
using GameLogic;
using GameServer;
using GameServer.Interfaces;
using GameServer.Leagues;
using NUnit.Framework;
using UserManagement;

namespace Testing.SystemTesting
{
    /// <summary>
    /// Tests the GameCenter methods
    /// </summary>
    [TestFixture]
    public class GameCenterTest
    {
        private readonly GameCenter _gameCenter = GameCenter.Instance;
        private List<User> _users;
        private GameFactory _gameFactory;
        private IGame _activatedGame;
        private readonly IUserEnforcer _userEnforcer = new UserEnforcer();

        [SetUp]
        public void SetUp()
        {
            _users = new List<User>
            {
                UserFactory.CreateUser("Alice", "1234567"),
                UserFactory.CreateUser("Bob", "1234567"),
                UserFactory.CreateUser("Chris", "1234567")
            };
            _gameFactory = new PokerFactory();
            _activatedGame = _gameFactory.CreateAndActivateGame(_users, GameType.NoLimit, 1000, 10, 5, 10, true,
                new Chip(100));
        }

        [TearDown]
        public void TearDown()
        {
            if (_gameCenter.GetAllActiveGames().Contains(_activatedGame))
            {
                _gameCenter.DeactivateGame(_activatedGame);
            }

            foreach (var user in _users)
            {
                _userEnforcer.RemoveThisUserFromDataBase(user);
            }
        }

        [TestCase]
        public void AddRemoveUserToLeagueTest()
        {
            var user = _users.First();
            Assert.IsTrue(_gameCenter.IsUserInLeague(LeagueType.Unknown, user));

            var leagues = _gameCenter.LeagueTypes;

            foreach (var league in leagues)
            {
                AssertUserAddedRemovedToLeague(user, league);
            }
        }

        private void AssertUserAddedRemovedToLeague(User user, LeagueType leagueType)
        {
            _gameCenter.AddUserToLeague(leagueType, user);
            Assert.IsTrue(_gameCenter.IsUserInLeague(leagueType, user),
                "User couldn't be added to " + leagueType + " league.");
            _gameCenter.RemoveUserFromLeague(leagueType, user.PlayerName);
        }

        [TestCase]
        public void SetCriteriaToLeagueTest()
        {
            var defaultLeague = _gameCenter.GetDefaultLeagueObject();
            var oldMinRank = defaultLeague.MinRank;
            var oldMaxRank = defaultLeague.MaxRank;

            _gameCenter.SetLeagueCriteria(defaultLeague, oldMinRank+1000, oldMaxRank+1000);

            Assert.AreNotEqual(defaultLeague.MinRank, oldMinRank, "The leagues criteria didn't change at minRank");
            Assert.AreNotEqual(defaultLeague.MaxRank, oldMaxRank,"The leagues criteria didn't change at maxRank");
        }

        [TestCase]
        public void UpdateUserRankTest()
        {
            var user = _users.First();
            Assert.IsTrue(_gameCenter.IsUserInLeague(LeagueType.Unknown, user));
            var oldRank = user.GetRank();
            _gameCenter.UpdateUserRank(user, _activatedGame.GetProperties());
            var newRank = user.GetRank();

            Assert.AreNotEqual(newRank, oldRank);
        }

        [TestCase]
        public void GameActivationDeactivationTest()
        {
            var user = _users.First();
            Assert.IsNotNull(user, "User creation returned null.");
            
            var foundGames = _gameCenter.SearchActiveGamesBy(user.PlayerName);
            Assert.IsTrue(foundGames.Contains(_activatedGame), "Game wasn't found in active games after activation");

            _gameCenter.DeactivateGame(_activatedGame);
            foundGames = _gameCenter.SearchActiveGamesBy(user.PlayerName);
            Assert.IsFalse(foundGames.Contains(_activatedGame), "Game was found in active games after deactivation");
        }

        /// <summary>
        /// Tests search active games by properties
        /// </summary>
        [TestCase]
        public void SearchActiveGamesByPropertiesTest()
        {
            var properties = _activatedGame.GetProperties();
            var results = _gameCenter.SearchActiveGamesBy(properties);
            Assert.Contains(_activatedGame, results, "Game wasn't found in active games with properties: " + properties);
        }

        /// <summary>
        /// Tests search active games by player name
        /// </summary>
        [TestCase]
        public void SearchActiveGamesByPlayerNameTest()
        {
            var userOne = _users.First().PlayerName;
            var results = _gameCenter.SearchActiveGamesBy(userOne);
            Assert.Contains(_activatedGame, results, userOne + " wasn't found in active games players.");

            var userTwo = _users.ElementAt(2).PlayerName;
            results = _gameCenter.SearchActiveGamesBy(userTwo);
            Assert.Contains(_activatedGame, results, userTwo + " wasn't found in active games players.");
        }

        /// <summary>
        /// Test search active games by pot size
        /// </summary>
        [TestCase]
        public void SearchActiveGamesByPotSizeTest()
        {
            var potSize = _activatedGame.GetMainPotSize();
            var results = _gameCenter.SearchActiveGamesBy(potSize);
            Assert.Contains(_activatedGame, results, "Game wasn't found in active games with Pot Size: " + potSize);

        }

        /// <summary>
        /// Tests joining games
        /// </summary>
        [TestCase]
        public void JoinLeaveGamesTest()
        {
            var newPlayer = UserFactory.CreateUser("AliceBob", "1233211");
            var newSpectator = UserFactory.CreateUser("AliceBob2", "2344321");

            Assert.IsFalse(_activatedGame.GetProperties().ContainsPlayerName("AliceBob"));
            
            _gameCenter.JoinGameAsPlayer(newPlayer, _activatedGame);
            _gameCenter.JoinGameAsSpectator(newSpectator, _activatedGame);

            Assert.IsTrue(_activatedGame.GetProperties().ContainsPlayerName(newPlayer.PlayerName));
            Assert.IsFalse(_activatedGame.GetProperties().ContainsPlayerName(newSpectator.PlayerName));

            _gameCenter.LeaveGame(newPlayer, _activatedGame);
            _gameCenter.LeaveGame(newSpectator, _activatedGame);

            Assert.IsFalse(_activatedGame.GetProperties().ContainsPlayerName(newPlayer.PlayerName));
            
        }

        [TestCase]
        public void SaveAndLoadGameTest()
        {
            var gameHistory = _gameCenter.GameHistory;
            var gameProperties = _activatedGame.GetProperties();
            var gameId = gameProperties.GameIdentifier;

            gameHistory.SaveGame(_activatedGame);

            var loadedProperties = gameHistory.LoadGame(gameId);
            Assert.AreEqual(gameProperties, loadedProperties);

        }
    }
}
