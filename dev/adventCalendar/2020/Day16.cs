using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dev.adventCalendar._2020
{
    class Day16 : Day
    {
        private class ValidNumbers
        {
            private bool[] valid;

            private void InitNumbers()
            {
                valid = new bool[1000];
                for (int i = 0; i < valid.Length; ++i)
                    valid[i] = false;
            }

            private (int, int) SplitNumbers(string s)
            {
                var n = s.Split('-');
                return (int.Parse(n[0]), int.Parse(n[1]));
            }

            private List<(int, int)> GetNumberRanges(List<string> lines)
            {
                var valid = new List<(int, int)>();
                var constraintsEnd = lines.FindIndex(x => x.Contains("your ticket")) - 1;

                for (int i = 0; i < constraintsEnd; ++i)
                {
                    if (!lines[i].Contains(':'))
                        continue;

                    var s = lines[i].Split(": ")[1].Split(" or ");
                    valid.Add(SplitNumbers(s[0]));
                    valid.Add(SplitNumbers(s[1]));
                }

                return valid;
            }

            private void GetValidNumbers(List<(int, int)> numbers)
            {
                foreach ((int lower, int upper) in numbers)
                    for (int i = lower; i <= upper; ++i)
                        valid[i] = true;
            }

            public ValidNumbers(List<string> lines)
            {
                InitNumbers();
                GetValidNumbers(GetNumberRanges(lines));
            }

            public bool[] Get()
                => valid;
        }

        private int SumErrorRate(List<string> lines, bool[] validNumbers)
        {
            int total = 0;

            int start = lines.FindIndex(x => x.Contains("nearby tickets")) + 1;
            for (int i = start; i < lines.Count; ++i)
            {
                var numbers = lines[i].Split(',');
                foreach (var num in numbers)
                {
                    var n = int.Parse(num);
                    if (!validNumbers[n])
                        total += n;
                }
            }

            return total;
        }

        public override string ExecuteFirst()
        {
            var lines = GetFileLines(16).ToList();
            var validNumbers = new ValidNumbers(lines).Get();
            return SumErrorRate(lines, validNumbers).ToString();
        }

        public override string ExecuteSecond()
        {
            return "";
        }
    }
}
