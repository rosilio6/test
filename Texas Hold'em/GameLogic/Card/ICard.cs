namespace GameLogic
{
    interface ICard
    {
        /* compares cards value,
         *  returning positive number for higher card, 
         *  returning negative number for lower card, 
         *  or zero for equal values
         */
        int Compare(Card other);
    }
}
