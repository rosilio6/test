
using System.Collections.ObjectModel;
using UserManagement;

namespace GameLogic
{
    public class Player
    {
        internal Hand Hand { get; set; }
        public Collection<Card> Cards;
        public Chip Chip;
        public int PlayerPot;
        public bool AmIInAllIn = false;
        public User User;
        public int Id;

        public Player()
        {
            PlayerPot = 0;
            Id = 0;
        }

        public Player(User user)
        {
            User = user;
            PlayerPot = 0;
            Id = user.GetId();
            Chip=new Chip(user.GetMoney());

        }

      

        internal void SetCards(Collection<Card> cards)
        {
            Cards = cards;
        }

        public void SetHand(Collection<Card> flop)
        {
            Hand = new Hand(Cards,flop);
        }

        public void ReduceChips(int value)
        {
            Chip.Sum -= value;
            PlayerPot += value;
        }
        public void AddChips(int value)
        {
            Chip.Sum += value;
        }

        public bool TakeAction(Dealer dealer,Turn.ActionType operation , int chip)
        {
          return dealer.SetAction(this, operation, chip);
        }
    }
}
