using System.Collections.Generic;

namespace AcceptanceTests.Data
{
    class League
    {

        private  LeagueType _type;
        private  List<User> _usersInLeague;

        public League(LeagueType type)
        {
            _type = type;
            _usersInLeague = new List<User>();
        }

    }
}
