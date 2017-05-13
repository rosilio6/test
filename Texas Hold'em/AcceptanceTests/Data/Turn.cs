

using System.Collections.Generic;

namespace AcceptanceTests.Data
{
   public class Turn
    {

        public Hand Hand { get; set; }

        public Flop Flop { get; set; }

        public int Pot { get; set; }

        public int PlayerMoney { get; set; }


        public Turn( Hand hand, Flop flop, int pot , int playerMoney)
        {
            Hand = hand;
            Flop = flop;
            Pot = pot;
            PlayerMoney = playerMoney;

        }

    }
}
