using System.Collections.ObjectModel;
using System.Linq;
using GameLogic;
using NUnit.Framework;

namespace Testing.GameTesting
{
    [TestFixture]
    class DeckTest
    {
        [TestCase]
        public void PickACardTest2()
        {
            Deck d = new Deck();
            for (int j = 0; j < 52; j++)
            {
                Assert.IsTrue(d.PickACard().GetType() == typeof(Card));
            }
            
        }
        [TestCase]
        public void PickNumCardTest()
        {
            IDeck d = new Deck();
            Collection<Card> set1 = d.Pick1Card();
            Collection<Card> set2 = d.Pick2Card();
            Collection<Card> set3 = d.Pick3Card();
            Assert.IsTrue(set1.GetType() == typeof(Collection<Card>));
            Assert.AreEqual(set1.Count(),1);
            Assert.IsTrue(set1.ElementAt(0).GetType() == typeof(Card));
            Assert.IsTrue(set2.GetType() == typeof(Collection<Card>));
            Assert.AreEqual(set2.Count(), 2);
            Assert.IsTrue(set2.ElementAt(0).GetType() == typeof(Card));
            Assert.IsTrue(set2.ElementAt(1).GetType() == typeof(Card));
            Assert.IsTrue(set3.GetType() == typeof(Collection<Card>));
            Assert.AreEqual(set3.Count(), 3);
            Assert.IsTrue(set3.ElementAt(0).GetType() == typeof(Card));
            Assert.IsTrue(set3.ElementAt(1).GetType() == typeof(Card));
            Assert.IsTrue(set3.ElementAt(2).GetType() == typeof(Card));

        }
        [TestCase]
        public void PickACardTest1()
        {
            Deck d = new Deck();
            Card[] cards = new Card[52];
            int counter = 0;
            for (int i = 0; i < 52; i++)
            {
                cards[counter] = d.PickACard();
                Assert.AreNotEqual(cards[counter], null);
                counter++;
            }
            Assert.AreEqual(d.PickACard(), null);
        }
        //might come as red flag sometimes, but most of the time should return green
        [TestCase]
        public void ShuffleTest1()
        {
            Deck d = new Deck();
            BurnCardsFromDeck(d,0);
            Card c1 = d.PickACard();
            d.Shuffle();
            BurnCardsFromDeck(d,0);
            Card c2 = d.PickACard();
            Assert.AreNotEqual(c1,c2);
        }
        [TestCase]
        public void ShuffleTest2()
        {
            Deck d = new Deck();
            BurnCardsFromDeck(d,5);
            Card c1 = d.PickACard();
            d.Shuffle();
            BurnCardsFromDeck(d,5);
            Card c2 = d.PickACard();
            Assert.AreNotEqual(c1, c2);
        }
        [TestCase]
        public void ShuffleTest3()
        {
            Deck d = new Deck();
            BurnCardsFromDeck(d,25);
            Card c1 = d.PickACard();
            d.Shuffle();
            BurnCardsFromDeck(d,25);
            Card c2 = d.PickACard();
            Assert.AreNotEqual(c1, c2);
        }
        [TestCase]
        public void ShuffleTest4()
        {
            Deck d = new Deck();
            BurnCardsFromDeck(d,51);
            Card c1 = d.PickACard();
            d.Shuffle();
            BurnCardsFromDeck(d,51);
            Card c2 = d.PickACard();
            Assert.AreNotEqual(c1, c2);
        }
        private void BurnCardsFromDeck(Deck d,int howManyCards)
        {
            for (int i = 0; i < howManyCards; i++)
            {
                d.PickACard();
            }
        }
       

    }
}
