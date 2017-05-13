using System.Collections.ObjectModel;

namespace GameLogic
{
    public interface IDeck
    {
        /* Picks the First card on the Deck 
         * return null if the deck is Empty
        */
        Card PickACard();
        /* Picks the First 1 card on the Deck 
         * return null if the deck is Empty
         * otherwise returns a HashSet of the cards
        */
        Collection<Card> Pick1Card();
        /* Picks the First 2 card on the Deck 
         * return null if the deck is Empty
         * otherwise returns a HashSet of the cards
        */
        Collection<Card> Pick2Card();
        /* Picks the First 3 card on the Deck 
         * return null if the deck is Empty
         * otherwise returns a HashSet of the cards
        */
        Collection<Card> Pick3Card();
        // "Collects" all the cards,or in other words the deck is full again, and is in randomized order
        void Shuffle();

    }
}