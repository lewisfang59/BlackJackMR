using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGameLogic
{
    class ChipsStackHelperFunctions
    {
        private readonly int startingSum, one = 1, five = 5, twentyFive = 25, hundred = 100, fiveHundred = 500;

        /// <summary>
        /// gets chip color
        /// </summary>
        /// <param name="chipValue">value assocaited with chip color</param>
        /// <returns>chip color</returns>
        public int GetChipColor(int chipValue)
        {
            int colorValue;
            if (chipValue == fiveHundred) { colorValue = 5; }
            else if (chipValue == hundred) { colorValue = 4; }
            else if (chipValue == twentyFive) { colorValue = 3; }
            else if (chipValue == five) { colorValue = 2; }
            else { colorValue = 1; }
            return colorValue;
        }

        /// <summary>
        /// gets single chip
        /// </summary>
        /// <param name="chipValue">value of chip</param>
        /// <returns>chip</returns>
        public Chip WhichChip(int chipValue)
        {
            Chip chip = new Chip
            {
                Color = (ChipColor)GetChipColor(chipValue),
                Value = (ChipValue)chipValue
            };
            return chip;
        }

       


    }
}
