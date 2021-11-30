using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dev.adventCalendar._2020
{
    class Day10 : Day
    {
        private int SumAdapters()
        {
            List<int> n = new List<int>(GetIntegers(10, 2020));
            n.Add(n.Max() + 3);

            int diff3 = 0, diff1 = 0;
            int currentNum = 0, nextNum = -1;

            while (n.Count > 0)
            {
                nextNum = n.Where(x => Math.Abs(x - currentNum) <= 3).Min();

                int diff = Math.Abs(currentNum - nextNum);
                if (diff == 1)
                    ++diff1;
                if (diff == 3)
                    ++diff3;

                n.Remove(n.Find(x => x == nextNum));
                currentNum = nextNum;
            }

            return diff3 * diff1;
        }

        public override string ExecuteFirst()
        {
            return SumAdapters().ToString();
        }

        public override string ExecuteSecond() // too low - 1215012992
        {
            List<int> n = new List<int>(GetIntegers(10, 2020));
            n.Add(n.Max() + 3);

            return "";
        }
    }
}
