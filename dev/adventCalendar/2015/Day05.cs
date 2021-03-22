using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dev.adventCalendar._2015
{
    class Day05 : Day
    {
        private static readonly List<string> disallowed = new() { "ab", "cd", "pq", "xy" };
        private static readonly List<char> vowels = new() { 'a', 'e', 'i', 'o', 'u' };

        public override string ExecuteFirst()
        {
            var l = GetFileLines(5, 15).ToList();
            int nice = 0;

            foreach (string s in l)
            {
                if (disallowed.Any(x => s.Contains(x)))
                    continue;

                char prev = s[0];
                bool hasRepeating = false;
                int vowelCount = 0;
                for (int i = 0; i < s.Length; ++i)
                {
                    if (i > 0 && prev == s[i])
                        hasRepeating = true;
                    else if (!hasRepeating)
                        prev = s[i];

                    if (vowels.Contains(s[i]))
                        ++vowelCount;

                    if (vowelCount >= 3 && hasRepeating)
                    {
                        ++nice;
                        continue;
                    }
                }
            }

            return nice.ToString();
        }

        public override string ExecuteSecond()
        {
            return "";
        }
    }
}
