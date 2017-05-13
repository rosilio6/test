using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Texas_Hold_em.Loggers;
using UserManagement;


namespace GameLogic.Game_Client
{
    public class GameClient : IGame
    {
        private List<IObserver<PlayerNotification>> _observers;
        private Collection<Player> _players;
        private readonly Collection<User> _spectators;
        private readonly int _entranceFee;
        public readonly Dealer _dealer;
        private readonly Deck _deck;
        private readonly int _maxPlayers;
        private readonly bool _allowedSpectator;
        private readonly Chip _chipPolicy;
        private readonly int _buyInPolicy;

        private static readonly IGameLogger ErrorLogger = LoggerFactory.GetErrorLogger();
        private static readonly IGameLogger ActiveLogger = LoggerFactory.GetActiveLogger();
        public GameClient(Collection<User> users, GameType gameType, int buyInPolicy, int enteranceFee, int minPlayers,
                                int maxPlyaers, bool allowedSpectator, Chip chipPolicy)
        {
            _observers = new List<IObserver<PlayerNotification>>();
            checkPramaters(users, buyInPolicy, enteranceFee, minPlayers, maxPlyaers, chipPolicy);
            _entranceFee = enteranceFee;
            _players = new Collection<Player>();
            _spectators = new Collection<User>();
            foreach (var u in users)
            {
                if (u.GetPlayerState() == PlayerState.Player)
                {
                    if (u.GetMoney() >= buyInPolicy)
                        _players.Add(new Player(u));
                    else
                    {
                        ErrorLogger.Error("Player" + u.GetId() + " has'nt enough moeny");
                    }
                }
                else if (u.GetPlayerState() == PlayerState.Spectator)
                {
                    _spectators.Add(u);
                }
            }
            _maxPlayers = maxPlyaers;
            _allowedSpectator = allowedSpectator;
            _chipPolicy = chipPolicy;
            _buyInPolicy = buyInPolicy;


            _dealer = new Dealer(_players, gameType, enteranceFee, minPlayers);
            _deck = new Deck();

        }

        private void checkPramaters(Collection<User> users, int buyInPolicy, int enteranceFee, int minPlayers, int maxPlyaers, Chip chipPolicy)
        {
            if ((users.Count < minPlayers) || (buyInPolicy < 0) || (enteranceFee < 0) || (minPlayers < 2) ||
                (users.Count > maxPlyaers) || (chipPolicy.Sum > 0))

                ErrorLogger.Error("Insert Worng Paramters");
        }

        public Collection<Player> GetPlayers()
        {
            return _players;
        }


        public void AddPlayer(User user)
        {
            var newPlayer = new Player(user);
            if (_dealer._gameState != Dealer.GameState.GameMode)
            {
                _players[_players.Count] = newPlayer;
                _dealer.UpdatePlayers(_players);
            }
            else
            {
                _dealer.AddStandByPlayer(newPlayer);
            }

        }

        public void DeleteUser(User user)
        {

            foreach (Player p in _players)
            {
                if (p.Id == user.GetId())
                {
                    _players.Remove(p);

                    break;
                }
            }
            _dealer.UpdatePlayers(_players);
        }

        public void DealCards()
        {
            foreach (Player t in _players)
            {
                t.SetCards(_deck.Pick2Card());
            }
        }

        public void Run()
        {
            /*  ***CLIENT***
            if (_dealer.CheckIfGameCanBegin())
            {
                _dealer.PrepareForGame();
                _players = _dealer._players;
                DealCards();
                var flop = _deck.Pick3Card();
                var river = _deck.Pick1Card();
                var turn = _deck.Pick1Card();
                var allFlop = new Collection<Card>(flop.ToList()) { river[0], turn[0] };
                SetHands(allFlop);

                for (var i = 0; i < 4; i++)
                {
                    {
                        switch (i)
                        {
                            case 0:
                                ShowInBoard(flop);
                                break;
                            case 1:
                                ShowInBoard(river);
                                break;
                            case 2:
                                ShowInBoard(turn);
                                break;
                        }
                    }
                    else
                    {
                        GameOver();
                        return;

                    }
                }
                CompareHands();
            }
            else
            {
                Console.WriteLine("There isn't enough players!");
            }
            */
        }


        private void SetHands(Collection<Card> allFlop)
        {
            foreach (var t in _players)
            {
                t.SetHand(allFlop);
            }
        }

