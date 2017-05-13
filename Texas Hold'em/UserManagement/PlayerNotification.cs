
using System.Collections.Generic;
using System.Security.Cryptography;

namespace UserManagement
{
   
    public class PlayerNotification
    {
        private readonly LinkedList<string> _notifications;

        public PlayerNotification()
        {
            _notifications = _notifications = new LinkedList<string>();
        }


        public void AddNotification(string notification)
        {
            _notifications.AddLast(notification);

        }

        public void ClearNotifaictions()
        {
            int size = _notifications.Count;

            while (size != 0)
            {
                _notifications.RemoveFirst();
                size--;
            }

        }

        public int NotificationsAmout()
        {
            return  _notifications.Count;
        }

        public string GetNotification()
        {
            string notification = null; 
            if (NotificationsAmout() != 0)
            {
                notification = _notifications.First.Value;
                _notifications.Remove(notification);
            }

            return  notification;
        }


        public LinkedList<string> Notifications()
        {
            if (NotificationsAmout() != 0)
            {
                return _notifications;
            }
             return null;
        }


    }

 
   
}
