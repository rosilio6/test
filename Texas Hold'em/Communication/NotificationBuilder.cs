using System.Collections.Generic;
using System.Timers;
using GameLogic;
using GameLogic.Game_Client;
using UserManagement;
using Timer = System.Timers.Timer;

namespace Communication
{
    public class NotificationBuilder : INotification
    {

        private const int UpdateRate = 5000;
        private List<IGame> _allGames;
        private PlayerNotification _notifications;

        public NotificationBuilder()
        {
            _notifications = new PlayerNotification();
        }

        public void Notify()
        {
            
            Timer timer = new Timer(UpdateRate);
            timer.Elapsed += TimerOnElapsed;
            timer.Start();

        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            _allGames = GameServer.GameCenter.Instance.GetAllActiveGames();
            if (_notifications != null)
            {
                foreach (IGame game in _allGames)
                {
                    _notifications = BuildNotificationsFromGamePropoties(game);
                    ((GameClient)game).UpdatePlayersFromServer(_notifications);
                    
                }
            }
        }

        //exmple to notifacations from game propoties
        private PlayerNotification BuildNotificationsFromGamePropoties(IGame game)
        {
            
            PlayerNotification data = new PlayerNotification();

           
            data.AddNotification("Hello From Game " + game.GetProperties().GameIdentifier);
            data.AddNotification("The Game chip policy is : " + game.GetProperties().ChipPolicy.Sum);
            data.AddNotification("the pot size is " + game.GetMainPotSize());
            data.AddNotification("The Current by in policy " + game.GetProperties().BuyInPolicy);

            return data;

        }
    }
}
