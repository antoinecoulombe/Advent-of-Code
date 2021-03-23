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

        private bool HasPairs(string s)
        {
            for (int i = 1; i < s.Length; ++i)
            {
                var parts = s.Split(s[i - 1].ToString() + s[i].ToString());
                if (parts.Length > 2)
                    return true;
            }
            return false;
        }

        private bool HasRepeating(string s)
        {
            for (int i = 2; i < s.Length; ++i)
                if (s[i - 2] == s[i] && s[i] != s[i - 1])
                    return true;
            return false;
        }

        private bool IsValid(string s)
        {
            bool hasRepeating = false;
            int vowelCount = 0;
            for (int i = 0; i < s.Length; ++i)
            {
                if (i > 0 && s[i - 1] == s[i])
                    hasRepeating = true;
                if (vowels.Contains(s[i]))
                    ++vowelCount;
                if (vowelCount >= 3 && hasRepeating)
                    return true;
            }
            return false;
        }

        public override string ExecuteFirst()
        {
            var l = GetFileLines(5, 15).ToList();

            int nice = 0;
            foreach (string s in l)
                if (!disallowed.Any(x => s.Contains(x)) && IsValid(s))
                    ++nice;

            return nice.ToString();
        }

        public override string ExecuteSecond()
        {
            var l = GetFileLines(5, 15).ToList();

            int nice = 0;
            foreach (string s in l)
                if (HasPairs(s) && HasRepeating(s))
                    ++nice;

            return nice.ToString();
        }
    }
}
