using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UserManagement
{
    public class User : IObserver<PlayerNotification>
    {
        private readonly int _id;
        private int _money;
        private PlayerState _playerState;
        public string PlayerName { get; private set; }

        private string _password;
        private string _email;
        private string _pathToImage;
        private int _rank;


        public int GetId()
        {
            return _id;
        }

        public int GetMoney()
        {
            return _money;
        }

        public void SetMoney(int value)
        {
            _money = value;
        }


        public string Get_pathToImage()
        {
            return _pathToImage;
        }

        public bool Set_pathToImage(string value)
        {
            _pathToImage = value;
            return true;
        }

        public string Get_email()
        {
            return _email;
        }

        public bool Set_email(string value)
        {
            _email = value;
            return true;
        }


        public User(string name, string password, int id)
        {
            _id = id;
            _rank = 0;
            _playerState = PlayerState.InLobby;
            PlayerName = name;
            _password = password;
        }

        public int GetRank()
        {
            return _rank;
        }

        public void SetRank(int rank)
        {
            _rank = rank;
        }

        public PlayerState GetPlayerState()
        {
            return _playerState;
        }

        public bool CheckPassword(string pass)
        {
            if (this._password == pass)
                return true;
            return false;
        }

        public bool ChangePassword(string oldPass, string newPass)
        {
            if (!CheckPassword(oldPass)) return false;
            _password = newPass;
            return true;
        }

        public void ChangePlayerState(PlayerState playerState)
        {
            _playerState = playerState;
        }

        public void OnNext(PlayerNotification notifications)
        {
            //TODO: here we need to connect recived data to the GUI 
            foreach (string msg in notifications.Notifications())
            {
                Console.WriteLine("User : " +PlayerName +" got messege : " + msg + ".");
            }

        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            //TODO: here we need to refresh the GUI or what we want when the notification proccess has been done 
        }
    }
}