
using System;
using System.Collections.Generic;
using System.Net.Mail;
using AcceptanceTests.Bridge;
using AcceptanceTests.Data;
using NUnit.Framework;

namespace AcceptanceTests.Tests
{
    [TestFixture]
    public class TestCases
    {
        private IBridge _bridge;
        private LinkedList<User> _user;
        private GameClient _game;
        

        [SetUp]
        public void Initialize()
        {
            _bridge = Driver.GetBridge();
             SetUpUsers();
        }

        protected void SetUpUsers()
        {
            _user = new LinkedList<User>();
            User user1 = new User("User1", "sharpHasC",  "user1@gmail.com");
            User user2 = new User("User2", "spook&stuf", "user2@gmail.com");
            User user3 = new User("User3", "passMyWord", "user3@gmail.com");
            User user4 = new User("User4", "paperCut",   "user4@gmail.com");

            _user.AddFirst(user1);
            _user.AddLast(user2);
            _user.AddLast(user3);
            _user.AddLast(user4);

            foreach (var u in _user)
            {
                _bridge.Register(u.UserName, u.Password , u.Email);
            }
        }

        protected void KillUsers()
        {
            foreach (var u in _user)
            {
                TestLogout(u);
                _user.Remove(u);
            }
            _user = null;
        }

        protected void SetUpGame(int byIn ,int numOfUsers)
        {
            _game = new GameClient(byIn , numOfUsers);
            int i = 1;

            foreach (var u in _user)
            {
                if (i <= numOfUsers)
                {
                    _game.Players.Add(u.UserName, u);
                    i++;
                }
                else
                {
                    break;
                }
            }

        }

        protected void KillGame()
        {
            _game = null;
        }

        protected void CreateReplay()
        {
            KillGame();
            SetUpGame(100,2);
            SetUpInactiveGame();

        }

        protected bool TestSaveTurnFromInactiveGame(string userName, GameClient game, int turnNum)
        {
            return _bridge.SaveTurnFromInactiveGame(userName, game, turnNum);

        }

        protected User TestfindUserByUserName(string userName)
        {
            return _bridge.TestfindUserByUserName(userName);

        }

        protected int TestLogin(string userName, string password)
        {
            return _bridge.Login(userName, password);
        }

        protected int TestRegister(string userName, string password, MailAddress email)
        {
            return _bridge.Register(userName, password , email);
        }


        protected bool TestLogout(User user)
        {
            return _bridge.Logout(user);
        }

        protected int TestFilterActiveGamesByUserName(string username)
        {
            return _bridge.FilterActiveGamesByUserName(username);
        }

        protected User TestEditUserDetails(User olduser , User newuser)
        {
            return _bridge.EditUserDetails(olduser,newuser);
        }

        protected bool TestCreateNewGame(int byInPolicy, int numOfPlayers)
        {
            return _bridge.CreateNewGame(byInPolicy,numOfPlayers);
        }

        protected bool TestCreateNewGameFromInstance(GameClient game)
        {
           return _bridge.CreateNewGameFromInstance(game);
        }

        protected bool TestJoinToGame(GameClient game, User user, State state)
        {
            return _bridge.JoinToGame(game,user,state);
        }

        protected bool TestLeaveActiveGame(GameClient game, User user)

        {
            return _bridge.LeaveActiveGame(game,user);
        }

        protected bool TestReplayInactiveGame(GameClient game)
        {
            return _bridge.ReplayInactiveGam(game);
        }

        protected int TestFindAllActiveGamesForState(State state)
        {
            return _bridge.FindAllActiveGames(state);

        }

        protected bool TestJoinUserToFirstActiveGame(User user, State state)
        {   
            return _bridge.JoinUserToFirstActiveGame(user, state);
        }


        protected bool TestCreateNewGameOnlyForPlayers(int byInPolicy, int numOfPlayers)
        {
            return _bridge.CreateNewGameOnlyForPlayers(byInPolicy, numOfPlayers);
        }

        private void SetUpInactiveGame()
        {
            Game.GameStatus = 1;

            //Creating turns for User1
            Flop f1 = new Flop();
            Flop f2 = new Flop();
            Flop f3 = new Flop();

            // User1 Hand
            LinkedList<Card> handPair = new LinkedList<Card>();
            handPair.AddLast(new Card(Value.Eight, Suit.Club));
            handPair.AddLast(new Card(Value.Ace, Suit.Spade));

            // User2 Hand
            LinkedList<Card> handPairTwo = new LinkedList<Card>();
            handPair.AddLast(new Card(Value.Two, Suit.Club));
            handPair.AddLast(new Card(Value.Seven, Suit.Spade));

            initFlop(f1, f2, f3);

            InitUser(f1, f2, f3, handPair, 1212, "User1");
            InitUser(f1, f2, f3, handPairTwo, 2323, "User2");

        }

        private void initFlop(Flop f1, Flop f2, Flop f3)
        {


            f1.Cards[0] = new Card(Value.Ace, Suit.Club);
            f1.Cards[1] = new Card(Value.Ace, Suit.Diamond);
            f1.Cards[2] = new Card(Value.Ace, Suit.Heart);
            f1.Cards[3] = null;
            f1.Cards[4] = null;


            f2.Cards[0] = new Card(Value.Ace, Suit.Club);
            f2.Cards[1] = new Card(Value.Ace, Suit.Diamond);
            f2.Cards[2] = new Card(Value.Ace, Suit.Heart);
            f2.Cards[3] = new Card(Value.King, Suit.Heart);
            f2.Cards[4] = null;

            f3.Cards[0] = new Card(Value.Ace, Suit.Club);
            f3.Cards[1] = new Card(Value.Ace, Suit.Diamond);
            f3.Cards[2] = new Card(Value.Ace, Suit.Heart);
            f3.Cards[3] = new Card(Value.King, Suit.Heart);
            f3.Cards[4] = new Card(Value.Two, Suit.Heart);
        }

        private void InitUser(Flop f1, Flop f2, Flop f3, LinkedList<Card> handPair, int userMoney, String userName)
        {

            // Pack the Hand
            Hand h1 = new Hand(handPair);

            Turn turnOne = new Turn(h1, f1, 425, userMoney);
            Turn turnTwo = new Turn(h1, f2, 425, userMoney);
            Turn turnThree = new Turn(h1, f3, 425, userMoney);

            LinkedList<Turn> userTurns = new LinkedList<Turn>();
            userTurns.AddLast(turnOne);
            userTurns.AddLast(turnTwo);
            userTurns.AddLast(turnThree);

            //add to user his turns
            Game.GameHistory.Add(userName, userTurns);
        }

        public IBridge Bridge
        {
            get { return _bridge; }
            set { _bridge = value; }
        }

        public LinkedList<User> Users
        {
            get { return _user; }
            set { _user = value; }
        }

        public GameClient Game
        {
            get { return _game; }
            set { _game = value; }
        }

    }
}
