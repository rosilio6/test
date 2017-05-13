using System;
using System.Collections.Generic;
using System.IO;
using GameLogic;
using Newtonsoft.Json;
using Texas_Hold_em.Loggers;
using UserManagement;

namespace GameServer.GameSaving
{
    public class GameHistory
    {
        private readonly IGameLogger _errorLogger = LoggerFactory.GetErrorLogger();

        private const string DirectoryName = "GameHistory";
        private readonly string _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        private readonly Dictionary<User, List<IGame>> _usersGameHistory;
        private readonly SortedDictionary<IGame, DateTime> _gamesHistory;

        public GameHistory()
        {
            _usersGameHistory = new Dictionary<User, List<IGame>>();
            _gamesHistory = new SortedDictionary<IGame, DateTime>();
        }

        /// <summary>
        /// Saves the given game data for replays
        /// </summary>
        /// <param name="gameToSave">The game the user wants to save</param>
        public void SaveGame(IGame gameToSave)
        {
            if (_gamesHistory.ContainsKey(gameToSave)) return;
            var gameProperties = gameToSave.GetProperties();

            try
            {
                SaveGameToFile(gameProperties);
                var lastRound = gameProperties.RoundInGame.Count - 1;
                var users = gameToSave.GetUsersInRound(lastRound);
                foreach (var user in users)
                {
                    if (_usersGameHistory.ContainsKey(user))
                    {
                        if (!_usersGameHistory[user].Contains(gameToSave))
                        {
                            _usersGameHistory[user].Add(gameToSave);
                        }
                    }
                    else
                    {
                        _usersGameHistory.Add(user, new List<IGame> {gameToSave});
                    }
                }
                _gamesHistory.Add(gameToSave, DateTime.Now.ToUniversalTime());
            }
            catch (Exception ex)
            {
                _errorLogger.Error("Game " + gameProperties.GameIdentifier + " could not be saved - " +
                                   ex.GetType().Name);
            }
        }

        
        /// <summary>
        /// Loads a game from the give game identifier
        /// </summary>
        /// <param name="gameIdentifier">The identifier of a game you want to load</param>
        /// <returns></returns>
        public GameProperties LoadGame(Guid gameIdentifier)
        {
            var gameIdString = gameIdentifier.ToString();
            var filePath = GetGameFilePath(gameIdString);

            var gameJson = File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<GameProperties>(gameJson);
        }

        private string GetGameFilePath(string filename)
        {
            return Path.Combine(_baseDirectory, DirectoryName, filename);
        }

        private void SaveGameToFile(GameProperties gameProperties)
        {
            var gameIdentifier = gameProperties.GameIdentifier.ToString();
            var gameJson = JsonConvert.SerializeObject(gameProperties);
            var filePath = GetGameFilePath(gameIdentifier);

            File.WriteAllText(filePath, gameJson);
        }

        public int NumOfGamesFinished(User user)
        {
            return _usersGameHistory.ContainsKey(user) ? _usersGameHistory[user].Count : 0;
        }
    }
}
