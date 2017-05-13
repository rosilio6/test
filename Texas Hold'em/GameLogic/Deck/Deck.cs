using System;
using System.Collections.ObjectModel;

namespace GameLogic
{
    public class Deck : IDeck
    {

        private int _currTop;
        private Card[] _cards;
        public Deck()
        {
            _currTop = 52;
            _cards = new Card[52];
            for (int i = 2; i < 15; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    _cards[_currTop-1] =new Card((Symbol)i, (Suit)j);
                    _currTop--;
                }
            }
            _currTop = 51;
            Shuffle();
        }
        

        public Card PickACard()
        {
            if (_currTop < 0)
            { return null; }
             _currTop--;
            return _cards[_currTop + 1];
        }

        public void Shuffle()
        {
            for (int j = 0; j < 1001; j++)
            {
                var rand = new Random();
                for (int i = 51; i > 0; i--)
                {
                    int n = rand.Next(i + 1);
                    Card temp = _cards[i];
                    _cards[i] = _cards[n];
                    _cards[n] = temp;
                }
            }
            for (int i = 0; i < 6000; i++)
            {
                ShuffleChunk(1);
            }

            Random rnd = new Random();
            for (int i = 0; i < 2000; i++)
            {
                ShuffleChunk(rnd.Next(5, 10));
            }

            for (int i = 0; i < 1000; i++)
            {
                ShuffleChunk(rnd.Next(10, 20));
            }

        }
        private void ShuffleChunk(int chunkSize)
        {
            Random rnd = new Random();
            int firstNum = rnd.Next(52 - chunkSize + 1);
            int secondNum = rnd.Next(52 - chunkSize + 1);
            Card tempCard;
            for (int i = 0; i < chunkSize; i++)
            {
                tempCard = _cards[secondNum + i];
                _cards[secondNum + i] = _cards[firstNum + i];
                _cards[firstNum + i] = tempCard;
            }
        }

        public Collection<Card> Pick1Card()
        {
            return new Collection<Card> { PickACard() };
        }

        public Collection<Card> Pick2Card()
        {
            return new Collection<Card> { PickACard(), PickACard() };
        }

        public Collection<Card> Pick3Card()
        {
            return new Collection<Card> { PickACard(), PickACard(), PickACard() };
        }
    }
}
