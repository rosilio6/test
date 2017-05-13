using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Chip
    {
        public int Sum { get; set; }

        public Chip(int sum)
        {
            this.Sum = sum;
        }

        public void Reduce(int value)
        {
            Sum = Sum - value;
        }
        public void Add(int value)
        {
            Sum = Sum + value;
        }

        public void MakeZero()
        {
            Sum = 0;
        }
    }
}
