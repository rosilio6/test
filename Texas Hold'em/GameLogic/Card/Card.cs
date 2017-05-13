namespace GameLogic
{
    public class Card : ICard
    {
        public Suit SuitsType { get; }
        public Symbol SymbolType { get; }
        public int Value { get; }
        public int SuitVal { get; }

        public Card(Symbol symbol, Suit suit)
        {
            SuitsType = suit;
            SymbolType = symbol;
            Value = (int)symbol;
            SuitVal = (int) suit;
        }
        public int Compare(Card other)
        {
            return this.Value - other.Value;
        }

        public override string ToString()
        {
            return SymbolType + "(" + SuitsType + ") ";
        }
    }
}
