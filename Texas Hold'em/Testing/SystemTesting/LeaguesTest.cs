
using System.Collections.Generic;
using GameServer;
using GameServer.Leagues;
using NUnit.Framework;
using UserManagement;

namespace Testing.SystemTesting
{
    [TestFixture]
    public class LeaguesTest
    {
        private GameCenter _gameCenter = GameCenter.Instance;
        private User _user;
        private readonly IUserEnforcer _userHandler = new UserEnforcer();

        [SetUp]
        public void SetUp()
        {
            _user = UserFactory.CreateUser("Dillen", "0987654321");
        }

        [TearDown]
        public void TearDown()
        {
            _userHandler.RemoveThisUserFromDataBase(_user);
        }

        [TestCase]
        public void CanBeInLeagueTest()
        {
            var league = _gameCenter.GetLeagueByType(LeagueType.Silver);

            _user.SetRank((int)LeagueType.Gold);
            Assert.IsFalse(league.CanBeInLeague(_user));

            _user.SetRank((int) LeagueType.Silver + 1);
            Assert.IsTrue(league.CanBeInLeague(_user));
        }

        [TestCase]
        public void AddRemoveUserToLeagueTest()
        {
            var league = _gameCenter.GetLeagueByType(LeagueType.Gold);
            _user.SetRank((int) LeagueType.Gold + 10);
            Assert.IsFalse(league.GetUsers().Contains(_user));

            league.AddUserToLeague(_user);
            Assert.Contains(_user, league.GetUsers());

            league.RemoveUserFromLeague(_user);
            Assert.IsFalse(league.GetUsers().Contains(_user));
        }

        [TestCase]
        public void MaxRankedUserTest()
        {
            var league = _gameCenter.GetLeagueByType(LeagueType.Unknown);
            var users = league.GetUsers();
            _user.SetRank(league.MaxRank);
            if (users.Count == 0)
            {
                users.Add(_user);
            }

            var maxRankedUser = league.GetMaxRankedUser();
            Assert.AreEqual(maxRankedUser, _user);
        }
    }
}
