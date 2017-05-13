namespace GameLogic
{
    public enum PokerHand : ulong
    {
        HighCard = 1,
        OnePair = 10,
        TwoPair = 100,
        ThreeOfAKind = 1000,
        Straight = 10000,
        Flush = 100000,
        FullHouse = 1000000,
        FourOfAKind = 10000000,
        StraightFlush = 100000000,
        RoyalFlush = 1000000000,

    }
}