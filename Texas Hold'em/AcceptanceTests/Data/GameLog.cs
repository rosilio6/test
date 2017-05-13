
using System.Collections.Generic;

namespace AcceptanceTests.Data
{
   
   public class GameLog
   {
        private static GameLog _logger; 
        private static Dictionary<string, GameClient> _gameLogs;
         
        private GameLog(){}

        public static GameLog GetGameLog()
        {
            if (_gameLogs == null)
            {
                _logger = new GameLog();
                _gameLogs = new Dictionary<string, GameClient>();
            }
            return _logger;
            }


        public void Log(string id,GameClient game)
        {
            _gameLogs.Add(id,game);
        }

        public Dictionary<string, GameClient> GameLogs
        {
            get { return _gameLogs;}
            set { _gameLogs = value; }
        }
    }
}
