using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GameLogic;
using GameLogic.Game_Client;
using GameServer.GameSaving;
using GameServer.Interfaces;
using GameServer.Leagues;
using Texas_Hold_em.Loggers;
using UserManagement;
using User = UserManagement.User;

namespace GameServer
{
    public class GameCenter : IGameCenter
    {
        public static readonly GameCenter Instance = new GameCenter();
        private static readonly IGameLogger ErrorLogger = LoggerFactory.GetErrorLogger();

        private readonly SortedDictionary<LeagueType, League> _leaguesByType;
        private readonly Dictionary<User, League> _usersLeagues;
        private LeagueType _defaultLeagueType;

        private DateTime _lastLeagueUpdate;
        private readonly TimeSpan _timeIntervalForUpdate = TimeSpan.FromDays(7);

        private readonly List<IGame> _activeGames;
        private readonly Dictionary<User, List<IGame>> _usersInActiveGame;

        private readonly TaskScheduler _gameSchedualer = TaskScheduler.Default;
        public GameHistory GameHistory { get; }
        private Thread _updateRunner;

        private readonly object _serverLock = new object();

        private GameCenter()
        {
            _leaguesByType = new SortedDictionary<LeagueType, League>();
            InitLeagues();
            _defaultLeagueType = LeagueType.Unknown;

            _usersLeagues = new Dictionary<User, League>();
            _lastLeagueUpdate = DateTime.Now;

            _activeGames = new List<IGame>();
            _usersInActiveGame = new Dictionary<User, List<IGame>>();
            
            GameHistory = new GameHistory();
            StartUpdateRunner();
        }

        private void StartUpdateRunner()
        {
            _updateRunner = new Thread(() =>
            {
                while (true)
                {
                    lock (_serverLock)
                    {
                        if (!CanUpdateLeague())
                        {
                            Thread.Sleep(TimeSpan.FromMinutes(1));
                            continue;
                        }
                        UpdateLeagues();
                    }
                }
            });
            _updateRunner.Start();
        }

        private void InitLeagues()
        {
            var previousLeague = LeagueType.Unknown;
            foreach (LeagueType type in Enum.GetValues(typeof(LeagueType)))
            {
                if (type != previousLeague)
                {
                    _leaguesByType.Add(type, new League(type, (int)previousLeague, (int)type));
                }
            }
        }

        public List<LeagueType> LeagueTypes => new List<LeagueType>(_leaguesByType.Keys);

        public League GetLeagueByType(LeagueType leagueType)
        {
            return _leaguesByType[leagueType];
        }

        private bool CanUpdateLeague()
        {
            return (DateTime.Now - _lastLeagueUpdate) >= _timeIntervalForUpdate;
        }

        public void UpdateLeagues()
        {
            var users = _usersLeagues.Keys;
            foreach (var user in users)
            {
                if (!_usersLeagues.ContainsKey(user)) continue;
                var league = _usersLeagues[user];
                league.RemoveUserFromLeague(user);
            }
            _usersLeagues.Clear();

            var leagues = _leaguesByType.Values;
            foreach (var user in users)
            {
                if (IsUserInLeague(LeagueType.Unknown, user) && GameHistory.NumOfGamesFinished(user) < 10)
                {
                    continue;
                }
                foreach (var league in leagues)
                {
                    if (!league.CanBeInLeague(user)) continue;
                    AddUserToLeague(league.Type, user);
                    break;
                }
            }
            _lastLeagueUpdate = DateTime.Now;
        }

        public void SetLeagueCriteria(League league, int newMinRank, int newMaxRank)
        {
            league.SetCriteriaForLeague(newMinRank, newMaxRank);
        }

        public void AddUserToLeague(LeagueType leagueType, User userToAdd)
        {
            lock (_serverLock)
            {
                var league = _leaguesByType[leagueType];
                if (!league.CanBeInLeague(userToAdd)) return;
                league.AddUserToLeague(userToAdd);
                _usersLeagues.Add(userToAdd, league);
            }
        }