        private void CompareHands()
        {
            ShowHands();
            var winners = new Collection<Player> { _players[0] };
            foreach (var p in _players)
            {
                if (p.Hand.Compare(winners[0].Hand) > 0)
                {
                    winners = new Collection<Player> { p };
                }
                if (p.Hand.Compare(winners[0].Hand) != 0) continue;
                if (!winners.Contains(p))
                    winners.Add(p);
            }
            _dealer.GameOver(winners);
        }

        private void ShowHands()
        {
            foreach (var p in _players)
            {
                Console.WriteLine("Player" + p.Id + " Card:" + p.Cards[0] + " " + p.Cards[1] + " " + p.Hand.HandTitle);

            }
        }

        private void GameOver()
        {
            if (_players.Count == 1)
                _dealer.GameOver(_players);
            Console.WriteLine("The Game Stop!");
        }

        private void ShowInBoard(Collection<Card> cards)
        {
            Console.WriteLine("Cards:");
            foreach (var card in cards)
            {
                Console.Write(card.ToString());
            }
            Console.WriteLine();

        }

        public Boolean ChecksBets(Turn.ActionType operation, int chip = 0)
        {
            if (!_dealer.IsEveryoneElseInAllIn())
            {
                if (_players[_dealer._currPlayerIndex].TakeAction(_dealer, operation, chip))
                {
                    while (!_dealer.CheckIfRoundIsFinished(_players[_dealer._currPlayerIndex]))
                    {
                        if (_players[_dealer._currPlayerIndex].TakeAction(_dealer, operation, chip) != true)
                        {
                            Console.WriteLine("Betting Went Worng!");
                            return false;
                        }
                        if (_players.Count == 1)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Betting Went Worng!");
                    return false;
                }
            }
            else
            {
                _dealer.AllPlayerInAllIn();
            }
            Console.WriteLine("Round" + _dealer._currRound + "  is Finished");
            Console.WriteLine("Pot Amount: " + _dealer.GetSumPots());
            ShowPlayerChip();
            return true;
        }



        private void ShowPlayerChip()
        {
            foreach (var p in _players)
            {
                Console.WriteLine("Player" + p.Id + " Chip Amount: " + p.Chip.Sum);
            }
        }

        public IGame CreateGame(List<User> users)
        {
            var newUsers = new Collection<User>(users);
            return new GameClient(newUsers, GameType.NoLimit, 5, 3000, 4, 10, true, new Chip(1000));
        }

        public GameProperties GetProperties()
        {
            Tuple<Hand, User>[] usersHands = new Tuple<Hand, User>[_players.Count];
            for (var i = 0; i < _players.Count; i++)
            {
                usersHands[i] = new Tuple<Hand, User>(_players[i].Hand, _players[i].User);
            }
            return new GameProperties(_dealer._gameType, _buyInPolicy, _chipPolicy, 2 * _entranceFee,
                _dealer._minimumUsersForPlaying, _maxPlayers, _allowedSpectator);
        }

        public int GetMainPotSize()
        {
            return _dealer.GetSumPots();
        }

        public void AddSpectator(User spectator)
        {
            _spectators.Add(spectator);
        }

        public List<User> GetUsersInRound(int numOfTurn)
        {
            return extractUser();
        }

        private List<User> extractUser()
        {
            List<User> players = new List<User>();
            foreach (var player in _players)
            {
                players.Add(player.User);
            }
            return players;
        }

        public void NotifyAllUsers(PlayerNotification notifications)
        {
            foreach (IObserver<PlayerNotification> player in _observers)
            {
               player.OnNext(notifications);
            }
        }

        public IDisposable Subscribe(IObserver<PlayerNotification> observer)
        {
            _observers.Add(observer);
            return new Unsubscriber(observer, _observers);
        }

        public void UpdatePlayersFromServer(PlayerNotification data)
        {
            NotifyAllUsers(data);
        }


        private class Unsubscriber : IDisposable
        {
            private List<IObserver<PlayerNotification>> _observers;
            private IObserver<PlayerNotification> _observer;

            public Unsubscriber(IObserver<PlayerNotification> observer, List<IObserver<PlayerNotification>> observers)
            {
                _observer = observer;
                _observers = observers;

            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                {
                    _observers.Remove(_observer);
                }
            }


        }
    }
}

