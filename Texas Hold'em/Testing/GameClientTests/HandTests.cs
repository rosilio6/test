using System.Collections.ObjectModel;
using NUnit.Framework;

namespace GameLogic.NUnitTests
{
    [TestFixture]
    class HandTests
    {
        
        [TestCase]
        public void FullHouseTests1()
        {
            Card c1 = new Card(((Symbol)3), 0);
            Card c2 = new Card(((Symbol)3), ((Suit)1));
            Card c3 = new Card(((Symbol)3), ((Suit)2));
            Card c4 = new Card(((Symbol)7), ((Suit)3));
            Card c5 = new Card(((Symbol)14), ((Suit)3));
            Card c6 = new Card(((Symbol)10), ((Suit)1));
            Card c7 = new Card(((Symbol)10), ((Suit)3));
            Collection<Card> hand = new Collection<Card>{c6,c7};
            Collection<Card> flop = new Collection<Card> { c1, c2, c3, c4, c5 };
            Hand myHand = new Hand(hand,flop);
            Assert.AreEqual(PokerHand.FullHouse, myHand.HandTitle);
            Assert.AreEqual(Symbol.Three, myHand.PrimaryValue);
            Assert.AreEqual(new[] { Symbol.Ten }, myHand.KickersValues);
        }
        [TestCase]
        public void FullHouseTests2()
        {
            Card c1 = new Card(((Symbol)3), ((Suit)1));
            Card c2 = new Card(((Symbol)3), ((Suit)1));
            Card c3 = new Card(((Symbol)3), ((Suit)2));
            Card c4 = new Card(((Symbol)10), ((Suit)3));
            Card c5 = new Card(((Symbol)14), ((Suit)3));
            Card c6 = new Card(((Symbol)10), ((Suit)1));
            Card c7 = new Card(((Symbol)10), ((Suit)3));
            Collection<Card> hand = new Collection<Card> { c6, c7 };
            Collection<Card> flop = new Collection<Card> { c1, c2, c3, c4, c5 };
            Hand myHand = new Hand(hand, flop);
            Assert.AreEqual(PokerHand.FullHouse, myHand.HandTitle);
            Assert.AreEqual(Symbol.Ten, myHand.PrimaryValue);
            Assert.AreEqual(new[] { Symbol.Three }, myHand.KickersValues);
        }
        [TestCase]
        public void TwoPairTests1()
        {
            
            Card c1 = new Card(((Symbol)3), 0);
            Card c2 = new Card(((Symbol)3), ((Suit)1));
            Card c3 = new Card(((Symbol)4), ((Suit)2));
            Card c4 = new Card(((Symbol)4), ((Suit)3));
            Card c5 = new Card(((Symbol)14), ((Suit)3));
            Card c6 = new Card(((Symbol)10), ((Suit)1));
            Card c7 = new Card(((Symbol)10), ((Suit)3));
            Collection<Card> hand = new Collection<Card> { c6, c7 };
            Collection<Card> flop = new Collection<Card> { c1, c2, c3, c4, c5 };
            Hand myHand = new Hand(hand, flop);
            Assert.AreEqual(PokerHand.TwoPair, myHand.HandTitle);
            Assert.AreEqual(Symbol.Ten, myHand.PrimaryValue);
            Assert.AreEqual(new[] { Symbol.Four,Symbol.Ace}, myHand.KickersValues);
        }
        [TestCase]
        public void TwoPairTests2()
        {
            Card c1 = new Card(((Symbol)3), 0);
            Card c2 = new Card(((Symbol)3), ((Suit)1));
            Card c3 = new Card(((Symbol)4), ((Suit)2));
            Card c4 = new Card(((Symbol)5), ((Suit)3));
            Card c5 = new Card(((Symbol)14), ((Suit)3));
            Card c6 = new Card(((Symbol)10), ((Suit)1));
            Card c7 = new Card(((Symbol)10), ((Suit)3));
            Collection<Card> hand = new Collection<Card> { c6, c7 };
            Collection<Card> flop = new Collection<Card> { c1, c2, c3, c4, c5 };
            Hand myHand = new Hand(hand, flop);
            Assert.AreEqual(PokerHand.TwoPair, myHand.HandTitle);
            Assert.AreEqual(Symbol.Ten, myHand.PrimaryValue);
            Assert.AreEqual(new[]{Symbol.Three,Symbol.Ace}, myHand.KickersValues);
        }
        [TestCase]
        public void OnePairTests()
        {
            Card c1 = new Card(((Symbol)3), 0);
            Card c2 = new Card(((Symbol)3), ((Suit)1));
            Card c3 = new Card(((Symbol)4), ((Suit)2));
            Card c4 = new Card(((Symbol)5), ((Suit)3));
            Card c5 = new Card(((Symbol)14), ((Suit)3));
            Card c6 = new Card(((Symbol)9), ((Suit)1));
            Card c7 = new Card(((Symbol)10), ((Suit)3));
            Collection<Card> hand = new Collection<Card> { c6, c7 };
            Collection<Card> flop = new Collection<Card> { c1, c2, c3, c4, c5 };
            Hand myHand = new Hand(hand, flop);
            Assert.AreEqual(PokerHand.OnePair, myHand.HandTitle);
            Assert.AreEqual(Symbol.Three, myHand.PrimaryValue);
            Assert.AreEqual(new[] { Symbol.Ace, Symbol.Ten, Symbol.Nine }, myHand.KickersValues);
        }
        [TestCase]
        public void ThreeOfAKindTests()
        {
            
            Card c1 = new Card(((Symbol)3), 0);
            Card c2 = new Card(((Symbol)3), ((Suit)1));
            Card c3 = new Card(((Symbol)3), ((Suit)2));
            Card c4 = new Card(((Symbol)7), ((Suit)3));
            Card c5 = new Card(((Symbol)14), ((Suit)3));
            Card c6 = new Card(((Symbol)9), ((Suit)1));
            Card c7 = new Card(((Symbol)10), ((Suit)3));
            Collection<Card> hand = new Collection<Card> { c6, c7 };
            Collection<Card> flop = new Collection<Card> { c1, c2, c3, c4, c5 };

            Hand myHand = new Hand(hand, flop);
            Assert.AreEqual(PokerHand.ThreeOfAKind, myHand.HandTitle);
            Assert.AreEqual(Symbol.Three, myHand.PrimaryValue);
            Assert.AreEqual(new[] { Symbol.Ace, Symbol.Ten }, myHand.KickersValues);
        }
        [TestCase]
        public void FourOfAKindTests()
        {
            
            Card c1 = new Card(((Symbol)3), 0);
            Card c2 = new Card(((Symbol)3), ((Suit)1));
            Card c3 = new Card(((Symbol)3), ((Suit)2));
            Card c4 = new Card(((Symbol)3), ((Suit)3));
            Card c5 = new Card(((Symbol)14), ((Suit)3));
            Card c6 = new Card(((Symbol)10), ((Suit)1));
            Card c7 = new Card(((Symbol)10), ((Suit)3));
            Collection<Card> hand = new Collection<Card> { c6, c7 };
            Collection<Card> flop = new Collection<Card> { c1, c2, c3, c4, c5 };
            Hand myHand = new Hand(hand, flop);
            Assert.AreEqual(PokerHand.FourOfAKind, myHand.HandTitle);
            Assert.AreEqual(Symbol.Three, myHand.PrimaryValue);
            Assert.AreEqual(new[] { Symbol.Ace }, myHand.KickersValues);
        }


