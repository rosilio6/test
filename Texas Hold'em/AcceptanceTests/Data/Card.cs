using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcceptanceTests.Data
{
   public class Card
    {

        public Suit Shape { get; }
        public Value Value { get; }

        public Card(Value val, Suit shape)
        {
            Shape = shape;
            Value = val;

        }
    }
}
