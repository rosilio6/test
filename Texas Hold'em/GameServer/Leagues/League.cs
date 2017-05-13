using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Texas_Hold_em.Loggers;
using UserManagement;


namespace GameServer.Leagues
{
    public class League
    {
        private readonly IGameLogger _errorLogger = LoggerFactory.GetErrorLogger();

        public readonly LeagueType Type;
        private readonly List<User> _usersInLeague;
        public int MinRank { get; private set; }
        public int MaxRank { get; private set; }
        private readonly object _leagueLock = new object();

        public League(LeagueType type, int minRank, int maxRank)
        {
            Type = type;
            MinRank = minRank;
            MaxRank = maxRank;
            _usersInLeague = new List<User>();
        }

        public bool CanBeInLeague(User userToAdd)
        {
            lock (_leagueLock)
            {
                return !_usersInLeague.Contains(userToAdd) && RankFitsLeague(userToAdd);
            }
        }

        private bool RankFitsLeague(User userToAdd)
        {
            var userRank = userToAdd.GetRank();
            return userRank >= MinRank && userRank < MaxRank;
        }

        public void SetCriteriaForLeague(int newMinRank, int newMaxRank)
        {
            lock (_leagueLock)
            {
                MinRank = newMinRank;
                MaxRank = newMaxRank;
            }
        }

        public void AddUserToLeague(User userToAdd)
        {
            if (!CanBeInLeague(userToAdd)) return;
            lock (_leagueLock)
            {
                _usersInLeague.Add(userToAdd);
            }
        }

        public void RemoveUserFromLeague(User userToRemove)
        {
            if (CanBeInLeague(userToRemove)) return;
            lock (_leagueLock)
            {
                _usersInLeague.Remove(userToRemove);
            }
        }

        public List<User> GetUsers()
        {
            lock (_leagueLock)
            {
                return _usersInLeague.ToList();
            }
        }

        public User GetMaxRankedUser()
        {
            if (_usersInLeague != null)
            {
                lock (_leagueLock)
                {
                    var maxUserRank = MinRank;
                    var maxRankedUser = _usersInLeague.First();

                    foreach (var user in _usersInLeague)
                    {
                        if (user.GetRank() <= maxUserRank) continue;
                        maxUserRank = user.GetRank();
                        maxRankedUser = user;
                    }

                    return maxRankedUser;
                }
            }
            const string msg = "Could not get highest user - no users in league";
            _errorLogger.Error(msg);
            throw new InvalidOperationException(msg);

        }
    }
}