        [TestCase]
        public void RoyalFlushTests()
        {

            Card c1 = new Card(((Symbol)4), ((Suit)2));
            Card c2 = new Card(((Symbol)5), ((Suit)3));
            Card c3 = new Card(((Symbol)10), ((Suit)1));
            Card c4 = new Card(((Symbol)11), ((Suit)1));
            Card c5 = new Card(((Symbol)12), ((Suit)1));
            Card c6 = new Card(((Symbol)13), ((Suit)1));
            Card c7 = new Card(((Symbol)14), ((Suit)1));
            Collection<Card> hand = new Collection<Card> { c6, c7 };
            Collection<Card> flop = new Collection<Card> { c1, c2, c3, c4, c5 };

            Hand myHand = new Hand(hand, flop);
            Assert.AreEqual(PokerHand.RoyalFlush, myHand.HandTitle);
            Assert.AreEqual(Symbol.Ace, myHand.PrimaryValue);
            Assert.AreEqual(new[] { Symbol.King, Symbol.Queen, Symbol.Jack, Symbol.Ten }, myHand.KickersValues);
        }

        [TestCase]
        public void FlushTests1()
        {
            Card c1 = new Card(((Symbol)4), ((Suit)1));
            Card c2 = new Card(((Symbol)5), ((Suit)1));
            Card c3 = new Card(((Symbol)3), ((Suit)1));
            Card c4 = new Card(((Symbol)6), ((Suit)1));
            Card c5 = new Card(((Symbol)10), ((Suit)1));
            Card c6 = new Card(((Symbol)8), ((Suit)1));
            Card c7 = new Card(((Symbol)9), ((Suit)1));
            Collection<Card> hand = new Collection<Card> { c6, c7 };
            Collection<Card> flop = new Collection<Card> { c1, c2, c3, c4, c5 };

            Hand myHand = new Hand(hand, flop);
            Assert.AreEqual(PokerHand.Flush, myHand.HandTitle);
            Assert.AreEqual(Symbol.Ten, myHand.PrimaryValue);
            Assert.AreEqual(new[] { Symbol.Nine, Symbol.Eight, Symbol.Six, Symbol.Five }, myHand.KickersValues);
        }
        [TestCase]
        public void FlushTests2()
        {

            Card c1 = new Card(((Symbol)4), ((Suit)1));
            Card c2 = new Card(((Symbol)5), ((Suit)1));
            Card c3 = new Card(((Symbol)3), ((Suit)1));
            Card c4 = new Card(((Symbol)6), ((Suit)1));
            Card c5 = new Card(((Symbol)7), ((Suit)2));
            Card c6 = new Card(((Symbol)8), ((Suit)1));
            Card c7 = new Card(((Symbol)9), ((Suit)1));
            Collection<Card> hand = new Collection<Card> { c6, c7 };
            Collection<Card> flop = new Collection<Card> { c1, c2, c3, c4, c5 };

            Hand myHand = new Hand(hand, flop);
            Assert.AreEqual(PokerHand.Flush, myHand.HandTitle);
            Assert.AreEqual(Symbol.Nine, myHand.PrimaryValue);
            Assert.AreEqual(new[] { Symbol.Eight, Symbol.Six, Symbol.Five, Symbol.Four }, myHand.KickersValues);
        }
        [TestCase]
        public void FlushTests3()
        {

            Card c1 = new Card(((Symbol)4), ((Suit)1));
            Card c2 = new Card(((Symbol)5), ((Suit)1));
            Card c3 = new Card(((Symbol)3), ((Suit)2));
            Card c4 = new Card(((Symbol)6), ((Suit)2));
            Card c5 = new Card(((Symbol)7), ((Suit)1));
            Card c6 = new Card(((Symbol)8), ((Suit)1));
            Card c7 = new Card(((Symbol)9), ((Suit)1));
            Collection<Card> hand = new Collection<Card> { c6, c7 };
            Collection<Card> flop = new Collection<Card> { c1, c2, c3, c4, c5 };

            Hand myHand = new Hand(hand, flop);
            Assert.AreEqual(PokerHand.Flush, myHand.HandTitle);
            Assert.AreEqual(Symbol.Nine, myHand.PrimaryValue);
            Assert.AreEqual(new[] { Symbol.Eight, Symbol.Seven, Symbol.Five, Symbol.Four }, myHand.KickersValues);
        }
        