        public bool IsUserInLeague(LeagueType leagueType, User userTosearch)
        {
            var league = _leaguesByType[leagueType];

            if (!_usersLeagues.ContainsKey(userTosearch))
                return false;

            var foundLeague = _usersLeagues[userTosearch];
            return foundLeague.Type == league.Type;
        }

        public void RemoveUserFromLeague(LeagueType leagueType, string userName)
        {
            var league = _leaguesByType[leagueType];
            var usersInLeague = league.GetUsers();
            foreach (var user in usersInLeague)
            {
                if (!user.PlayerName.Equals(userName)) continue;
                league.RemoveUserFromLeague(user);
                _usersLeagues.Remove(user);
            }
        }

        public void ActivateGame(IGame gameToActivate)
        {
            var gameProperties = gameToActivate.GetProperties();

            if (gameProperties.IsClosed)
            {
                ErrorLogger.Warn("Game " + gameProperties.GameIdentifier + " is closed, could not activate.");
                return;
            }

            if (_activeGames.Contains(gameToActivate))
            {
                ErrorLogger.Error("During activation, the game was already in active games.");
                throw new InvalidOperationException("Game already active: " + gameProperties.GameIdentifier);
            }

            _activeGames.Add(gameToActivate);
            var players = gameToActivate.GetUsersInRound(0);

            foreach (var player in players)
            {
                if (_usersInActiveGame.ContainsKey(player))
                {
                    var games = _usersInActiveGame[player];
                    if (games != null && !games.Contains(gameToActivate))
                    {
                        games.Add(gameToActivate);
                    }
                }
                else
                {
                    _usersInActiveGame.Add(player, new List<IGame> {gameToActivate});
                }
            }

            var newGame = Task.Factory.StartNew(gameToActivate.Run, CancellationToken.None, TaskCreationOptions.None, _gameSchedualer);
            newGame.ContinueWith(task => DeactivateGame(gameToActivate), _gameSchedualer);
        }

        public void DeactivateGame(IGame gameToClose)
        {
            var gameProperties = gameToClose.GetProperties();

            if (!gameProperties.IsClosed)
            {
                ErrorLogger.Warn("Game " + gameProperties.GameIdentifier + " is not closed, could not deactivate.");
                return;
            }

            if (!_activeGames.Contains(gameToClose))
            {
                ErrorLogger.Error("During deactivation, the game was missing from active games.");
                throw new InvalidOperationException("Game not found: " + gameProperties.GameIdentifier);
            }

            GameHistory.SaveGame(gameToClose);
            _activeGames.Remove(gameToClose);

            var lastTurn = gameProperties.RoundInGame.Count;
            var users = gameToClose.GetUsersInRound(lastTurn);
            foreach (var user in users)
            {
                LeaveGame(user, gameToClose);
            }
        }

        public void UpdateUserRank(User player, GameProperties gameProperties)
        {
            var hands = gameProperties.HighestHand;
            var handRank =
                (from hand in hands where hand.Value.Equals(player) select (int) hands.Max().Key - (int) hand.Key)
                .FirstOrDefault();

            var bets = gameProperties.HighestBet;
            var betRank =
                (from bet in bets where bet.Value.Equals(player) select bets.Max().Key - bet.Key)
                .FirstOrDefault();

            player.SetRank(player.GetRank()+handRank+betRank);
        }

        public List<IGame> GetAllActiveGames()
        {
           // lock (_serverLock)
          //  {
                return _activeGames.ToList();
           // }
        }

        public List<IGame> SearchActiveGamesBy(string playerName)
        {
            lock (_serverLock)
            {
                return _activeGames.Where(game => game.GetProperties().ContainsPlayerName(playerName)).ToList();
            }
        }

        public List<IGame> SearchActiveGamesBy(int mainPotSize)
        {
            lock (_serverLock)
            {
                return _activeGames.Where(game => game.GetMainPotSize() == mainPotSize).ToList();
            }
        }

        public List<IGame> SearchActiveGamesBy(GameProperties gameProperties)
        {
            lock (_serverLock)
            {
                return _activeGames.Where(game => game.GetProperties().Equals(gameProperties)) as List<IGame>;
            }
        }

