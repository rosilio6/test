using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLogic;
using GameLogic.Game_Client;
using GameServer.Interfaces;
using Texas_Hold_em.Loggers;
using UserManagement;

namespace GameServer
{
    public class PokerFactory : GameFactory
    {
        private readonly IGameLogger _errorLogger = LoggerFactory.GetErrorLogger();

        public override IGame CreateAndActivateGame(List<User> users, GameType gameType, int buyInPolicy, int enteranceFee, int minPlayers, int maxPlayers, bool allowedSpectator, Chip chipPolicy)
        {
            if (CheckUsers(users) && CheckAmountOfPlayers(minPlayers, maxPlayers))
            {
                if (CheckByInPolicy(buyInPolicy) && CheckChipPolicy(chipPolicy) && CheckMinimumBet(enteranceFee))
                {
                    Collection<User> _users = new Collection<User>(users);
                    var newGame = new GameClient(_users, gameType, buyInPolicy, enteranceFee, minPlayers, maxPlayers,
                        allowedSpectator, chipPolicy);
                    GameCenter.Instance.ActivateGame(newGame);
                    return newGame;
                }
                else
                {
                    var msg = "Cannot create game - The given policie are invalid: Buy-In = " + buyInPolicy + " Chip = "+chipPolicy.Sum+" Minimum bet = "+enteranceFee;
                    _errorLogger.Error(msg);
                    throw new ArgumentException(msg);
                }
            }
            else
            {
                const string msg = "Cannot create game - the range for the amount of players is invalid";
                _errorLogger.Error(msg);
                throw new ArgumentOutOfRangeException(msg);
            }
        }

        public IGame LoadGame(Guid gameIdentifier)
        {
            var gameHistory = GameCenter.Instance.GameHistory;
            try
            {
               var gameProperties = gameHistory.LoadGame(gameIdentifier);
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _errorLogger.Error("Game " + gameIdentifier + " could not be loaded - " + ex.GetType().Name);
                throw;
            }
        }

        private bool CheckUsers(List<User> users)
        {
            if (users == null)
            {
                const string msg = "Cannot create game - list of users is null ";
                _errorLogger.Error(msg);
                throw new ArgumentNullException(msg+ nameof(users));
            }

            if (users.Any(user => user == null))
            {
                const string msg = "Cannot create game - one of the given users is null ";
                _errorLogger.Error(msg);
                throw new ArgumentNullException(msg);
            }
            return users.Count > 1;
        }

        private bool CheckByInPolicy(int buyInPolicy)
        {
            return buyInPolicy >= 0;
        }

        private bool CheckChipPolicy(Chip chipPolicy)
        {
            var sum = chipPolicy.Sum;
            return sum >= 0;
        }

        private bool CheckMinimumBet(int minimumBet)
        {
            return minimumBet > 0;
        }

        private bool CheckAmountOfPlayers(int minAmount, int maxAmount)
        {
            return minAmount > 1 && maxAmount > minAmount && maxAmount < 10;
        }
        
    }
}