        [TestCase]
        public void StraightFlushTests1()
        {

            Card c1 = new Card(((Symbol)4), ((Suit)2));
            Card c2 = new Card(((Symbol)5), ((Suit)1));
            Card c3 = new Card(((Symbol)3), ((Suit)3));
            Card c4 = new Card(((Symbol)6), ((Suit)1));
            Card c5 = new Card(((Symbol)7), ((Suit)1));
            Card c6 = new Card(((Symbol)8), ((Suit)1));
            Card c7 = new Card(((Symbol)9), ((Suit)1));
            Collection<Card> hand = new Collection<Card> { c6, c7 };
            Collection<Card> flop = new Collection<Card> { c1, c2, c3, c4, c5 };

            Hand myHand = new Hand(hand, flop);
            Assert.AreEqual(PokerHand.StraightFlush, myHand.HandTitle);
            Assert.AreEqual(Symbol.Nine, myHand.PrimaryValue);
            Assert.AreEqual(new[] { Symbol.Eight,Symbol.Seven, Symbol.Six, Symbol.Five}, myHand.KickersValues);
        }
        [TestCase]
        public void StraightFlushTests2()
        {

            Card c1 = new Card(((Symbol)4), ((Suit)1));
            Card c2 = new Card(((Symbol)5), ((Suit)1));
            Card c3 = new Card(((Symbol)3), ((Suit)3));
            Card c4 = new Card(((Symbol)6), ((Suit)1));
            Card c5 = new Card(((Symbol)7), ((Suit)1));
            Card c6 = new Card(((Symbol)8), ((Suit)1));
            Card c7 = new Card(((Symbol)9), ((Suit)2));
            Collection<Card> hand = new Collection<Card> { c6, c7 };
            Collection<Card> flop = new Collection<Card> { c1, c2, c3, c4, c5 };

            Hand myHand = new Hand(hand, flop);
            Assert.AreEqual(PokerHand.StraightFlush, myHand.HandTitle);
            Assert.AreEqual(Symbol.Eight, myHand.PrimaryValue);
            Assert.AreEqual(new[] { Symbol.Seven, Symbol.Six, Symbol.Five, Symbol.Four}, myHand.KickersValues);
        }
        [TestCase]
        public void StraightFlushTests3()
        {

            Card c1 = new Card(((Symbol)14), ((Suit)1));
            Card c2 = new Card(((Symbol)2), ((Suit)1));
            Card c3 = new Card(((Symbol)3), ((Suit)1));
            Card c4 = new Card(((Symbol)4), ((Suit)1));
            Card c5 = new Card(((Symbol)5), ((Suit)1));
            Card c6 = new Card(((Symbol)8), ((Suit)2));
            Card c7 = new Card(((Symbol)9), ((Suit)3));
            Collection<Card> hand = new Collection<Card> { c6, c7 };
            Collection<Card> flop = new Collection<Card> { c1, c2, c3, c4, c5 };

            Hand myHand = new Hand(hand, flop);
            Assert.AreEqual(PokerHand.StraightFlush, myHand.HandTitle);
            Assert.AreEqual(Symbol.Five, myHand.PrimaryValue);
            Assert.AreEqual(new[] { Symbol.Four, Symbol.Three, Symbol.Two, Symbol.Ace }, myHand.KickersValues);
        }

