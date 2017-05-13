using NLog;
using System;
using System.Collections.ObjectModel;
using System.Dynamic;
using GameLogic.Game_Client;
using UserManagement;
using Texas_Hold_em.Loggers;

namespace GameLogic
{
    public class Dealer : IDealer
    {
        /* for each pot in this collection,
                 * is associated with specific players in the current game
                 * if the winning player is'nt associated with this pot
                 * this pot needs to be split up between the players associated with it
                 * otherwise the associated winner takes this sidepot
                 */
        private Collection<Round> _gameRounds;
        public int _currRound = 0;
        private Player _lastPlayerInRound = null;
        private Collection<Player> _playerAndSpectators;
        public Collection<Player> _players;
        public GameType _gameType;
        public GameState _gameState;
        public int _smallBlind;
        public int _dealerButtonIndex;
        public int _minimumUsersForPlaying;
        public int _currPlayerIndex = -1;
        public PotManager _potManager;
        private bool someoneInAllIn = false;
        private bool someoneFoldAndLast = false;
        public Collection<Player> _standBy;

        private static readonly IGameLogger ErrorLogger = LoggerFactory.GetErrorLogger();
        private static readonly IGameLogger ActiveLogger = LoggerFactory.GetActiveLogger();


        public Dealer(Player player)
        {
            _gameType = GameType.NoLimit;
            _players = new Collection<Player>() {player};
            _gameState = GameState.WaitingForPlayers;
            _smallBlind = 1;
            _minimumUsersForPlaying = 2;
            _potManager = new PotManager(_players, _gameType, new Chip(2 * _smallBlind));
            _gameRounds = new Collection<Round>();
            _potManager._previousRaise = new Chip(2 * _smallBlind);
            _standBy = new Collection<Player>();


        }

        public Dealer(Collection<Player> players, GameType gameType, int smallBlind, int minimumUsersForPlaying)
        {
            _gameType = gameType;
            _players = players;
            _gameState = GameState.WaitingForPlayers;
            _smallBlind = smallBlind;
            _minimumUsersForPlaying = minimumUsersForPlaying;
            _potManager = new PotManager(_players, _gameType, new Chip(2 * _smallBlind));
            _gameRounds = new Collection<Round>();
            _potManager._previousRaise = new Chip(2 * _smallBlind);
            _standBy = new Collection<Player>();
        }

        public void UpdatePlayers(Collection<Player> players)
        {
            _players = players;
        }

        public bool CheckIfGameCanBegin()
        {
            return ((_players.Count >= _minimumUsersForPlaying));
        }


        public void PrepareForGame()
        {
            Random rnd = new Random();
            CheckIfStandByPlayers();
            _dealerButtonIndex = rnd.Next(1, _players.Count);
            ActiveLogger.Debug(
                "the dealer button is given to player: " + _players[_dealerButtonIndex].User.PlayerName);
            _currPlayerIndex = ((_dealerButtonIndex + 3) % _players.Count);
            ActiveLogger.Debug("the first player's turn is: " + _players[_currPlayerIndex].User.PlayerName);
            _lastPlayerInRound = _players[((_dealerButtonIndex + 3) % _players.Count)];
            _gameRounds.Add(new Round(_players));
            _gameState = GameState.GameMode;
            ActiveLogger.Debug( "Game State was changed to Game Mode");
            TakeBlinds();
            ActiveLogger.Debug( "Dealer, took blinds from the players");
        }

        private void TakeBlinds()
        {
            if (_players.Count == 2)
            {
                _players[_dealerButtonIndex].ReduceChips(_smallBlind);
                _players[(_dealerButtonIndex + 1) % _players.Count].ReduceChips(2 * _smallBlind);
            }
            else if (_players.Count > 2)
            {
                _players[(_dealerButtonIndex + 1) % _players.Count].ReduceChips(1 * _smallBlind);
                _players[(_dealerButtonIndex + 2) % _players.Count].ReduceChips(2 * _smallBlind);
            }
        }

        public bool AllIn(Player player)
        {
            if (CheckPlayer(player))
            {
                someoneInAllIn = true;
                player.AmIInAllIn = true;
                if (player.Chip.Sum > _potManager._previousRaise.Sum)
                {
                    _lastPlayerInRound = player;
                    _potManager._previousRaise = new Chip(player.Chip.Sum);
                }
                player.ReduceChips(player.Chip.Sum);
                NextPlayer(player, Turn.ActionType.AllIn, player.Chip);
                ActiveLogger.Debug( player.User.PlayerName + "AllIned successfully");
                return true;
            }
            ActiveLogger.Debug(  player.User.PlayerName + " Tried to Allin but did'nt succeed");
            return false;
        }


        public bool CheckPlayer(Player player)
        {
            return ((_gameState == GameState.GameMode) && (_players[_currPlayerIndex].Equals(player)));
        }


        public bool Raise(Player player, Chip chip)
        {
            if (CheckPlayer(player))
            {
                if (_potManager.LegalRaise(chip, _currRound))
                {

                    player.ReduceChips(chip.Sum);
                    _potManager._previousRaise = new Chip(chip.Sum);
                    _lastPlayerInRound = player;
                    NextPlayer(player, Turn.ActionType.Raise, chip);
                    ActiveLogger.Debug(  player.User.PlayerName + "Raised successfully");
                    return true;
                }
            }
            ActiveLogger.Debug( player.User.PlayerName + " Tried to Raise but did'nt succeed");
            return false;
        }

