using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGameLogic
{
    public enum ChipValue
    {
        one = 1,
        five = 5,
        twentyFive = 25,
        hundred = 100,
        fiveHundred = 500
    }

    public enum ChipColor
    {
        white = 1,
        red = 2,
        green = 3,
        black = 4,
        purple = 5
    }
    class Chip
    {
        private ChipValue chipValue;
        private ChipColor chipColor;

        public ChipValue Value
        {
            get { return this.chipValue; }
            set { this.chipValue = value; }
        }

        public ChipColor Color
        {
            get { return this.chipColor; }
            set { this.chipColor = value; }
        }

        public Chip()
        {
            chipColor = 0;
            chipValue = 0;
        }
    }
}
