using System.Collections.Generic;

namespace AcceptanceTests.Data
{
   public class GameClient
   {
        private Dictionary<string , LinkedList<Turn>> _gameHistory;
        public const int Active = 0;
        public const int Inactive = 1;

        public int GameStatus { get; set; }
        public int ByInPolicy { get; set; }
        public State Type { get; set;}
        public int AmountOfPlayers { get; set;}
        public Dictionary<string, User> Players { get; }

       public GameClient(int byInPolicy,int amountOfPlayers)
        {
            ByInPolicy = byInPolicy;
            AmountOfPlayers = amountOfPlayers;
            Players = new Dictionary<string, User>(AmountOfPlayers);
            GameStatus = Active;
            _gameHistory = new Dictionary<string, LinkedList<Turn>>();

        }

       public Dictionary<string, LinkedList<Turn>> GameHistory
       {
           get
           {
               if (_gameHistory != null && GameStatus == Inactive)
               {
                   return _gameHistory;
               }
               else return null;
           }
           set {
               _gameHistory = value;
           }
       }


   }
}
