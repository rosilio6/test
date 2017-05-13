using System.Collections.Generic;

namespace AcceptanceTests.Data
{
    public class Hand
    {

        public LinkedList<Card> Cards { get; set; }
   
        public Hand(LinkedList<Card> cards)
        {
            Cards = cards;
        }
    }
}
