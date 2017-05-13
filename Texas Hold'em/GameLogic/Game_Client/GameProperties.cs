using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement;

namespace GameLogic
{
    /// <summary>
    /// The properties that represent the game and are created when the game is created.
    /// These properties are used in the calculation of the user rank and are updated throught the game.
    /// </summary>
    public class GameProperties
    {
        public Guid GameIdentifier { get; }
        public bool IsClosed;
        public DateTime StartTime;
        public DateTime EndTime;
        public int NumberOfPlayersAtStart { get; set; }
        public GameType Policy;
        public int BuyInPolicy;
        public Chip ChipPolicy;
        public int MinBet;
        public int MinPlayers;
        public int MaxPlayers;
        public bool IsGameAllowSpectator;

        public List<Round> RoundInGame { get; private set; }

        public SortedDictionary<PokerHand, User> HighestHand { get; set; }
        public SortedDictionary<int, User> HighestBet { get; set; }

        private List<User> _activePlayers;
        private List<User> _spectatorUser;

        public GameProperties(GameType policy, int buyInPolicy, Chip chipPolicy, int minBet, int minPlayers, int maxPlayers, bool isGameAllowSpectator)
        {
            Policy = policy;
            BuyInPolicy = buyInPolicy;
            ChipPolicy = chipPolicy;
            MinBet = minBet;
            MinPlayers = minPlayers;
            MaxPlayers = maxPlayers;
            IsGameAllowSpectator = isGameAllowSpectator;
            GameIdentifier = new Guid();
            IsClosed = false;
            _activePlayers = new List<User>();
            _spectatorUser = new List<User>();
            RoundInGame = new List<Round>();
        }

        public void AddSpectator(User spectator)
        {
            if (spectator.GetPlayerState() == PlayerState.Spectator)
            {
                _spectatorUser.Add(spectator);
            }
        }

        public void AddPlayer(User player)

        {
            if (player.GetPlayerState() == PlayerState.Player)
            {
                _activePlayers.Add(player);
            }
        }

        public bool ContainsPlayerName(string name)
        {
            return _activePlayers.Any(activePlayer => activePlayer.PlayerName.Equals(name));
        }

    }
}