        public bool Call(Player player)
        {
            if (CheckPlayer(player))
            {
                if (_potManager._previousRaise.Sum - player.PlayerPot <= player.Chip.Sum)
                {
                    Chip difference = new Chip(_potManager._previousRaise.Sum - player.PlayerPot);

                    player.ReduceChips(difference.Sum);
                    NextPlayer(player, Turn.ActionType.Call, difference);
                    ActiveLogger.Debug(  player.User.PlayerName + "Called successfully");
                    return true;
                }
            }
            ActiveLogger.Debug(  player.User.PlayerName + " Tried to Call but did'nt succeed");
            return false;
        }

        public bool Check(Player player)
        {
            if (!CheckPlayer(player))
            {
                ActiveLogger.Debug(  player.User.PlayerName + " Tried to Check but did'nt succeed");
                return false;
            }
            if (player.PlayerPot == _potManager.GetMaxPlayerPot(_players))
            {
                NextPlayer(player, Turn.ActionType.Check, new Chip(0));
                ActiveLogger.Debug(  player.User.PlayerName + "Checked successfully");
                return true;
            }
            else
            {
                ActiveLogger.Debug(  player.User.PlayerName + " Tried to Check but did'nt succeed");
                return false;
            }

        }

        public void NextPlayer(Player player, Turn.ActionType action, Chip chip)
        {
            _gameRounds[_currRound].AddTurn(new Turn(player, action, chip));
            if (action != Turn.ActionType.Fold)
                _currPlayerIndex = ((_currPlayerIndex + 1) % _players.Count);
            else
            {
                _currPlayerIndex %= _players.Count;
            }
            ActiveLogger.Debug("the turn was passed to: " + _players[_currPlayerIndex].User.PlayerName);
            if (CheckIfRoundIsFinished(_players[_currPlayerIndex]))
            {
                _gameRounds.Add(new Round(_players));
                _currRound++;
                ActiveLogger.Debug("new Round has began");
            }

            while ((_players[_currPlayerIndex].AmIInAllIn) || (IsEveryoneElseInAllIn()))
            {
                _currPlayerIndex = ((_currPlayerIndex + 1) % _players.Count);
                ActiveLogger.Debug("the turn was passed to: " + _players[_currPlayerIndex].User.PlayerName);
                if (CheckIfRoundIsFinished(_players[_currPlayerIndex]))
                {

                    _gameRounds.Add(new Round(_players));
                    _currRound++;
                    ActiveLogger.Debug( "new Round has began");
                    return;
                }

            }

            if (someoneFoldAndLast)
            {
                _lastPlayerInRound = player;
                someoneFoldAndLast = false;
            }
        }

        public bool IsEveryoneElseInAllIn()
        {
            foreach (Player p in _players)
            {
                if (!p.AmIInAllIn)
                    return false;
            }
            return true;
        }

        public bool CheckIfRoundIsFinished(Player player)
        {
            if (_lastPlayerInRound == player)
            {
                if (someoneInAllIn)
                    _potManager.GoAllIn(_players);
                else
                {
                    if (!IsEveryoneElseInAllIn())
                    {
                        someoneInAllIn = false;
                    }
                }
                _potManager._previousRaise.Sum = 0;
                foreach (Player p in _players)
                {
                    _potManager.putMoney(p, p.PlayerPot);
                    p.PlayerPot = 0;
                }
                return true;
            }
            return false;
        }


        public bool Fold(Player player)
        {
            if (CheckPlayer(player))
            {
                _players.Remove(player);
                AddStandByPlayer(player);
                _potManager.putMoney(player, player.PlayerPot);
                _potManager.playerFolded(player);
                NextPlayer(player, Turn.ActionType.Fold, new Chip(0));
                if (_lastPlayerInRound == player)
                {
                    someoneFoldAndLast = true;
                }
                ActiveLogger.Debug(  player.User.PlayerName + "Folded successfully");
                return true;
            }
            
            ActiveLogger.Debug(  player.User.PlayerName + " Tried to Fold but did'nt succeed");
            return false;
        }

        public bool LeaveToLobby(Player player)
        {
            _players.Remove(player);
            player.User.ChangePlayerState(PlayerState.InLobby);
            ActiveLogger.Debug(  player.User.PlayerName + " left the game");
            return true;
        }

        public enum GameState
        {
            WaitingForPlayers,
            GameMode
        }

        public void GameOver(Collection<Player> players)
        {
            _potManager.Winner(players);

            foreach (var p in players)
            {
                Console.WriteLine("The Winner is Player" + p.Id);
                Console.WriteLine("Player Updated Pot : " + p.Chip.Sum);
            }


        }

        public int GetSumPots()
        {
            return _potManager.GetSum();
        }

        public void AllPlayerInAllIn()
        {
            foreach (Player player in _players)
            {
                _gameRounds[_currRound].AddTurn(new Turn(player, Turn.ActionType.Check, new Chip(0)));
            }
            _gameRounds.Add(new Round(_players));
            _currRound++;
        }

        public void AddStandByPlayer(Player player)
        {
            _standBy.Add(player);
        }

        public Collection<Player> CheckIfStandByPlayers()
        {
            foreach (Player p in _standBy)
            {
                _players.Add(p);
            }
            _standBy = new Collection<Player>();

            return _players;
        }

        public bool SetAction(Player player, Turn.ActionType action, int chip = 0)
        {
            switch (action)
            {
                case Turn.ActionType.Check:
                    return Check(player);

                case Turn.ActionType.Fold:
                    return Fold(player);

                case Turn.ActionType.Raise:

                    return Raise(player, new Chip(chip));

                case Turn.ActionType.Call:
                    return Call(player);

                case Turn.ActionType.AllIn:
                    return AllIn(player);

                default:
                    return false;
            }
        }
    }
}