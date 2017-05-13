using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GameLogic
{
    public class Hand
    {
        private readonly Collection<Card> _cards;
        private readonly Collection<Card> _flop;
        public PokerHand HandTitle { get; set; }
        public Symbol PrimaryValue { get; set; }
        public Symbol[] KickersValues { get; set; }
        public Hand(Collection<Card> cards,Collection<Card> flop)
        {
            this._cards = cards;
            this._flop = flop;
           
            InitHand(new List<Card>(_cards.Concat(_flop).ToList()));
        }

        
        private void InitHand(List<Card>  allCards)
        {
            int[] cardValueCount = CardMapper(allCards);

            if (CheckForRoyalFlush(allCards))
            {
                InitHelper5Symbols(InitStraightFlush(allCards),PokerHand.RoyalFlush);
                return;
            }
            if (CheckForStraightFlush(allCards))
            {
                InitHelper5Symbols(InitStraightFlush(allCards), PokerHand.StraightFlush);
                return;
            }
            MyHand sameOfAKind = CheckForSameOfAKind(allCards);
            if (sameOfAKind == MyHand.FourOfAKind)
            {
                InitHelperDynamicSymbols(cardValueCount, 4, PokerHand.FourOfAKind,1,1);
                return;
            }
            if (sameOfAKind == MyHand.FullHouse)
            {
                InitHelperDynamicSymbols(cardValueCount, 3, PokerHand.FullHouse,2,1);
                return;
            }
            if (CheckForFlush(allCards))
            {
                InitHelper5Symbols(InitFlush(allCards), PokerHand.Flush);
                return;
            }
            if (CheckForStraight(allCards))
            {
                InitHelper5Symbols(InitStraight(cardValueCount), PokerHand.Straight);
                return;
            }
            if (sameOfAKind == MyHand.ThreeOfAKind)
            {
                InitHelperDynamicSymbols(cardValueCount, 3, PokerHand.ThreeOfAKind,1,2);
                return;
            }
            if (sameOfAKind == MyHand.TwoPair)
            {
                InitHelperTwoPair(cardValueCount);
                return;
            }
            if (sameOfAKind == MyHand.OnePair)
            {
                InitHelperDynamicSymbols(cardValueCount, 2, PokerHand.OnePair,1,3);
                return;
            }
            InitHighcard(allCards);
        }

        private void InitHighcard(List<Card> allCards)
        {
            List<Card> orderedCards = allCards.OrderBy(card => card.Value).ToList();
            PrimaryValue = orderedCards[orderedCards.Count - 1].SymbolType;
            KickersValues = new Symbol[4];
            int kickerCounter = 0;
            for (int i = orderedCards.Count - 2; i >= orderedCards.Count - 5; i--)
            {
                KickersValues[kickerCounter] = orderedCards[i].SymbolType;
                kickerCounter++;
            }
            this.HandTitle = PokerHand.HighCard;
        }
        private void InitHelper5Symbols(Symbol[] chosenCardSymbols,PokerHand hand)
        {
            PrimaryValue = chosenCardSymbols[0];
            KickersValues = new Symbol[4];
            Array.Copy(chosenCardSymbols, 1, KickersValues, 0, 4);
            this.HandTitle = hand;
        }
        private void InitHelperDynamicSymbols(int[] cardValueCount, int sameOfAKindCounter, PokerHand hand,int kickerCounterToMatch,int kickerValueToFind)
        {
            PrimaryValue = InitPrimarySameOfAKind(cardValueCount, sameOfAKindCounter);
            KickersValues = InitKickersSameOfAKind(cardValueCount, kickerCounterToMatch, kickerValueToFind, new List<Symbol> { PrimaryValue });
            this.HandTitle = hand;
        }
        private void InitHelperTwoPair(int[] cardValueCount)
        {
            PrimaryValue = InitPrimarySameOfAKind(cardValueCount, 2);
            KickersValues = new Symbol[2];
            KickersValues[0] = InitKickersSameOfAKind(cardValueCount, 2, 1, new List<Symbol> { PrimaryValue })[0];
            KickersValues[1] = InitKickersSameOfAKind(cardValueCount, 1, 1, new List<Symbol> { PrimaryValue, KickersValues[0] })[0];
            this.HandTitle = PokerHand.TwoPair;
        }

        private Symbol[] InitFlush(List<Card> cards)
        {
            int[] suitCounter = new int[4];
            int suitflag = -1;
            for (int i = 0; ((i < cards.Count)&&(suitflag==-1)); i++)
            {
                suitCounter[cards[i].SuitVal]++;
                if (suitCounter[cards[i].SuitVal] == 5)
                    suitflag = cards[i].SuitVal;
            }
            List<Card> orderedCards = cards.OrderBy(card => card.Value).ToList();
            Symbol[] symbols = new Symbol[5];
            int symbolCount = 0;
            for (int i = orderedCards.Count-1; ((i >= 0)&&(symbolCount<5)) ; i--)
            {
                if (orderedCards[i].SuitVal == suitflag)
                {
                    symbols[symbolCount] = orderedCards[i].SymbolType;
                    symbolCount++;
                }
            }
            return symbols;
        }

        private Symbol[] InitStraight(int[] cardValueCount)
        {
            int currSeq = 0;
            for (int i = 14; i >= 1; i--)
            {
                if (cardValueCount[i] >= 1)
                { currSeq++; }
                else
                { currSeq = 0; }

                if ((currSeq == 5) && (i == 1))
                    return new Symbol[] { (Symbol)i + 4, (Symbol)i + 3, (Symbol)i + 2, (Symbol)i + 1, (Symbol)14 };
                else if (currSeq == 5)
                    return new Symbol[] { (Symbol)i + 4, (Symbol)i + 3, (Symbol)i + 2, (Symbol)i + 1, (Symbol)i };
            }
            return null;
        }

        private Symbol InitPrimarySameOfAKind(int[] cardValueCount, int countToMatch)
        {
            for (int i = 14; i >= 2; i--)
            {
                if (cardValueCount[i] == countToMatch)
                    return (Symbol)i;
            }
            return 0;
        }
        private Symbol[] InitKickersSameOfAKind(int[] cardValueCount, int countToMatch, int kickersToFind, List<Symbol> ignoreValues)
        {
            Symbol[] symbols = new Symbol[kickersToFind];
            int foundAlready = 0;
            for (int i = 14; ((i >= 2) && (foundAlready < kickersToFind)); i--)
            {
                if ((cardValueCount[i] >= countToMatch) && (!ignoreValues.Contains((Symbol)i)))
                {
                    symbols[foundAlready] = (Symbol)i;
                    foundAlready++;
                }
            }
            return symbols;
        }

        private Symbol[] InitStraightFlush(List<Card> allCards)
        {
            Symbol[] symbols = new Symbol[5];
            for (int i = 0; i < 4; i++)
            {
                symbols = InitStraight(CardMapper(allCards.FindAll(card => (card.SuitVal == i))));
                if(symbols != null)
                    return symbols;
            }
            return null;
        }

        private bool CheckForRoyalFlush(List<Card> cards)
        {
            return CheckForStraightFlush(cards.FindAll(card => (card.Value >= 10)));
        }
        private bool CheckForFlush(List<Card> cards)
        {
            int[] suitCounter = new int[4];
            for (int i = 0; i < cards.Count; i++)
            {
                suitCounter[cards[i].SuitVal]++;
            }
            return (suitCounter.Max()>=5);
        }
        private bool CheckForStraightFlush(List<Card> cards)
        {
            if (CheckForFlush(cards))
            {
                for (int i = 0; i < 4; i++)
                {
                    if (CheckForStraight(cards.FindAll(card => (card.SuitVal == i))))
                        return true;
                }
            }
            return false;
        }
        private bool CheckForStraight(List<Card> cards)
        {
            int[] cardValueCount = CardMapper(cards);
            int currentSequence = 0, maxSequence = 1;
            for (var i = cardValueCount.Length - 1; i >= 1; i--)
            {
                if (cardValueCount[i] > 0)
                    currentSequence++;
                else
                    currentSequence = 0;
                if (currentSequence > maxSequence)
                    maxSequence = currentSequence;
            }
            return (maxSequence >= 5);
        }



        private MyHand CheckForSameOfAKind(List<Card> cards)
        {
            int max = 1,kicker = 1;
            int[] cardValueCount = CardMapper(cards);
            for (var i = cardValueCount.Length-1; i >= 2 ; i--)
            {
                if (max < cardValueCount[i])
                {
                    if (max > kicker)
                        kicker = max;
                    max = cardValueCount[i];
                }
                else if (kicker< cardValueCount[i])
                    kicker = cardValueCount[i];
            }
            if((max <= 3) && (max >= 2) && (kicker>1))
            return (MyHand)(max*2+1);
            return (MyHand)(max*2);
        }
        private static int[] CardMapper(List<Card> cards)
        {
            int[] cardValueCount = new int[15];
            for (var i = 0; i < cards.Count(); i++)
            {
                cardValueCount[cards[i].Value]++;
                if (cards[i].Value == 14)
                { cardValueCount[1]++;}
            }
            return cardValueCount;
        }
        private enum MyHand
        {
            OnePair = 4, TwoPair = 5, ThreeOfAKind = 6, FullHouse = 7, FourOfAKind = 8
        }
        /// <summary>
        /// (result equal zero)  => tie
        /// (result less than zero) -> other hand is stronger, this hand loses
        /// (result greater than zero) -> other hand is weaker, this hand is Stronger
        /// </summary>
        public int Compare(Hand other)
        {
            if (((int)this.HandTitle - (int)other.HandTitle) != 0)
                return ((int)this.HandTitle - (int)other.HandTitle);
            if (((int)this.PrimaryValue - (int)other.PrimaryValue) != 0)
                return ((int)this.PrimaryValue - (int)other.PrimaryValue);

            for (int i = 0; i < KickersValues.Length - 1; i++)
            {
                if (((int)this.KickersValues[i] - (int)other.KickersValues[i]) != 0)
                    return ((int)this.KickersValues[i] - (int)other.KickersValues[i]);
            }
            return 0;
        }
    }

}
