using System.Collections.Generic;
using System.Linq;

namespace dev.adventCalendar._2020
{
    class Day06 : Day
    {
        public override string ExecuteFirst()
        {
            List<char> allQ = new List<char>();
            int total = 0;

            foreach (string l in GetFileLines(6))
            {
                foreach (char c in l)
                    if (!allQ.Contains(c))
                        allQ.Add(c);

                if (l.Length == 0)
                {
                    total += allQ.Count;
                    allQ = new List<char>();
                }
            }

            return (total + allQ.Count).ToString();
        }

        public override string ExecuteSecond()
        {
            var lines = GetFileLines(6);
            char[] allQ = lines[0].ToCharArray(); 
            int total = 0;

            for (int i = 0; i < lines.Length; ++i)
            {
                if (lines[i].Length == 0)
                {
                    total += allQ.Length;
                    allQ = lines[i + 1].ToCharArray();
                    continue;
                }

                if (allQ.Length > 0)
                    allQ = allQ.Intersect(lines[i].ToCharArray()).ToArray();
            }

            return (total + allQ.Length).ToString();
        }
    }
}