        public void JoinGameAsPlayer(User userToJoin, IGame gameToJoin)
        {
            var properties = gameToJoin.GetProperties();
            if (gameToJoin.GetProperties().IsClosed)
            {
                var msg = "The game "+properties.GameIdentifier+" to join was closed!";
                ErrorLogger.Error(msg);
                throw new InvalidExpressionException(msg);
            }

            var userState = userToJoin.GetPlayerState();
            if (userState == PlayerState.InLobby)
            {
                JoinGame(userToJoin, gameToJoin);
                userToJoin.ChangePlayerState(PlayerState.Player);
                gameToJoin.AddPlayer(userToJoin);
            }
            else
            {
                var msg = "User "+userToJoin.PlayerName+"["+userState+"] can't join the game: " + gameToJoin.GetProperties().GameIdentifier;
                ErrorLogger.Error(msg);
                throw new InvalidOperationException(msg);
            }
        }

        public void JoinGameAsSpectator(User userToJoin, IGame gameToJoin)
        {
            var properties = gameToJoin.GetProperties();
            if (gameToJoin.GetProperties().IsClosed)
            {
                var msg = "The game " + properties.GameIdentifier + " to join was closed!";
                ErrorLogger.Error(msg);
                throw new InvalidExpressionException(msg);
            }
            var userState = userToJoin.GetPlayerState();
            if (userState == PlayerState.InLobby)
            {
                JoinGame(userToJoin, gameToJoin);
                userToJoin.ChangePlayerState(PlayerState.Spectator);
                gameToJoin.AddSpectator(userToJoin);
            }
            else
            {
                var msg = "User " + userToJoin.PlayerName + "[" + userState + "] can't join the game: " + gameToJoin.GetProperties().GameIdentifier;
                ErrorLogger.Error(msg);
                throw new InvalidOperationException(msg);
            }
        }

        private void JoinGame(User userToJoin, IGame gameToJoin)
        {
            lock (_serverLock)
            {
                if (_usersInActiveGame.ContainsKey(userToJoin))
                {
                    _usersInActiveGame[userToJoin].Add(gameToJoin);
                }
                else
                {
                    _usersInActiveGame.Add(userToJoin, new List<IGame> {gameToJoin});
                }
            }
        }

        public List<IGame> GetAllActiveSpectatableGames()
        {
            return _activeGames.Where(activeGame => activeGame.GetProperties().IsGameAllowSpectator).ToList();
        }

        public void LeaveGame(User userToLeave, IGame game)
        {
            game.DeleteUser(userToLeave);
            lock (_serverLock)
            {
                if (!_usersInActiveGame.ContainsKey(userToLeave)) return;

                if (_usersInActiveGame[userToLeave].Contains(game))
                {
                    _usersInActiveGame[userToLeave].Remove(game);
                }
                else
                {
                    const string msg = "The game is not registered with the user.";
                    ErrorLogger.Warn(msg);
                    throw new InvalidExpressionException(msg);
                }
            }
            
            UpdateUserRank(userToLeave, game.GetProperties());
        }

        private bool IsHighestRankingUser(User user)
        {
            lock (_serverLock)
            {
                var userLeague = _usersLeagues[user];
                var highestLeague = _leaguesByType.Last().Value;
                var highestUser = highestLeague.GetMaxRankedUser();

                return userLeague != null && userLeague.Type >= highestLeague.Type &&
                       user.GetRank() >= highestUser.GetRank();
            }
        }

        public void SetDefaultLeagueType(User setter, LeagueType defaultType)
        {
            _defaultLeagueType = defaultType;
        }

        public LeagueType GetDefaultLeague()
        {
            return _defaultLeagueType;
        }

        public League GetDefaultLeagueObject()
        {
            lock (_serverLock)
            {
                return _leaguesByType[_defaultLeagueType];
            }
        }

        public DateTime GetLastLeagueUpdate()
        {
            return _lastLeagueUpdate;
        }
    }
}
