using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGameLogic
{
    class ChipsStack
    {
        ChipsStackHelperFunctions hf;
        private readonly int startingSum, one = 1, five = 5, twentyFive = 25, hundred = 100, fiveHundred = 500;
        private List<Chip> white1, red5, green25, black100, purple500;
        private Dictionary<String, List<Chip>> chipStack;
        private StringBuilder str = new StringBuilder();
        private List<String> posCombinations;

        /// <summary>
        /// creates instance of all chips
        /// </summary>
        /// <param name="initialSum">user's initial availble money</param>
        public ChipsStack(int initialSum)
        {
            chipStack = new Dictionary<string, List<Chip>>();
            while (initialSum > 0)
            {
                if (initialSum >= fiveHundred) { 
                    initialSum -= fiveHundred;
                    purple500.Add(hf.WhichChip(fiveHundred));
                }
                else if (initialSum >= hundred) { 
                    initialSum -= hundred;
                    black100.Add(hf.WhichChip(hundred));
                }
                else if (initialSum >= twentyFive) { 
                    initialSum -= twentyFive;
                    green25.Add(hf.WhichChip(twentyFive));
                }
                else if (initialSum >= five) { 
                    initialSum -= five;
                    red5.Add(hf.WhichChip(five));
                }
                else
                {
                    initialSum -= one;
                    white1.Add(hf.WhichChip(one));
                }
            }
            chipStack.Add("purple500", purple500);
            chipStack.Add("black100", black100);
            chipStack.Add("green25", green25);
            chipStack.Add("red5", red5);
            chipStack.Add("white1", white1);
        }

        /// <summary>
        /// gives a list of all possible combinations of chips currently in user's possesion to hit desired bet value
        /// </summary>
        /// <param name="l1">white1</param>
        /// <param name="l2">red5</param>
        /// <param name="l3">green25</param>
        /// <param name="l4">black100</param>
        /// <param name="l5">purple500</param>
        /// <param name="target">desired bet value</param>
        /// <returns></returns>
        public List<String> CanBet(List<Chip> l1, List<Chip> l2, List<Chip> l3, List<Chip> l4, List<Chip> l5, int target)
        {
            List<int> chips = new List<int>();
            for (int i = 0; i < l1.Count; i++)
                chips.Add(1);
            for (int i = 0; i < l2.Count; i++)
                chips.Add(5);
            for (int i = 0; i < l3.Count; i++)
                chips.Add(25);
            for (int i = 0; i < l4.Count; i++)
                chips.Add(100);
            for (int i = 0; i < l5.Count; i++)
                chips.Add(500);
            posCombinations.Clear();
            PossibleBets(chips, target, new List<int>());
            return posCombinations;
        }
        /// <summary>
        /// recursive function that would determine all possible combinations of chips to reach desired bet value
        /// </summary>
        /// <param name="numbers">list of values</param>
        /// <param name="target">desired bet value</param>
        /// <param name="partial">list of partial possible values</param>
        public void PossibleBets(List<int> numbers, int target, List<int> partial)
        {

            int s = 0;
            foreach (int x in partial) s += x;

            if (s == target)
                if (partial.Count() != 0)
                {
                    str.Append(string.Join(" ", partial.ToArray()));
                    posCombinations.Add(str.ToString());
                    str.Clear();
                }
            if (s >= target)
                return;

            for (int i = 0; i < numbers.Count; i++)
            {
                List<int> remaining = new List<int>();
                int n = numbers[i];
                for (int j = i + 1; j < numbers.Count; j++) remaining.Add(numbers[j]);

                List<int> partial_rec = new List<int>(partial);
                partial_rec.Add(n);
                PossibleBets(remaining, target, partial_rec);
            }
        }

    }
}