        [TestCase]
        public void StraightTests1()
        {

            Card c1 = new Card(((Symbol)14), 0);
            Card c2 = new Card(((Symbol)2), ((Suit)1));
            Card c3 = new Card(((Symbol)3), ((Suit)2));
            Card c4 = new Card(((Symbol)4), ((Suit)3));
            Card c5 = new Card(((Symbol)5), ((Suit)3));
            Card c6 = new Card(((Symbol)10), ((Suit)1));
            Card c7 = new Card(((Symbol)10), ((Suit)3));
            Collection<Card> hand = new Collection<Card> { c6, c7 };
            Collection<Card> flop = new Collection<Card> { c1, c2, c3, c4, c5 };

            Hand myHand = new Hand(hand, flop);
            Assert.AreEqual(PokerHand.Straight, myHand.HandTitle);
            Assert.AreEqual(Symbol.Five,myHand.PrimaryValue);
            Assert.AreEqual(new[] { Symbol.Four, Symbol.Three, Symbol.Two, Symbol.Ace }, myHand.KickersValues);
        }
        [TestCase]
        public void StraightTests2()
        {

            Card c1 = new Card(((Symbol)8), 0);
            Card c2 = new Card(((Symbol)9), ((Suit)1));
            Card c3 = new Card(((Symbol)10), ((Suit)2));
            Card c4 = new Card(((Symbol)11), ((Suit)3));
            Card c5 = new Card(((Symbol)12), ((Suit)3));
            Card c6 = new Card(((Symbol)13), ((Suit)1));
            Card c7 = new Card(((Symbol)7), ((Suit)3));
            Collection<Card> hand = new Collection<Card> { c6, c7 };
            Collection<Card> flop = new Collection<Card> { c1, c2, c3, c4, c5 };

            Hand myHand = new Hand(hand, flop);
            Assert.AreEqual(PokerHand.Straight, myHand.HandTitle);
            Assert.AreEqual(Symbol.King, myHand.PrimaryValue);
            Assert.AreEqual(new[] { Symbol.Queen, Symbol.Jack, Symbol.Ten, Symbol.Nine }, myHand.KickersValues);
        }
        [TestCase]
        public void StraightTests3()
        {

            Card c1 = new Card(((Symbol)14), 0);
            Card c2 = new Card(((Symbol)2), ((Suit)1));
            Card c3 = new Card(((Symbol)3), ((Suit)2));
            Card c4 = new Card(((Symbol)4), ((Suit)3));
            Card c5 = new Card(((Symbol)5), ((Suit)3));
            Card c6 = new Card(((Symbol)6), ((Suit)1));
            Card c7 = new Card(((Symbol)10), ((Suit)3));
            Collection<Card> hand = new Collection<Card> { c6, c7 };
            Collection<Card> flop = new Collection<Card> { c1, c2, c3, c4, c5 };

            Hand myHand = new Hand(hand, flop);
            Assert.AreEqual(PokerHand.Straight, myHand.HandTitle);
            Assert.AreEqual(Symbol.Six, myHand.PrimaryValue);
            Assert.AreEqual(new[] {Symbol.Five,Symbol.Four, Symbol.Three, Symbol.Two}, myHand.KickersValues);
        }
        [TestCase]
        public void HighcardTests1()
        {

            Card c1 = new Card(((Symbol)14), 0);
            Card c2 = new Card(((Symbol)2), ((Suit)1));
            Card c3 = new Card(((Symbol)3), ((Suit)2));
            Card c4 = new Card(((Symbol)8), ((Suit)3));
            Card c5 = new Card(((Symbol)5), 0);
            Card c6 = new Card(((Symbol)6), ((Suit)1));
            Card c7 = new Card(((Symbol)10), ((Suit)2));
            Collection<Card> hand = new Collection<Card> { c6, c7 };
            Collection<Card> flop = new Collection<Card> { c1, c2, c3, c4, c5 };

            Hand myHand = new Hand(hand, flop);
            Assert.AreEqual(PokerHand.HighCard, myHand.HandTitle);
            Assert.AreEqual(Symbol.Ace, myHand.PrimaryValue);
            Assert.AreEqual(new[] { Symbol.Ten, Symbol.Eight, Symbol.Six, Symbol.Five }, myHand.KickersValues);
        }
        [TestCase]
        public void HighcardTests2()
        {

            Card c1 = new Card(((Symbol)9), 0);
            Card c2 = new Card(((Symbol)2), ((Suit)1));
            Card c3 = new Card(((Symbol)3), ((Suit)2));
            Card c4 = new Card(((Symbol)8), ((Suit)3));
            Card c5 = new Card(((Symbol)5), 0);
            Card c6 = new Card(((Symbol)6), ((Suit)1));
            Card c7 = new Card(((Symbol)10), ((Suit)2));
            Collection<Card> hand = new Collection<Card> { c6, c7 };
            Collection<Card> flop = new Collection<Card> { c1, c2, c3, c4, c5 };

            Hand myHand = new Hand(hand, flop);
            Assert.AreEqual(PokerHand.HighCard, myHand.HandTitle);
            Assert.AreEqual(Symbol.Ten, myHand.PrimaryValue);
            Assert.AreEqual(new[] { Symbol.Nine, Symbol.Eight, Symbol.Six, Symbol.Five }, myHand.KickersValues);
        }
        [TestCase]
        public void HighcardTests3()
        {

            Card c1 = new Card(((Symbol)14), 0);
            Card c2 = new Card(((Symbol)2), ((Suit)1));
            Card c3 = new Card(((Symbol)3), ((Suit)2));
            Card c4 = new Card(((Symbol)8), ((Suit)3));
            Card c5 = new Card(((Symbol)11), 0);
            Card c6 = new Card(((Symbol)12), ((Suit)1));
            Card c7 = new Card(((Symbol)13), ((Suit)2));
            Collection<Card> hand = new Collection<Card> { c6, c7 };
            Collection<Card> flop = new Collection<Card> { c1, c2, c3, c4, c5 };

            Hand myHand = new Hand(hand, flop);
            Assert.AreEqual(PokerHand.HighCard, myHand.HandTitle);
            Assert.AreEqual(Symbol.Ace, myHand.PrimaryValue);
            Assert.AreEqual(new[] { Symbol.King, Symbol.Queen, Symbol.Jack, Symbol.Eight }, myHand.KickersValues);
        }

    }
}
