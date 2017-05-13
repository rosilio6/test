using GameLogic;
using NUnit.Framework;

namespace Testing.GameTesting
{
    [TestFixture]
    class CardTests
    {
        [TestCase]
        public void GeneralTests()
        {
            Card c1 = new Card(((Symbol)3), ((Suit)3));
            Assert.AreEqual(c1.SuitsType, Suit.Spade);
            Assert.AreEqual(c1.SymbolType, Symbol.Three);
        }

        [TestCase]
        public void Compare()
        {
            Card c1 = new Card(((Symbol)3), ((Suit)3));
            Card c2 = new Card(((Symbol)3), ((Suit)0));
            Card c3 = new Card(((Symbol)14), ((Suit)3));
            Card c4 = new Card(((Symbol)11), ((Suit)3));
            Assert.AreEqual(c1.Compare(c2), 0);
            Assert.AreEqual(c2.Compare(c3), -11);
            Assert.AreEqual(c3.Compare(c4), 3);
            Assert.AreEqual(c3.Compare(c3), 0);
        }
    }
}
