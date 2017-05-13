using System.Collections.ObjectModel;
using Communication;
using GameLogic;
using GameLogic.Game_Client;
using UserManagement;

namespace NotificationSimulator
{
    class Program
    {
        static void Main(string[] args)
        {


            User roni = new User("Roni", "1234567", 0);
            User maayan = new User("Maayan", "1234567", 1);
            User fibi = new User("Fibi", "1234567", 2);
            User jack = new User("Jack", "1234567", 3);

            //make all users players 
            roni.ChangePlayerState(PlayerState.Player);
            maayan.ChangePlayerState(PlayerState.Player);
            fibi.ChangePlayerState(PlayerState.Player);
            jack.ChangePlayerState(PlayerState.Player);


            Collection<User> C1 = new Collection<User>();
            Collection<User> C2 = new Collection<User>();

            C1.Add(roni);
            C1.Add(maayan);
            C2.Add(fibi);
            C2.Add(jack);

            GameClient game1 = new GameClient(C1, GameType.Limit, 0, 10, 2, 10, true, new Chip(30));
            GameClient game2 = new GameClient(C2, GameType.Limit, 2, 10, 2, 4, true, new Chip(10));


            //make game active 
            GameServer.GameCenter.Instance.ActivateGame(game1);
            GameServer.GameCenter.Instance.ActivateGame(game2);


            //register players to games notification service 
            game1.Subscribe(roni);
            game1.Subscribe(maayan);
            game2.Subscribe(fibi);
            game2.Subscribe(jack);

            //gets data from server -->this function should run when server creates 
            NotificationBuilder serverNotifications = new NotificationBuilder();
            serverNotifications.Notify();

            /*

            ////create data TODO:replace the data loading from manual to server////
            PlayerNotification data1 = new PlayerNotification();
            PlayerNotification data2 = new PlayerNotification();

            data1.AddNotification("Hello");
            data1.AddNotification("Maayan Fold");
            data2.AddNotification("Hello to game number 2 !!!!!");
            data2.AddNotification("jack rasid by 10 chips are you ready to tack action?");

            
            game1.UpdatePlayersFromServer(data1);
            game2.UpdatePlayersFromServer(data2);
            
            //delete notification after update
            data1.ClearNotifaictions();
            data2.ClearNotifaictions();


            //add roni to game 2 
            IDisposable roniChannal = game2.Subscribe(roni);

            data1.AddNotification("Hello from game 1");
            data2.AddNotification("Hello from game 2");

            game1.UpdatePlayersFromServer(data1);
            game2.UpdatePlayersFromServer(data2);

            //unsabscribe rony from game 2 updates
            roniChannal.Dispose();


            //sopust to notify fibi and jack witout ronny 
            game2.UpdatePlayersFromServer(data2);
       */
            

            while (true) ;
        }
    }
}
