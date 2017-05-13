
using System.Net.Mail;


 namespace AcceptanceTests.Data
{

    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public MailAddress Email { get; set;}
        public string LinkToAvater { get; set; }
        public LeagueType LeagueRank { get; set; }


        public User(string userName , string password , string email)
        {
            UserName = userName;
            Password = password;
            Email = new MailAddress(email,userName);
        }
    }
}
